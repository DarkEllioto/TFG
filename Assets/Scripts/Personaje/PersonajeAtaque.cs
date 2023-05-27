using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonajeAtaque : MonoBehaviour
{
    public static Action<float> EventoEnemigoDañado;
    [Header("Stats")] 
    [SerializeField] private PersonajeStats stats;

    //SE OBTIENE LA REFERENCIA
     [Header("Pooler")] 
    [SerializeField] private ObjectPooler pooler;

    //REFERENCIAS DE LAS POCISIONES DE ATAQUE
    [Header("Ataque")] 
    [SerializeField] private float tiempoEntreAtaques;
     [SerializeField] private Transform[] posicionesDisparo;

    //SE CREA UNA PROPIEDAD DE TIPO ARMA
    public Arma ArmaEquipada { get; private set; }
    //PROPIEDAD DE TIPO ENEMIGO INTERACCION
    public EnemigoInteraccion EnemigoObjetivo { get; private set; }
    //PROPIEDAD DE ATACAR ES DE TIPO BOOLEAN
     public bool Atacando { get; set; }
   
    //VARIABLE PARA OBTENER LAS DIRECCIONES DEL PERSONAJE
     private int indexDireccionDisparo;
     //VARIABLE  DE PERSONAJE MANA
      private PersonajeMana _personajeMana;
       private float tiempoParaSiguienteAtaque;



      private void Awake()
    {
        //SE OBTIENE EL COMPONENTE DE PERSONAJE MANA
        _personajeMana = GetComponent<PersonajeMana>();
    }

        private void Update()
    {
        ObtenerDireccionDisparo();
        //SI EL TIEMPO ACTUAL ES MAYOR QUE EL TIEMPO PARA SIGUIENTE ATAQUE
          if (Time.time > tiempoParaSiguienteAtaque)
        {
            //AL PULSAR LA TECLA ESPACIO 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //SE COMPRUEBA SI EL ENEMIGO OBJETIVO O UN ARMA EQUIPADA SON NULOS
                if (ArmaEquipada == null || EnemigoObjetivo == null)
                {
                    //SI NO SE REGRESA
                    return;
                }
                //DE LO CONTRARIO SE LLAMA AL METODO USAR ARMA
                UsarArma();
                //SE COMPRUEBA EL TIEMPO PARA EL SIGUIENTE ATAQUE Y SE ACTUALIZA
                tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques;
                //SE LLAMA AL METODO DE CONDICIONES DE ATAQUE
               StartCoroutine(IEEstablecerCondicionAtaque());
            }
        }
    }



    //METODO USAR ARMA
    private void UsarArma()
    {
        //SE VERIFICA QUE EL ARMA ES DE TIPO MAGIA
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            //SEGUIDAMENTE SE VERIFICA SI HAY MANA SUFICIENTE
            if (_personajeMana.ManaActual < ArmaEquipada.ManaRequerida)
            {
                //SI NO HAY SUFICIENTE SE REGRESA
                return;
            }
            //PARA UTILIZAR EL ARMA SE OBTIENE UNA INSTANCIA DE UN PROYECTIL
            GameObject nuevoProyectil = pooler.ObtenerInstancia();
            //SE OBTIENE LA POCISION DESDE DONDE SALE
            nuevoProyectil.transform.localPosition = posicionesDisparo[indexDireccionDisparo].position;
            //SE OBTIENE EL COMPONENTE EL COMPONENTE DE PROYECTIL 
            Proyectil proyectil = nuevoProyectil.GetComponent<Proyectil>();
            //SE INICIALIZA ESTE PROYECTIL
            proyectil.InicializarProyectil(this);
            //se activa el nuevo proyectil
            nuevoProyectil.SetActive(true);
            //SE DESCUENTA EL MANA QUE SE ESTA UTILIZANDO 
            _personajeMana.UsarMana(ArmaEquipada.ManaRequerida);
        }
        else //DE LO CONTRARIO 
        {
            float daño = ObtenerDaño();
            EnemigoVida enemigoVida = EnemigoObjetivo.GetComponent<EnemigoVida>();
            enemigoVida.RecibirDaño(daño);
            EventoEnemigoDañado?.Invoke(daño);
        }
    }
    //METODO DE OBTENER DAÑO 
     public float ObtenerDaño()
    {
        //SE OBTIENE REFERENCIA  DE DAÑO ACTUAL 
        float cantidad = stats.Daño;
        //SI EL PORCENTAJE DE CRITICO DIVIDIDO ENTRE 100 ES MAYOR QUE RANDOM VALUE
        if (Random.value < stats.DCritico / 100)
        {
            //SE DUPLICA LA CANTIDAD DE DAÑO 
            cantidad *= 2;
        }

        //DE LO CONTRARIO EL DAÑO ES NORMAL
        return cantidad;
    }

    //METODO IE ENUMERATOR PARA LAS CONDICIONES DE ATAQUE
     private IEnumerator IEEstablecerCondicionAtaque()
    {
        //SE ESTABLECE ATACANDO A VERDADERO
        Atacando = true;
        //LUEGO DE UNOS SEGUNDOS
        yield return new WaitForSeconds(0.3f);
        //SE ESTABLECE A FALSE
        Atacando = false;
    }
     

    //SE CREA EL METODO EQUIPAR ARMA SE PASA COMO PARAMETRO UN ITEM ARMA
     public void EquiparArma(ItemArma armaPorEquipar)
    {
        //SE DECLARA QUE ARMA EQUIPADA ES IGUAL A ARMA EQUIPADA
        ArmaEquipada = armaPorEquipar.Arma;
        //SE VERIFICA SI EL ARMA ES DE TIPO MAGIA 
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            //SE CREA EL POOLER Y SE LE PASA EL PROYECTIL
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }
        //SE LLAMA AL METODO DE LA CLASE STATS Y SE LE PASA LA REFERENCIA DEL ARMA
       stats.AñadirBonusPorArma(ArmaEquipada);
    }

    //METODO PARA REMOVER ARMA
     public void RemoverArma()
    {
        //SE VERIFICA SI EXISTE UN ARMA EQUIPADA 
        if (ArmaEquipada == null)
        {
            //SI ESTA NUL SE REGRESA
            return;
        }
        //CUANDO SE REMUEVE EL ARMA TAMBIEN SE REMUEVE EL POOLER 
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }
          //SE LLAMA AL METODO DE LA CLASE STATS Y SE LE PASA LA REFERENCIA DEL ARMA
       stats.RemoverBonusPorArma(ArmaEquipada);
        //SI ES DISTINTO A NUL SE PONE EN NULO 
        ArmaEquipada = null;
    }

    //METODO PARA OBTENER LA DIRECCION DEL DISPARO 
     private void ObtenerDireccionDisparo()
    {
        //SE OBTIENE EL MOVIMIENTO DEL PERSONAL SI ES HORIZONTAL O VERTICAL 
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //SE EL MOVIMIENTO ES HACIA LA DERECHA 
        if (input.x > 0.1f)
        {
            //EL INDEX O EL DISPARO ES HACIA LA DERECHA
            indexDireccionDisparo = 1;
        }
        //SI ES HACIA LA IZQUIERDA 
        else if (input.x < 0f)
        {
            //EL INDEX DEL DISPARO ES 3
            indexDireccionDisparo = 3;
        }
        //SI EL MOVIMIENTO ES HACIA ARRIBA
        else if (input.y > 0.1f)
        {
            //EL INDEX DE DISPARO ES 0
            indexDireccionDisparo = 0;
        }
        //SI EL MOVIMIENTO ES HACIA ABAJO 
        else if (input.y < 0f)
        {
            //EL DISPARO ES 2
            indexDireccionDisparo = 2;
        }
    }








    //SE CREA EL METODO SELECCION RANGO ENEMIGO SE LE PASA UN PARAMETRO DE TIPO ENEMIGO INTERACCION
    private void EnemigoRangoSeleccionado(EnemigoInteraccion enemigoSeleccionado)
    {
        //SI EL ARMA NO ESTA EQUIPADA SE REGRESA
        if (ArmaEquipada == null) { return; }
        //SI EL ARMA ES DISTINTA DE MAGIA SE REGRESA
        if (ArmaEquipada.Tipo != TipoArma.Magia) { return; }
        //SI EL ENEMIGO YA HA SIDO SELECCIONADO SE REGRESA
        if (EnemigoObjetivo == enemigoSeleccionado) { return; }

    //DE LO CONTRARIO SE OBTIENE LA REFERENCIA
        EnemigoObjetivo = enemigoSeleccionado;
        
        //SE LLAMA AL METODO Y SE LE PASA LOS PARAMETROS
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Rango);
    }

//SE CREA EL METODO ENEMIGO NO SELECCIONADAO
     private void EnemigoNoSeleccionado()
    {   
        //SI NO HAY UN ENEMIGO SELECCIONADO SE REGRESA
        if (EnemigoObjetivo == null) { return; }
        //SI NO SE PASA EL PARAMETRO FALSE
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Rango);
        //SE PONE EL ENEMIGO OBJETIVO A NULL
        EnemigoObjetivo = null;
    }
    //METODO ENEMIGO MELEE DETECTADO CON PARAMETRO DE TIPO ENEMIGO INTERACCION
    private void EnemigoMeleeDetectado(EnemigoInteraccion enemigoDetectado)
    {
        //SI EL ARMA ESTA NULL SE REGRESA
        if (ArmaEquipada == null) { return; }
        //SI EL ARMA ES DISTINTA A TIPO MELEE SE REGRESA
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        //DE LO CONTRARIO SE DETECTA EL ENEMIGO ESTABLECIENDO LA REFERENCIA
        EnemigoObjetivo = enemigoDetectado;
        //SE LLAMA AL METODO Y SE LE PASA LOS PARAMETROS
        EnemigoObjetivo.MostrarEnemigoSeleccionado(true, TipoDeteccion.Melee);
    }

    //METODO ENEMIGO MELEE PERDIDO 
    private void EnemigoMeleePerdido()
    {
        //SI NO HAY UN ENEMIGO SELECCIONADO SE REGRESA
        if (ArmaEquipada == null) { return; }
        //SI EL ENEMIGO OBJETIVO ES NULL SE REGRESA 
        if (EnemigoObjetivo == null) { return; }
        //SI EL ARMA ES DISTINTA A MELLE SE REGRESA
        if (ArmaEquipada.Tipo != TipoArma.Melee) { return; }
        //SI NO SE PASA EL PARAMETRO FALSE Y SE DESACTIVA
        EnemigoObjetivo.MostrarEnemigoSeleccionado(false, TipoDeteccion.Melee);
        EnemigoObjetivo = null;
    }



    //METODO 
      private void OnEnable()
    {
        //SE SUSCRIBE  EL EVENTO SELECIONADO AL ENEMIGO RANGO
        SeleccionManager.EventoEnemigoSeleccionado += EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado += EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado += EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido += EnemigoMeleePerdido;
    }

    private void OnDisable()
    {

        SeleccionManager.EventoEnemigoSeleccionado -= EnemigoRangoSeleccionado;
        SeleccionManager.EventoObjetoNoSeleccionado -= EnemigoNoSeleccionado;
        PersonajeDetector.EventoEnemigoDetectado -= EnemigoMeleeDetectado;
        PersonajeDetector.EventoEnemigoPerdido -= EnemigoMeleePerdido;
    }
   
}

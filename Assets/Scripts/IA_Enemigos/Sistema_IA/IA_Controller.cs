using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public enum TiposDeAtaque //enumeracion para los tipos de ataque
{
    Melee,
    Embestida
}
//CLASE QUE FUNCIONARA COMO EL CEREBRO DE LOS ENEMIGOS
public class IA_Controller : MonoBehaviour
{

   
    public static Action<float> EventoDañoRealizado;

    [Header("Stats")] 
    [SerializeField] private PersonajeStats stats; //SE OBTIENE ESTA REFERENCIA DEL ESTAS PARA BLOQUEAR ATAQUE

    //SE DECLARA UNA VARIABLE PARA VER CON QUE ESTADO INICIA UN ENEMIGO
    //EL HEADER ES PARA MEJORAR Y ORDENAR EL INSPECTOR
    [Header("Estados")]
    [SerializeField] private IA_Estado estadoInicial;
    [SerializeField] private IA_Estado estadoDefault;

    
    [Header("Config")] 
    [SerializeField] private float rangoDeteccion;//VARIABLE DE RANGO DE DETENCION
    [SerializeField] private float rangoDeAtaque; //RANGO DE ATAQUE
    [SerializeField] private float rangoDeEmbestida;//RANGO DE EMBESTIDA
    [SerializeField] private float velocidadMovimiento;//VATIABLE VELOCIDAD DEL MOVIMIENTO DEL ENEMIGO 
    [SerializeField] private float velocidadDeEmbestida;//VELOCIDAD DE EMBESTIDA
    [SerializeField] private LayerMask personajeLayerMask; //LEYER DEL PERSONAJE A DETECTAR 

    [Header("Ataque")] //VARIABLES PARA ATAQUES Y DAÑO 
    [SerializeField] private float daño;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private TiposDeAtaque tipoAtaque;

    [Header("Debug")] 
    [SerializeField] private bool mostrarDeteccion;  //VARIABLE PARA MOSTRAR
    [SerializeField] private bool mostrarRangoAtaque; //VARIABLE PARA RANGO DE ATAQUE
    [SerializeField] private bool mostrarRangoEmbestida;//mostrar el rango de embestida
    

     private float tiempoParaSiguienteAtaque; //VARIABLE TIEMPO DE UN ATAQUE A OTRO
     private BoxCollider2D _boxCollider2D; //VARIABLE PARA COLIDER DEL ENEMIGO
 

    //SE CREAN PROPIEDADES PARA GUARDAR 
    public Transform PersonajeReferencia { get; set; }
    public IA_Estado EstadoActual { get; set; }
    public EnemigoMovimiento EnemigoMovimiento { get; set; }
    public float RangoDeteccion => rangoDeteccion;
    // public float RangoDeAtaque => rangoDeAtaque;
    public float Daño =>daño;
    public TiposDeAtaque TipoAtaque => tipoAtaque;
    public float VelocidadMovimiento => velocidadMovimiento;
    public LayerMask PersonajeLayerMask => personajeLayerMask;
    public float RangoDeAtaqueDeterminado => tipoAtaque == TiposDeAtaque.Embestida ? rangoDeEmbestida : rangoDeAtaque; //SE GUARDA EL TIPO DE ATAQUE, SE VERIFICA QUE TIPO DE ATAQUE SE ESTA EJECUTANDO. 



//PARA INICIALIZAR EL ESTADO
        private void Start()
    {
    
         _boxCollider2D = GetComponent<BoxCollider2D>();
        EstadoActual = estadoInicial;
        EnemigoMovimiento = GetComponent<EnemigoMovimiento>(); //referencia de enemigo Movimiento
    }

//METODO PARA EJECUTAR EL METODO PRINCIPAR DE ESTADO
       private void Update()
    {
        //SE LLAMA EL METODO Y SE LE PASA EL CONTROLLER QUE SERIA THIS O ESTE 
        EstadoActual.EjecutarEstado(this);
    }

//PARA CAMBIAR EL ESTADO  SE PASA PARAMETROS DEL NUEVO ESTADO QUE SE QUIERE CAMBIAR
      public void CambiarEstado(IA_Estado nuevoEstado)
    {
        //CON UN IF SE COMPRUEBA EL NUEVO ESTADO Y SE CAMBIA 
        if (nuevoEstado != estadoDefault)
        {
            EstadoActual = nuevoEstado;
        }
    }

    //METODO ATAQUE MELEE
    public void AtaqueMelee(float cantidad)
    {
        if (PersonajeReferencia != null) //SI PERSONAJE NO ES NULL
        {
            AplicarDañoAlPersonaje(cantidad); //SE LLAMA AL METODO APLICAR DAÑO 
        }
    }

    //METODO PARA EJECUTAR EL ATAQUE DE EMBESTIDA
     public void AtaqueEmbestida(float cantidad) //SE PASA UN PARAMETRO TIPO FLOAT
    {
        StartCoroutine(IEEmbestida(cantidad));  //SE LLAMA AL METODO
    }


    //METODO DE EMBESTIDA
    private IEnumerator IEEmbestida(float cantidad) //CON PARAMETRO DE TIPO FLOAT
    {
        Vector3 personajePosicion = PersonajeReferencia.position; //SE OBTIENE LA POCISION DEL PERSONAJE
        Vector3 posicionInicial = transform.position;  //POSICION INICIAL DEL ENEMIGO
        Vector3 direccionHaciaPersonaje = (personajePosicion - posicionInicial).normalized; //DIRECCION HACIA DONDE SE REALIZA LA EMBESTIDA
        Vector3 posicionDeAtaque = personajePosicion - direccionHaciaPersonaje * 0.5f; //POSICION DE ATAQUE 
        _boxCollider2D.enabled = false; //SE DESACTIVA PARA EVITAR POSIBLE ERROR EN COLICIONES
        
        float transicionDeAtaque = 0f; //TRANSICION DE ATAQUE IGUAL A 0 
        while (transicionDeAtaque <= 1f) //MIENTRAS SEA  MENOR  O IGUAL A 1 
        {
            transicionDeAtaque += Time.deltaTime * velocidadMovimiento;  //SE SUMA EL TIEMPO POR LA VELOCIDAD
            float interpolacion = (-Mathf.Pow(transicionDeAtaque, 2) + transicionDeAtaque) * 4f; // SE OBTIENE LA INTERPOLACION
            transform.position = Vector3.Lerp(posicionInicial, posicionDeAtaque, interpolacion); //SE AJUSTA  LA POCISION DEL ENEMIGO 
            yield return null; //SE ESPERA UN FRAME
        }

        if (PersonajeReferencia != null) //CON UN IF DE SEGURIDAD SE COMPRUEBA SI SE TIENE LA REFERENCIA DEL PERSONAJE
        {
            AplicarDañoAlPersonaje(cantidad);// SE EJECUTA APLICAR DAÑOS
        }

        _boxCollider2D.enabled = true; // SE ACTIVA 
    }

    //PARA PODER ATACAR 
      public void AplicarDañoAlPersonaje(float cantidad) //SE PASA COMO PARAMETRO EL DAÑO
    {
        float dañoPorRealizar = 0; //SE INICIALIZA LA VARIABLE A 0
         //SE OBTIENE UN NUMERO RANDOM ENTRE 0 Y 1 
         //SI EL VALOR ES MENOR QUE STATS.PORCENTAJE BLOQUEO DIVIDIDO EN 100 SE DIVIDI PARA PODER COMPARAR 
        if (Random.value < stats.Bloquear / 100)
        {
            return;//SE REGRESA
        }

//SI LO SUPERA
//SE GUARDA EL DAÑO 
        dañoPorRealizar = Mathf.Max(cantidad - stats.Defensa, 1f); //SE ASEGURA QUE EL ENEMIGO HAGA AL MENOS UN 1 DE DAÑO
        PersonajeReferencia.GetComponent<PersonajeVida>().RecibirDaño(dañoPorRealizar); //SE APLICA EL DAÑO 
         EventoDañoRealizado?.Invoke(dañoPorRealizar); //SE LLAMA AL EVENTO EL SIMBOLO DE PREGUNTA - SI NO ES NULO SE INVOCA
      
    }

        //METODO QUE COMPRUEBA SI ESTAMOS EN RANGO DE ATAQUE
      public bool PersonajeEnRangoDeAtaque(float rango)
    {
        //GUARDAMOS LA POCICION DEL PERSONAJE MENOS LA POSICION DEL ENEMIGO  Y PARA OBTENER LA DISTANCIA SE USA SQRMAGNITUD QUE ES AL CUADRADO
        float distanciaHaciaPersonaje = (PersonajeReferencia.position - transform.position).sqrMagnitude;
        //SI LA DISTANCIA ES MENOR AL RANGO SE REGRESA VERDADERO, CON LA CLASE MATHF.POW SE SUBE AL CUADRADO A QUE DISTANCIA HACIA PERSONAJE ESTA AL CUADRADO
        if (distanciaHaciaPersonaje < Mathf.Pow(rango, 2))
        {
            return true;
        }

        return false; //SI NO FALSE
    }

    //METODO QUE NOS REGRESARA VERDADERO O FALSO SI YA PASO EL TIEMPO ENTRE UN ATAQUE Y OTRO
    public bool EsTiempoDeAtacar()
    {
        if (Time.time > tiempoParaSiguienteAtaque) //SI ES TIEMPO DE ATACAR , SI EL TIEMPO ACTUAL ES MAYOR QUE EL TIEMPO PARA EL ATAQUE
        {
            return true; //DEVUELVE TRUE
        }

        return false; //DEVUELVE FALSE
    }


    //METODO PARA ACTUALIZAR EL TIEMPO ENTRE ATAQUE
     public void ActualizarTiempoEntreAtaques()
    {
        tiempoParaSiguienteAtaque = Time.time + tiempoEntreAtaques; // SE SUMA EL TIEMPO ACTUAL CON LA VARIABLE TIEMPO ENTRE ATAQUE
    }

    //metodo para mostrar el rango de deteccion
    private void OnDrawGizmos()
    {
        if (mostrarDeteccion) //SE MARCA UN CIRCULO PARA SABER EL CAMPO DE DETENCION 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
        }

         if (mostrarRangoAtaque)//SI MOSTRAR RANGO DE ATAQUE ESTA SELECCIONADO
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, rangoDeAtaque);
        }
        if (mostrarRangoEmbestida)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangoDeEmbestida);
        }
    }
  
}

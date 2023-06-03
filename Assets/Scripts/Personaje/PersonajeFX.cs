using System;
using System.Collections;
using UnityEngine;

//ENUMERACION PARA LOS TIPOS DE PERSONAJE 
public enum TipoPersonaje
{
    Player,
    IA
}

public class PersonajeFX : MonoBehaviour
{

     //SE OBTIENE LA REFERENCIA
     [Header("Pooler")] 
    [SerializeField] private ObjectPooler pooler;

       [Header("config")] 
    //SE OBTIENE LA REFERENCIA DEL CANVAS 
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    //EXPECIFICACION DEL DONDE SE PONE EL TEXTO
    [SerializeField] private Transform canvasTextoPosicion;

     [Header("Tipo")] 
     //VARIABLE DE TIPO PERSONAJE
    [SerializeField] private TipoPersonaje tipoPersonaje;

    //REFERENCIA DEL ENEMIGO 
     private EnemigoVida _enemigoVida;

     private void Awake()
    {
        //SE OBTIENE EL COMPONENTE DEL ENEMIGO
        _enemigoVida = GetComponent<EnemigoVida>();
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab); //se crea el objeto del canvas
    }

    private IEnumerator IEMostrarTexto(float cantidad, Color color)   //para mostrar el texto se expecifica el daño que se esta haciendo 
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia(); //nuevo game object //se obtiene la instancia del ppoler
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>(); //SE ESTABLECE EL TEXTO DEL DAÑO, REFERENCIA DE LA CLASE
        texto.EstablecerTexto(cantidad, color); //SE LE ESTABLECE EL NUMERO DE DAÑO
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion); //SE EXTABLECE EL PAREN DEL TEXTO PARA QUE EL TEXTO SE MUEVA CON EL PERSONAJE
        nuevoTextoGO.transform.position = canvasTextoPosicion.position; //SE PASA LA POSICION DEL PERSONAJE
        nuevoTextoGO.SetActive(true);// SE ACTIVA A TRUE

        yield return new WaitForSeconds(1f);   //SE ESPERA UN SEGUNDO PARA DESACTIVAR
        nuevoTextoGO.SetActive(false); //SE DESACTIVA
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform); //SE PONE EL PAREN OTRA VEZ A LA LISTA CONTENEDOR 
    }
    //METODO PARA PASAR EL DAÑO 
    private void RespuestaDañoRecibidoHaciaPlayer(float daño)
    {
        //SE COMPRUEBA QUE SEA UN JUGADOR
        if (tipoPersonaje == TipoPersonaje.Player){
            //SE MUESTRA EL TEXTO DEL DAÑO 
            StartCoroutine(IEMostrarTexto(daño, Color.black));
        }
    }

        //METODO DEL DAÑO AL ENEMIGO SE PASA PARAMETRO TIPO FLOAT
    private void RespuestaDañoHaciaEnemigo(float daño, EnemigoVida enemigoVida)
    {
        //SE COMPRUEBA SI EL PERSONAJE ES DE TIPO ENEMIGO 
        if (tipoPersonaje == TipoPersonaje.IA && _enemigoVida == enemigoVida)
        {
            //SE MUESTRA EL DAÑO  
            StartCoroutine(IEMostrarTexto(daño, Color.red));
        }
    }
    
    private void OnEnable() //SE RESPONDE AL EVENTO
    {
        IA_Controller.EventoDañoRealizado += RespuestaDañoRecibidoHaciaPlayer;
        //SE GUARDA EL DAÑO GENERADO AL ENEMIGO
         PersonajeAtaque.EventoEnemigoDañado += RespuestaDañoHaciaEnemigo;
    }

    private void OnDisable() //SE RESPONDE AL EVENTO 
    {
        IA_Controller.EventoDañoRealizado -= RespuestaDañoRecibidoHaciaPlayer;
         PersonajeAtaque.EventoEnemigoDañado -= RespuestaDañoHaciaEnemigo;
    }
}

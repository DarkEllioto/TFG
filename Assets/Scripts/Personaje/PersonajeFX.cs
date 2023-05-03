using System;
using System.Collections;
using UnityEngine;


public class PersonajeFX : MonoBehaviour
{

    //SE OBTIENE LA REFERENCIA DEL CANVAS 
    [SerializeField] private GameObject canvasTextoAnimacionPrefab;
    //EXPECIFICACION DEL DONDE SE PONE EL TEXTO
    [SerializeField] private Transform canvasTextoPosicion;


    //REFERENCIA DE LA CLASE Object Pooler
    private ObjectPooler pooler;

    private void Awake()
    {
        pooler = GetComponent<ObjectPooler>(); 
    }

    private void Start()
    {
        pooler.CrearPooler(canvasTextoAnimacionPrefab); //se crea el objeto del canvas
    }

    private IEnumerator IEMostrarTexto(float cantidad)   //para mostrar el texto se expecifica el daño que se esta haciendo 
    {
        GameObject nuevoTextoGO = pooler.ObtenerInstancia(); //nuevo game object //se obtiene la instancia del ppoler
        TextoAnimacion texto = nuevoTextoGO.GetComponent<TextoAnimacion>(); //SE ESTABLECE EL TEXTO DEL DAÑO, REFERENCIA DE LA CLASE
        texto.EstablecerTexto(cantidad); //SE LE ESTABLECE EL NUMERO DE DAÑO
        nuevoTextoGO.transform.SetParent(canvasTextoPosicion); //SE EXTABLECE EL PAREN DEL TEXTO PARA QUE EL TEXTO SE MUEVA CON EL PERSONAJE
        nuevoTextoGO.transform.position = canvasTextoPosicion.position; //SE PASA LA POSICION DEL PERSONAJE
        nuevoTextoGO.SetActive(true);// SE ACTIVA A TRUE

        yield return new WaitForSeconds(1f);   //SE ESPERA UN SEGUNDO PARA DESACTIVAR
        nuevoTextoGO.SetActive(false); //SE DESACTIVA
        nuevoTextoGO.transform.SetParent(pooler.ListaContenedor.transform); //SE PONE EL PAREN OTRA VEZ A LA LISTA CONTENEDOR 
    }
    //METODO PARA PASAR EL DAÑO 
    private void RespuestaDañoRecibido(float daño)
    {
        StartCoroutine(IEMostrarTexto(daño));
    }
    
    private void OnEnable() //SE RESPONDE AL EVENTO
    {
        IA_Controller.EventoDañoRealizado += RespuestaDañoRecibido;
    }

    private void OnDisable() //SE RESPONDE AL EVENTO 
    {
        IA_Controller.EventoDañoRealizado -= RespuestaDañoRecibido;
    }
}

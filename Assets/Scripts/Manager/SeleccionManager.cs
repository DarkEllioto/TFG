using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SeleccionManager : MonoBehaviour
{
    //SE CREA LOS EVENTOS PARA PODER SELECCIONAR EL ENEMIGO 
    //AL EVENTO SELECCIONAR ENEMIGO SE LE PASA LA REFERENCIA DE ENEMIGO ITERACCION
    public static Action<EnemigoInteraccion> EventoEnemigoSeleccionado;
    public static Action EventoObjetoNoSeleccionado;


    //SE CREA LA PROPIEDAD DEL ENEMIGO SELECCIONADO  DE TIPO ENEMIGO ITERACCION
     public EnemigoInteraccion EnemigoSeleccionado { get; set; }

    //SE CREA LA VARIABLE DE TIPO DE LA CAMARA
      private Camera camara;


    // METODO START 
    void Start()
    {
        //SE OBTIENE LA REFERENCIA DE LA CAMARA PRINCIPAL DEL JUEGO
         camara = Camera.main;
    }

    //METODO UPDATE 
    void Update()
    {
        //SE LLAMA AL METODO SELECCIONAR ENEMIGO
        SeleccionarEnemigo();
    }

    //METODO SELECCIONAR ENEMIGO
     private void SeleccionarEnemigo()
    {
        //SE VERIFICA SI SE ESTA PULSANDO EL CLICK IZQUIERDO DEL MOUSE
        if (Input.GetMouseButtonDown(0))
        {
            //SE CREA LA VARIABLE  DE TIPO RAYCAST 
            //ESTE METODO PIDE LA DIRECCION DESDE DONDE SE LANZA Y HACIA DONDE SE LANZA 
            //SIENDO EL ORIGEN LA POSICION DEL MAUSE
            // LUEGO LA DISTANCIA QUE SE OBTIENE DE LA CLASE MATH INFINITY
            RaycastHit2D hit = Physics2D.Raycast(camara.ScreenToWorldPoint(Input.mousePosition),
                Vector2.zero, Mathf.Infinity, LayerMask.GetMask("Enemigo"));

            //SI SE HA COLICIONADO 
            if (hit.collider != null)
            {
                //SE LE OBTIENE EL COMPONENTE DEL ENEMIGO
                EnemigoSeleccionado = hit.collider.GetComponent<EnemigoInteraccion>();
                //SE INVOCA EL EVENTO Y SE LE PASA EL ENEMIGO SELECCIONADO
                EventoEnemigoSeleccionado?.Invoke(EnemigoSeleccionado);
            }
            else
            {
                //DE LO CONTRARIO SI NO SE HA COLICIONADA SE PASA EL EVENTO OBJETO NO SELECCIONADO
                EventoObjetoNoSeleccionado?.Invoke();
            }
        }
    }
}

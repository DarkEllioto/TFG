using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeDetector : MonoBehaviour
{
    //SE CRAN LOS EVENTOS
   public static Action<EnemigoInteraccion> EventoEnemigoDetectado;
    public static Action EventoEnemigoPerdido;

    //SE OBTIENE LA REFERENCIA DEL ENEMIGO 
    public EnemigoInteraccion EnemigoDetectado { get; private set; }
    
    //METODOS PARA SELECCIONAR EL PERSONAJE
    private void OnTriggerEnter2D(Collider2D other)
    {
        //SI EL OBJETO CON EL QUE SE COLICIONA TIENE EL TAG  DE ENEMIGO
        if (other.CompareTag("Enemigo"))
        {
            //SE OBTIENE LA REFERENCIA
            EnemigoDetectado = other.GetComponent<EnemigoInteraccion>();
            //SI EL ENEMIGO ESTA VIVO 
            if (EnemigoDetectado.GetComponent<EnemigoVida>().Vida > 0)
            {
                //SE LANZA EL EVENTO 
                EventoEnemigoDetectado?.Invoke(EnemigoDetectado);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //SI EL ENEMIGO SALIO DE LA DETENCION DEL PERSONAJE
        if (other.CompareTag("Enemigo"))
        {
            //SE INVOCA
            EventoEnemigoPerdido?.Invoke();
        }
    }
}

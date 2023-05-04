using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA_Enemigos/Decisiones/Detectar Personaje")]
public class DecisionDetectarPersonaje : IA_Decision  
//SE HEREDA DE LA CLASE IA_DECISION
{
  public override bool Decidir(IA_Controller controller) //SE IMPLEMENTA EL METODO  COMO EL METODO REGRESA UN BOOLEAN SE CREA OTRO METOOD
  {
    return DetectarPersonaje(controller); // SE LLAMA AL METODO 

  }

   private bool DetectarPersonaje(IA_Controller controller) //METODO QUE DEVUELVE EL BOOLEAN Y DONDE SE EJECUTA LA LOGICA
    {
        //PARA DETECTAR UN PERSONAJE UTILIZAMOS EL  METODO OVERLAPCIRCLE QUE NOS MUESTRA UN CIRCULO DE DETENCION 
        //se obtiene el radio del circulo con la variable de rango detencion
        Collider2D personajeDetectado = Physics2D.OverlapCircle(controller.transform.position,
            controller.RangoDeteccion, controller.PersonajeLayerMask);
        if (personajeDetectado != null)  //SE COMPRUEBA QUE SI SE DECTECTA AL PERSONAJE 
        {
            controller.PersonajeReferencia = personajeDetectado.transform; //SE REGRESA TRUE 
            return true;
        }
        //SI NO 
        controller.PersonajeReferencia = null;
        return false;// SE REGRESA FALSE
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA_Enemigos/Decisiones/Personaje En Rango")]
public class DecisionPersonajeRangoDeAtaque : IA_Decision
{
    public override bool Decidir(IA_Controller controller) //SE IMPLEMENTA EL METODO  COMO EL METODO REGRESA UN BOOLEAN SE CREA OTRO METOOD
  {
  
  return EnRangoDeAtaque(controller);

  }
  //SE CREA EL METODO ENRANGODE ATAQUE
  private bool EnRangoDeAtaque(IA_Controller controller) //SE PASA PARAMETRO A CONTROLLER
    {
        if (controller.PersonajeReferencia == null) //SI LA REFERENCIA DEL PERSONAJE ES FALNULLSA
        {
            return false; //DEVUELVE FALSE
        }

        //INDICA SI EL PERSONAJE ESTA EN RANGO DE ATAQUE 
        //ESTE METODO ES PARA TRANSICION DE ESTADOS
        float distancia = (controller.PersonajeReferencia.position - 
                           controller.transform.position).sqrMagnitude;
        if (distancia < Mathf.Pow(controller.RangoDeAtaqueDeterminado, 2))
        {
            return true;
        }

        return false;
    }
}

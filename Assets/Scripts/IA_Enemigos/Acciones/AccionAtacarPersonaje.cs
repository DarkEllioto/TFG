using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//CLASE PARA ATACAR EL PERSONAJE
[CreateAssetMenu(menuName = "IA_Enemigos/Acciones/Atacar Personaje")]
public class AccionAtacarPersonaje : IA_Accion
{
     public override void Ejecutar(IA_Controller controller) //SE IMPLEAMENTA EL METODO ABSTRATO
    {
     atacar(controller);
    }

//METODO DE ATAQUE
    private void atacar(IA_Controller controller)
    {
        // SI EL PERSONAJE REFERENCIA ES NULL 
        if (controller.PersonajeReferencia == null)
        {
            return; //REGRESAMOS
        }
        //SI EL TIEMPO DE ATAQUE ES FALSE 
        if (controller.EsTiempoDeAtacar() == false)
        {
            return; //REGRESAMOS
        }

         if (controller.PersonajeEnRangoDeAtaque(controller.RangoDeAtaqueDeterminado)) //SI EL PERSONAJE ESTA EN RANGO DE ATAQUE
         {

            if (controller.TipoAtaque == TiposDeAtaque.Embestida) //SE COMPRUEBA EL TIPO DE ATAQUE
            {
                controller.AtaqueEmbestida(controller.Daño);//SE LLAMA AL METODO ATAQUE EMBESTIDA
            }
            else
            {
                 controller.AtaqueMelee(controller.Daño); //SE LLAMA EL METODO ATACAR MELEE
            }
            
             controller.ActualizarTiempoEntreAtaques(); //SE LLAMA AL METODO ACTUALIZAR TIEMPO

         }


    }

   
}

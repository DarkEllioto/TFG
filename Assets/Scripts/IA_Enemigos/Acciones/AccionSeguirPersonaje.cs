
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA_Enemigos/Acciones/Seguir Personaje")]
public class AccionSeguirPersonaje : IA_Accion  
//SE HEREDA DE IA_ACCION
{ 
    public override void Ejecutar(IA_Controller controller) //SE IMPLEAMENTA EL METODO ABSTRATO
    {
      SeguirPersonaje(controller); //SE LLAMA AL METODO 
    }

     private void SeguirPersonaje(IA_Controller controller) // SE CREA EL METODO DE SEGUIR PERSONAJE 
    {
        if (controller.PersonajeReferencia == null) //SI NO SE TIENE LA REFERENCIA DEL PERSONAJE REGRESA
        {
            return;
        }

        //Comprobamos que el personaje no haya sido derrotado
        if (controller.PersonajeReferencia.GetComponent<PersonajeVida>().PersonajeKO)
        {
            return;
        }
        // SI SE DETECTA AL PERSONAJE SE OBTIENE LA POCISION 
        Vector3 dirHaciaPersonaje = controller.PersonajeReferencia.position - controller.transform.position;
        Vector3 direccion = dirHaciaPersonaje.normalized;
        float distancia = dirHaciaPersonaje.magnitude; //SE GUARDA LA DISTANCIA DONDE ESTA EL PERSONAJE

        if (distancia >= 1.30f) // SI LA DISTANCIA DEL PERSONAJE ES MAYOR A 1.30 SE SIGUE SI  NO SE DETIENE EL ENMIGO 
        {
            controller.transform.Translate(
                direccion * controller.VelocidadMovimiento * Time.deltaTime); 
        }
    }
 
}
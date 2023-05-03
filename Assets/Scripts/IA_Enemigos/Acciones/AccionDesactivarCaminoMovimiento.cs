using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "IA_Enemigos/Acciones/Desactivar Camino Movimiento")]
public class AccionDesactivarCaminoMovimiento : IA_Accion
{
    public override void Ejecutar(IA_Controller controller)
    {
        if (controller.EnemigoMovimiento == null) //SI NO SE TIENE LA REFERENCIA SE REGRESA
        {
            return;
        }

        controller.EnemigoMovimiento.enabled = false; //SI NO SE DESACTIVA 
    }
}
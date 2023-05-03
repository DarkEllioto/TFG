using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "IA_Enemigos/Estado")] //SE LE DA EL ATRIBUTO  CREATE ASSET MENU Y SE ESPECIFICA QUE SE ENCUENTRA EL FOLDER DE INTELIGENCIA ARTIFICIAL
public class IA_Estado : ScriptableObject
//CONTIENE TODAS LAS ACCIONES Y DECISIONES QUE UN ENEMIGO PUEDE EJECUTAR 
{
    //PARA TENER TODAS LAS ACCIONES EN EL ESTADO SE CREAN VARIABLES PUBLICAS DE TIPO ARRAY Y 
    //PARA LAS DECISIONES SE HACE LO MISMO PERO CON IA_TRANSICIONES YA QUE ESTA CLASE YA TIENE LAS DECISIONES 
    public IA_Accion[] Acciones;
    public IA_Transicion[] Transiciones;


    //METODO QUE EJECUTA LOS METODOS 
      public void EjecutarEstado(IA_Controller controller) // SE LE PASA UN PARAMETRO DE TIPO  IA_CONTROLLER
    {
        //SE LLAMAN LOS METODOS 
        EjecutarAcciones(controller);
        RealizarTransiciones(controller);
    }


    //PARA EJECUTAR TODAS LAS ACCIONES DEL ESTADO SE CREA UN METODO PRIVADO 

     private void EjecutarAcciones(IA_Controller controller) // SE LE PASA UN PARAMETRO DE TIPO  IA_CONTROLLER
    {
        // SE COMPRUEBA QUE EXISTAN ACCIONES 
        if (Acciones == null || Acciones.Length <= 0)
        {
            return;
        }
        
        //CICLO QUE RECORRE LAS ACCIONES 
        for (int i = 0; i < Acciones.Length; i++)
        {
            //SE RECORRE Y SE EJECUTA UNA A UNA 
            Acciones[i].Ejecutar(controller);
        }
    }

    //METRODO PARA EJECUTAR LAS TRANCICIONES 
    private void RealizarTransiciones(IA_Controller controller)// SE LE PASA UN PARAMETRO DE TIPO  IA_CONTROLLER
    {
        //SE COMPRUEBA QUE EXISTAN TRANCICIONES 
        if (Transiciones == null || Transiciones.Length <= 0)
        {
            return;
        }

        //SE RECORRE CON UN BUCLE FOR QUE RECORRA LAS TRANCICIONES DE ESTADOS 
        for (int i = 0; i < Transiciones.Length; i++)
        {
            //SE OBTIENE EL VALOR DE LA DECISION EN CADA TRANCISION  CON UNA VARIABLE DE TIPO BOOLEAN
            bool decisionValor = Transiciones[i].Decision.Decidir(controller);
            //SI ES VERDADERO SE HACE LA TRANCICION CON EL METODO CAMBIAR ESTADO SE PASA A ESTADO VERDADERO SI NO LO CONTRARIO AL ESTADOFALSO 
            if (decisionValor)
            {
                controller.CambiarEstado(Transiciones[i].EstadoVerdadero);
            }
            else
            {
                controller.CambiarEstado(Transiciones[i].EstadoFalso);
            }
        }
    }

    
}

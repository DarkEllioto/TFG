using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
    //Creamos un evento para notificar que la mision se ha completado
    public static Action<Quest> EventoQuestCompletado;
    //Creamos los campos de los campos principales de nuestras misiones
    [Header("Info")]
    public string Nombre;
    public string ID;
    public int cantidadObjetivo;

    //Creamos la descripcion de nuestras misiones

    [Header("Descripcion")]
    [TextArea] public string Descripcion;

    //Creamos las recompensas

    [Header("Recompensas")]
    public float RecompensaExp;
    public int RecompensaORO;
    public recompensaItem RecompensaItem;
    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool QuestCompletada;

    //Añadimos un metodo que nos permita añadir progreso a la mision
    public void añadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificaQuestCompletado();
    }

    //Creamos un metodo para verificar que se haya llegado al limite
    private void VerificaQuestCompletado()
    {
        //Comprobamos que hayamos llegado al limite
        if(CantidadActual >= cantidadObjetivo)
        {
            //Igualamos para que no supere el valor del objetivo
            CantidadActual = cantidadObjetivo;
            QuestCompletado();
        }
    }
    //Creamos un metodo para completar la quest
    private void QuestCompletado()
    {
        //Comprobamos que haya sido completada
        if (QuestCompletada)
        {
            return;
        }
        QuestCompletada = true;
        EventoQuestCompletado?.Invoke(this);
    }
    private void OnEnable()
    {
        QuestCompletada = false;
        CantidadActual = 0;
    }
}

//Creamos la recompensa de los objetos
[Serializable]
 public class recompensaItem
{
    public InventarioItem Item;
    public int Cantidad;
}
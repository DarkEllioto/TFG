using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Quest : ScriptableObject
{
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
}

//Creamos la recompensa de los objetos
[Serializable]
 public class recompensaItem
{
    public InventarioItem Item;
    public int Cantidad;
}
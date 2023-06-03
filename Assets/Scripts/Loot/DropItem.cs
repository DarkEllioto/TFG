using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class DropItem 
{
    //ES UNA CLASE QUE DEFINE QUE UN ENEMIGO DRAPEA UN ITEM
    
    //VARIABLES DE INFORMACION
     [Header("Info")]
    public string Nombre;
    public InventarioItem Item;
    public int Cantidad;
    
    //VARIABLE DE PORCENTAJE DE DROPEO CON UN ATRIBUTO DE RANGO DE 0 A  100
    [Header("Drop")]
    [Range(0, 100)]public float PorcentajeDrop;

    //PROPIEDADES DEL ITEM DROPEADO
    public bool ItemRecogido { get; set; }
}

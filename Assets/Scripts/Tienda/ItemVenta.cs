using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PARA QUE SE PUEDA VER LA CLASE EN EL INSPECTOR SE LE DA EL ATRIBUTO SERIALIZABLE
[Serializable]
public class ItemVenta
{
    //SE CRAN LAS VARIABLES PARA GUARDAR LOS DATOS
    public string Nombre; //NOMBRE
    public InventarioItem Item; //REFERENCIA
    public int Costo; //PRECIO
}

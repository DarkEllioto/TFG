using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Creamos su atributo de assets
[CreateAssetMenu(menuName ="Items/Pocion Mana")]
//Creamos la herencia desde inventarioitem
public class ItemPocionMana : InventarioItem
{
    //Creamos un header para la informacion
    [Header("Pocion info")]
    //Indicamos lo que podremos recuperar con esta pocion
    public float MP_restauracion;
}

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

    public override bool UsarItem()
    {
        //Llamamos a la clase PersonajeMana
        if (Inventario.Instance.Personaje.PersonajeMana.SePuedeRestaurar)
        {
            //Restauramos el mana con la pocion de mana
            Inventario.Instance.Personaje.PersonajeMana.RestaurarMana(MP_restauracion);
            return true;
        }

        return false;
    }
}

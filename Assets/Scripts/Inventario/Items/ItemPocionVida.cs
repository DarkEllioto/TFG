using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Creamos su atributo de assets
[CreateAssetMenu(menuName = "Items/Pocion Vida")]
//Creamos la herencia desde inventarioitem
public class ItemPocionVida : InventarioItem
{
    //Creamos un header para la informacion
    [Header("Pocion info")]
    //Indicamos lo que podremos recuperar con esta pocion
    public float HP_restauracion;

    public override bool UsarItem()
    {
        //Comprobamos que el personaje tenga vida para ser curado
        if (Inventario.Instance.Personaje.PersonajeVida.PuedeCurarse)
        {
            //Recuperamos vida
            Inventario.Instance.Personaje.PersonajeVida.RestaurarV(HP_restauracion);
            return true;
        }
        else
        {
            return false;
        }
    }
}

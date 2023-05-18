using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PARA QUE SE PUEDA CREAR EL ITEM
[CreateAssetMenu(menuName = "Items/Arma")]
public class ItemArma : InventarioItem
{
    //SE OBTIENE LA REFERENCIA DEL ARMA
    [Header("Arma")] 
    public Arma Arma;

   /* public override bool EquiparItem()
    {
        if (ContenedorArma.Instance.ArmaEquipada != null)
        {
            return false;
        }
        
        ContenedorArma.Instance.EquiparArma(this);
        return true;
    }

    public override bool RemoverItem()
    {
        if (ContenedorArma.Instance.ArmaEquipada == null)
        {
            return false;
        }
        
        ContenedorArma.Instance.RemoverArma();
        return true;
    }*/
}

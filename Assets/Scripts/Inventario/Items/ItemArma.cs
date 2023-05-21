using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "Items/Arma")]
public class ItemArma : InventarioItem
{
    //SE OBTIENE LA REFERENCIA DEL ARMA
    [Header("Arma")] 
    public Arma Arma;

    //SE SOBREESCRIBE EL METODO EQUIPAR ITEM, ESTA CLASE HEREDA DE INVENTARIOItem
    public override bool EquiparItem()
    {
        //SE COMPRUEBA SI EL CONTENEDOR ES DISTINTO DE NULO 
        if (ContenedorArma.Instance.ArmaEquipada != null)
        {
            //SI ESTA OCUPADO SE SALE NO DEJA SELECCIONAR OTRA ARMA 
            return false;
        }
        //SI POR EL CONTRARIO ESTA DESOCUPADA SE SELECCIONA EL ARMA 
        ContenedorArma.Instance.EquiparArma(this);
        return true;
    }
 //SE SOBREESCRIBE EL METODO REMOVER ITEM
    public override bool RemoverItem()
    {
        //SI EL ARMA NO ESTA EQUIPADA NO SE PUEDE REMOVER 
        if (ContenedorArma.Instance.ArmaEquipada == null)
        {
            //RETORNA FALSE
            return false;
        }
        //DE LO CONTRARIO SE REMUEVE EL ARMA DEL CONTENEDOR
        ContenedorArma.Instance.RemoverArma();
        return true;
    }

    
}

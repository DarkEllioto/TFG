using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    //Variables
   [SerializeField] private Image itemIcono;
     [SerializeField] private TextMeshProUGUI itemNombre;
    //PROPIEDAD DE ITEM POR RECOGER
     public DropItem ItemPorRecoger { get; set; }
    //METODO DE CONFIGURAR UN ITEM CON UN PARAMETRO DE TIPO DROP ITEM 
     public void ConfigurarLootItem(DropItem dropItem)
    {
        //ESTE METODO PERMITE ACTUALIZAR ICOO Y NOMBRE DEL ITEM 
        //SE CONFIGURA LA INFORMACION DEL ITEM
        ItemPorRecoger = dropItem;
        itemIcono.sprite = dropItem.Item.Icono;
        itemNombre.text = $"{dropItem.Item.Nombre} x{dropItem.Cantidad}";
    }

    //Metodo para recoger item
     public void RecogerItem()
    {
        //SE COMPRUEBA SI EL ITEM ESTA VACIO
        if (ItemPorRecoger == null)
        {
            //SE REGRESA
            return;
        }
        //SI NO SE RECOGE EN EL INVENTARIO
        Inventario.Instance.AñadirItem(ItemPorRecoger.Item, ItemPorRecoger.Cantidad);
        //SE PASA A TRUE 
        ItemPorRecoger.ItemRecogido = true;
        //SE ELEMINA ESE ITEM
        Destroy(gameObject);
    }

}

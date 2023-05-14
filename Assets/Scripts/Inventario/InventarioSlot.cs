using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class InventarioSlot : MonoBehaviour
{
    //Creamos los campos para referenciar nuestro icono,cantidad e imagen del inventario
    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;
    //Creamos la propiedad que nos permita saber en que slot estamos para poder usarlo o borrarlo
    public int Index { get; set; }
   
    //Creamos el metodo para actualizar el slot
    public void ActualizarSlot(InventarioItem item, int cantidad)
    {
        //Asignamos el icono
        itemIcono.sprite = item.Icono;
        //Asignamos la cantidad
        cantidadTMP.text = cantidad.ToString();
    }
    //Creamos el metodo para actualizar la UI
    public void ActivarSlotUI(bool estado)
    {
        itemIcono.gameObject.SetActive(estado);
        fondoCantidad.SetActive(estado);
    }
}

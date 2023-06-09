using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public enum TipoDeInteraccion
{
    Click,
    Usar,
    Equipar,
    Remover
}
public class InventarioSlot : MonoBehaviour
{
    //Creamos un evento para cuando usamos los objetos
    public static Action<TipoDeInteraccion, int> EventoSlotInteraccion;
    //Creamos los campos para referenciar nuestro icono,cantidad e imagen del inventario
    [SerializeField] private Image itemIcono;
    [SerializeField] private GameObject fondoCantidad;
    [SerializeField] private TextMeshProUGUI cantidadTMP;
    //Creamos la propiedad que nos permita saber en que slot estamos para poder usarlo o borrarlo
    public int Index { get; set; }
    private Button _button;
    private void Awake()
    {
        _button = GetComponent<Button>(); 
    }
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
    //Creamos el metodo para volver a seleccionar el slot
    public void SeleccionarSlot()
    {
        _button.Select();
    }
    //Creamos un metodo para usar el evento 
    public void ClickSlot()
    {
        EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Click,Index);
    }
    //Creamos un metodo para usar el item
    public void SlotUsarItem()
    {
        //Comprobamos que haya un item en el slot para poder lanzar el evento
        if(Inventario.Instance.ItemsInventario[Index] != null)
        {
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Usar, Index);
        }
    }
}

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

        //Mover Item
        //Comprobamos que haya un slot seleccionado
        if (InventarioUI.Instance.IndexSlotPorMover != -1)
        {
            if(InventarioUI.Instance.IndexSlotPorMover != Index)
            {
                //Moveremos el item
                Inventario.Instance.MoverItem(InventarioUI.Instance.IndexSlotPorMover, Index);
            }
        }
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
    //SE CREA EL METODO SLOCK EQUIPAR ITEM
    public void SlotEquiparItem()
    {
        //SE COMPRUEBA SI EL SLOT TIENE ITEM SE LANZA EL EVENTO
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            //SE INVOCA Y SE LE PASA EL TIPO DE ITERACION Y EL INDEX
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Equipar, Index);
        }
    }
//METODO PARA REMOVER EL ITEM DEL CONTENEDOR
     public void SlotRemoverItem()
    {
        
        //SE COMPRUEBA SI EL SLOT TIENE ITEM SE LANZA EL EVENTO
        if (Inventario.Instance.ItemsInventario[Index] != null)
        {
            
            //SE INVOCA Y SE LE PASA EL TIPO DE ITERACION QUE SERA REMOVER Y EL INDEX
            EventoSlotInteraccion?.Invoke(TipoDeInteraccion.Remover, Index);
        }
    }
}

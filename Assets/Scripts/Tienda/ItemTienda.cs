using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//CLASE DE ITEM TIENDA
public class ItemTienda : MonoBehaviour
{
    [Header("Config")] 
    //SE CREAN LAS VARIABLES DE CONFIGURACION
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemCosto;
    [SerializeField] private TextMeshProUGUI cantidadPorComprar;

    //SE CREA UNA PROPIEDAD PARA GUARDAR LOS DATOS DEE ITEM CARGADO
    public ItemVenta ItemCargado { get; private set; }

    //VARIABLES PARA CONTROLAR EL PRECIO Y CANTIDAD DEL ITEM 
    private int cantidad;
    private int costoInicial;
    private int costoActual;

    private void Update()
    {
        cantidadPorComprar.text = cantidad.ToString();
        itemCosto.text = costoActual.ToString();
    }
    //METODO PARA ACTUALIZAR LAR REFERENCIAS
    public void ConfigurarItemVenta(ItemVenta itemVenta) //SE PASA POR PARAMETRO DE ITEMVENTE
    {
        //SE MUESTRAN LOS DATOS DEL ITEM CARGADO PARA ACTUALIZARLOS 
        ItemCargado = itemVenta;
        itemIcono.sprite = itemVenta.Item.Icono;
        itemNombre.text = itemVenta.Item.Nombre;
        itemCosto.text = itemVenta.Costo.ToString();

        //SE DEJA EL LA CANTIDAD POR DEFAULT EN 1 
        cantidad = 1;
        costoInicial = itemVenta.Costo;
        costoActual = itemVenta.Costo;
    }

//METODO PARA COMPRAR 
    public void ComprarItem()
    {
        //SE CALCULA QUE LAS MONEDAS SEAN SUFICIENTES PARA COMPRAR
        if (MonedasManager.Instance.MonedasTotales >= costoActual)
        {
            //SI ES ASI SE COMPRA Y SE AÑADE AL INVENTARIO
            Inventario.Instance.AñadirItem(ItemCargado.Item, cantidad);
            //SE ACTUALIZAN LAS MONEDAS
            MonedasManager.Instance.RemoverMonedas(costoActual);
            //SE RESTABLECE EL ITEM
            cantidad = 1;
            costoActual = costoInicial;
        }
    }
    
    //METODO AUMENTAR CANTIDAD DE ITEM 
    public void SumarItemPorComprar()
    {
        //SE EJECUTA LA ECUACION PARA MOSTRAR PARA CALCULAR EL COSTO
        int costoDeCompra = costoInicial * (cantidad + 1);
        //SE COMPRUEBA QUE SE LA CANTIDAD DE MONEDAS SEA MENOR QUE EL PRECIO DE LA COMPRA
        if (MonedasManager.Instance.MonedasTotales >= costoDeCompra)
        {
            //I ES ASI SE AUMENTA LA CANTIDAD
            cantidad++;
            //SE CALCULA EL PRECIO FINAL
            costoActual = costoInicial * cantidad;
        }
    }

    //METODO DISMINUIR CANTIDAD
    public void RestarItemPorComprar()
    {
        //SE COMPRUEBA QUE LA CANTIDAD SEA MAYOR A 1
        if (cantidad == 1)
        {
            return;
        }
        //SI ES MEYAOR A 1 SE DISMINUYE 
        cantidad--;
        //Y SE CALCULA EL PRECIO
        costoActual = costoInicial * cantidad;
    }
}

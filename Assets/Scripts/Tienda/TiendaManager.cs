using System;
using UnityEngine;

//CLASE TIENDA MANAGER
public class TiendaManager : MonoBehaviour
{
    //SE CREAN LAS VARIABLES DE CONFIGURACION //VARIABLE DE REFERNCIA DEL ITEM
    [Header("Config")] 
    [SerializeField] private ItemTienda itemTiendaPrefab;
    [SerializeField] private Transform panelContenedor;


    [Header("Items")]
    //ARRAY CON TODOS LOS ITEM DISPONIBLES 
    [SerializeField] private ItemVenta[] itemsDisponibles;

    //METODO PARA INICIAR
    private void Start()
    {
        CargarItemEnVenta(); //SE LLAMA AL METODO
    }

    //METODO PARA CARGAR EL ITEM 
    private void CargarItemEnVenta()
    {
        //CON UN BUCLE FOR SE RECORRE LOS ITEM 
        for (int i = 0; i < itemsDisponibles.Length; i++)
        {
            //SE AÑADE LOS METODOS EN EL CONTENEDOR
            ItemTienda itemTienda = Instantiate(itemTiendaPrefab, panelContenedor);
            itemTienda.ConfigurarItemVenta(itemsDisponibles[i]);
        }
    }
}
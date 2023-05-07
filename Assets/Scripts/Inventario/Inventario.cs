using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    //Creamos la variable para obtener el total de slots de nuestro inventario

    [SerializeField] private int numeroDeSlots;

    //Creamos la propiedad que nos permita obtener la referencia desde otras clases

    public int NumeroDeSlots => numeroDeSlots;

    //Creamos un array para guardar los items de nuestro inventario
    [Header("Items")]
    [SerializeField] private InventarioItem[] itemsInventario;

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }
}

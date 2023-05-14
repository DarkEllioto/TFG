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
    //Creamos el metodo que nos permita a�adir un item
    public void A�adirItem(InventarioItem itemPorA�adir,int cantidad)
    {
        if(itemPorA�adir == null)
        {
            return;
        }
        //Hacemos la verificacion en caso de tener un item en el inventario ya llamando a la funcion
        List<int> indexes = VerificarExistencias(itemPorA�adir.ID);
        if (itemPorA�adir.esAcumulable)
        {
            if(indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    //Comprobamos que no hayamos superado la cantidad maxima por slot
                    if (itemsInventario[indexes[i]].cantidad < itemPorA�adir.AcumulacionMax)
                    {
                        //a�adimos la cantidad al slot
                        itemsInventario[indexes[i]].cantidad += cantidad;
                        //creamos una nueva cantidad en caso de superar el maximo del slot
                        if (itemsInventario[indexes[i]].cantidad > itemPorA�adir.AcumulacionMax)
                        {
                            //Creamos la diferencia para a�adirlo al nuevo slot
                            int diferencia = itemsInventario[indexes[i]].cantidad - itemPorA�adir.AcumulacionMax;
                            itemsInventario[indexes[i]].cantidad = itemPorA�adir.AcumulacionMax;
                            //Volvemos a llamar al metodo usando la recursividad para a�adir las cantidades
                            A�adirItem(itemPorA�adir, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemPorA�adir,
                            itemsInventario[indexes[i]].cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        //A�adimos de manera nueva el slot
        if(cantidad <= 0)
        {
            return;
        }

        if(cantidad > itemPorA�adir.AcumulacionMax)
        {
            A�adirItemEnSlotDisponible(itemPorA�adir, itemPorA�adir.AcumulacionMax);
            cantidad -= itemPorA�adir.AcumulacionMax;
            A�adirItem(itemPorA�adir, cantidad);
        }
        else
        {
            A�adirItemEnSlotDisponible(itemPorA�adir, cantidad);
        }

    }
    //Creamos el metodo para verificar que el item se encuentre o no en el inventario
    private List<int> VerificarExistencias(string itemId)
    {
        //Creamos una lista con los indexes de nuestro inventario
        List<int> indexesDelItem = new List<int>();
        //Recorremos cada item del inventario
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if(itemsInventario[i] != null)
            {
                //Comparamos con cada id para cercionarnos de que esta en el inventario
                if (itemsInventario[i].ID == itemId)
                {
                    //Si el item coincide con algun id de nuestro index entonces le a�adimos la cantidad
                    indexesDelItem.Add(i);
                }
            }
            
            
        }
        return indexesDelItem;
    }
    
    private void A�adirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        //Buscamos el slot vacio
        for (int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].cantidad = cantidad;
                InventarioUI.Instance.DibujarItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }
}

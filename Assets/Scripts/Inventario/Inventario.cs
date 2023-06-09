using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")]
    //Creamos la variable para obtener el total de slots de nuestro inventario
    [SerializeField] private int numeroDeSlots;
    //Creamos un array para guardar los items de nuestro inventario
    [SerializeField] private InventarioItem[] itemsInventario;
    //Creamos una referencia a nuestro personaje
    [SerializeField] private Personaje personaje;

    //Creamos las propiedades que nos permita obtener la referencia desde otras clases
    public int NumeroDeSlots => numeroDeSlots;
    public InventarioItem[] ItemsInventario => itemsInventario;
    public Personaje Personaje => personaje;


    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
    }
    //Creamos el metodo que nos permita aņadir un item
    public void AņadirItem(InventarioItem itemPorAņadir,int cantidad)
    {
        if(itemPorAņadir == null)
        {
            return;
        }
        //Hacemos la verificacion en caso de tener un item en el inventario ya llamando a la funcion
        List<int> indexes = VerificarExistencias(itemPorAņadir.ID);
        if (itemPorAņadir.esAcumulable)
        {
            if(indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    //Comprobamos que no hayamos superado la cantidad maxima por slot
                    if (itemsInventario[indexes[i]].cantidad < itemPorAņadir.AcumulacionMax)
                    {
                        //aņadimos la cantidad al slot
                        itemsInventario[indexes[i]].cantidad += cantidad;
                        //creamos una nueva cantidad en caso de superar el maximo del slot
                        if (itemsInventario[indexes[i]].cantidad > itemPorAņadir.AcumulacionMax)
                        {
                            //Creamos la diferencia para aņadirlo al nuevo slot
                            int diferencia = itemsInventario[indexes[i]].cantidad - itemPorAņadir.AcumulacionMax;
                            itemsInventario[indexes[i]].cantidad = itemPorAņadir.AcumulacionMax;
                            //Volvemos a llamar al metodo usando la recursividad para aņadir las cantidades
                            AņadirItem(itemPorAņadir, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAņadir,
                            itemsInventario[indexes[i]].cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        //Aņadimos de manera nueva el slot
        if(cantidad <= 0)
        {
            return;
        }

        if(cantidad > itemPorAņadir.AcumulacionMax)
        {
            AņadirItemEnSlotDisponible(itemPorAņadir, itemPorAņadir.AcumulacionMax);
            cantidad -= itemPorAņadir.AcumulacionMax;
            AņadirItem(itemPorAņadir, cantidad);
        }
        else
        {
            AņadirItemEnSlotDisponible(itemPorAņadir, cantidad);
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
                    //Si el item coincide con algun id de nuestro index entonces le aņadimos la cantidad
                    indexesDelItem.Add(i);
                }
            }
            
            
        }
        return indexesDelItem;
    }
    
    private void AņadirItemEnSlotDisponible(InventarioItem item, int cantidad)
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
    //Creamos el metodo para eliminar el item o reducir la cantidad en caso de usarlo
    private void EliminarItem(int index)
    {
        itemsInventario[index].cantidad--;
        //Verificamos que la cantidad no queda negativa
        if(itemsInventario[index].cantidad <= 0)
        {
            itemsInventario[index].cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.DibujarItemEnInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[index], itemsInventario[index].cantidad, index);
        }
    }
    //Creamos el metodo para usar el item
    private void UsarItem(int index)
    {
        //Verificamos que el item existe en el slot
        if(ItemsInventario[index] == null)
        {
            return;
        }
        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }
    }
    #region Eventos
    //Creamos los activables para el boton usar
    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        //Creamos un switch con los tipos de interaccion posibles
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                break;
            case TipoDeInteraccion.Remover:
                break;
        }
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }
    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using BayatGames.SaveGameFree;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [Header("Items")]
    //REFERENCIA AL ALMACEN
     [SerializeField] private InventarioAlmacen inventarioAlmacen;
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

    private readonly string INVENTARIO_KEY = "MiJuegoMiInventario105205120";

    private void Start()
    {
        itemsInventario = new InventarioItem[numeroDeSlots];
        CargarInventario();
    }
    //Creamos el metodo que nos permita añadir un item
    public void AñadirItem(InventarioItem itemPorAñadir,int cantidad)
    {
        if(itemPorAñadir == null)
        {
            return;
        }
        //Hacemos la verificacion en caso de tener un item en el inventario ya llamando a la funcion
        List<int> indexes = VerificarExistencias(itemPorAñadir.ID);
        if (itemPorAñadir.esAcumulable)
        {
            if(indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    //Comprobamos que no hayamos superado la cantidad maxima por slot
                    if (itemsInventario[indexes[i]].cantidad < itemPorAñadir.AcumulacionMax)
                    {
                        //añadimos la cantidad al slot
                        itemsInventario[indexes[i]].cantidad += cantidad;
                        //creamos una nueva cantidad en caso de superar el maximo del slot
                        if (itemsInventario[indexes[i]].cantidad > itemPorAñadir.AcumulacionMax)
                        {
                            //Creamos la diferencia para a�adirlo al nuevo slot
                            int diferencia = itemsInventario[indexes[i]].cantidad - itemPorAñadir.AcumulacionMax;
                            itemsInventario[indexes[i]].cantidad = itemPorAñadir.AcumulacionMax;
                            //Volvemos a llamar al metodo usando la recursividad para a�adir las cantidades
                            AñadirItem(itemPorAñadir, diferencia);
                        }

                        InventarioUI.Instance.DibujarItemEnInventario(itemPorAñadir,
                            itemsInventario[indexes[i]].cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }

        //Añadimos de manera nueva el slot
        if(cantidad <= 0)
        {
            return;
        }

        if(cantidad > itemPorAñadir.AcumulacionMax)
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, itemPorAñadir.AcumulacionMax);
            cantidad -= itemPorAñadir.AcumulacionMax;
            AñadirItem(itemPorAñadir, cantidad);
        }
        else
        {
            AñadirItemEnSlotDisponible(itemPorAñadir, cantidad);
        }
        //SE LLAMA AL METODO GUARDAR INVENTARIO
        GuardarInventario();

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
                    //Si el item coincide con algun id de nuestro index entonces le añadimos la cantidad
                    indexesDelItem.Add(i);
                }
            }
            
            
        }
        return indexesDelItem;
    }
    
    private void AñadirItemEnSlotDisponible(InventarioItem item, int cantidad)
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
        GuardarInventario();
    }
    //Creamos el metodo para usar el item
    private void UsarItem(int index)
    {
        //Verificamos que el item existe en el slot
        if(ItemsInventario[index] == null)
        {
            return;
        }
        if (itemsInventario[index].esConsumible == false) 
        {
            return;
        }
        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }
    }

     //METODO EQUIPAR ITEM SE PASA PARAMETRO ENTERO
     private void EquiparItem(int index)
    {
        //SE VERIFICA SI EXISTE EL ITEM
        if (itemsInventario[index] == null)
        {
            //SI NO EXISTE SE REGRES
            return;
        }
        //SE VERIFICA SI ES UNA ARMA
        if (itemsInventario[index].Tipo != tiposDeItems.Armas)
        
        {
            //SI NO ES ARMA SE REGRESA
            return;
        }
        //DE LO CONTRARIO SE EQUIPA
        itemsInventario[index].EquiparItem();
    }

    //METODO PARA REMOVER ITEM  SE PASA LA REFERENCIA INDEX
     private void RemoverItem(int index) 
    {
        //SE VERIFICA SI ESTA VACIO
        if (itemsInventario[index] == null)
        {
            //SE REGRESA
            return;
        }
        //SI EL ITEM NO ES DE ARMA 
        if (itemsInventario[index].Tipo != tiposDeItems.Armas)
        {
            //SE REGRESA
            return;
        }
        //DE LO CONTRARIO SI NINGUNA DE LAS CONDICIONES ANTERIORES SE CUMPLE SE LLAMA AL METODO REMOVER ITEM
        itemsInventario[index].RemoverItem();
    }



     #region Guardado

    //METODO PARA INVENTARIO
    private InventarioItem ItemExisteEnAlmacen(string ID)
    {
        //SE RECORRE EL INVENTARIO
        for (int i = 0; i < inventarioAlmacen.Items.Length; i++)
        {
            //SE COMPRUEBA QUE EL ID ES IGUAL A ALGUNO 
            if (inventarioAlmacen.Items[i].ID == ID)
            {
                //SE REGRESA LA REFERENCIA
                return inventarioAlmacen.Items[i];
            }
        }

        return null;
    }
    
    //SE CREA UNA REFERENCIA DE LA CLASE INVENTARIODATA
    private InventarioData dataGuardado;
    //METODO GUARDAR INVENTARIO
    private void GuardarInventario()
    {
        //SE INICIALIZA LA REFERENCIA EN UN NUEVO INVENTARIO 
        dataGuardado = new InventarioData();
        //SE INICIALIZA EL TAMAÑO DE LOS ARRAY
        dataGuardado.ItemsDatos = new string[numeroDeSlots];
        dataGuardado.ItemsCantidad = new int[numeroDeSlots];

        //SE RECORRE EL NUMERO DE SLOTS 
        for (int i = 0; i < numeroDeSlots; i++)
        {
            //SE COMPRUEBA SI LOS INDEX DEL INVENTARIO SON NULOS 
            if (itemsInventario[i] == null || string.IsNullOrEmpty(itemsInventario[i].ID))
            {
                //SE IGUAL A NULOS Y A 0 
                dataGuardado.ItemsDatos[i] = null;
                dataGuardado.ItemsCantidad[i] = 0;
            }
            else //SI POR EL CONTRARIO SI HAY ALGO EN EL 
            {
                //SE GUARDA EL ID Y LA CANTIDAD DEL ITEM CORRESPONDIENTE
                dataGuardado.ItemsDatos[i] = itemsInventario[i].ID;
                dataGuardado.ItemsCantidad[i] = itemsInventario[i].cantidad;
            }
        }
        
        //FINALMENTE SE GUARDA LA INFORMACION EN EL SAVEGame
        SaveGame.Save(INVENTARIO_KEY, dataGuardado);
    }

    private InventarioData dataCargado;
    //METODO Cargar INVENTARIO
    private void CargarInventario()
    {
        //SE COMPRUEBA QUE LA LLAVE TENGA UN VALOR ASOCIADO
        if (SaveGame.Exists(INVENTARIO_KEY))
        {
            //SE CARGA LA INFORMACION EN LA REFERENCIA 
            dataCargado = SaveGame.Load<InventarioData>(INVENTARIO_KEY);
            //SE RECORRE EL SLOT
            for (int i = 0; i < numeroDeSlots; i++)
            {
                //SE COMPRUEBA QUE EL ITEMS SEA DISTINTO DE NULL
                if (dataCargado.ItemsDatos[i] != null)
                {
                    //SE AÑADE LA INFORMACION AL DE ITEM DEL ALMACEN
                    InventarioItem itemAlmacen = ItemExisteEnAlmacen(dataCargado.ItemsDatos[i]);
                    //SE COMPRUEBA QUE NO ESTE VACIO
                    if (itemAlmacen != null)
                    {
                        //SE CARGAN LOS ITEM
                        itemsInventario[i] = itemAlmacen.CopiarItem();
                        itemsInventario[i].cantidad = dataCargado.ItemsCantidad[i];
                        InventarioUI.Instance.DibujarItemEnInventario(itemsInventario[i], 
                            itemsInventario[i].cantidad, i);
                    }
                }
                else //SI ES NULL REGRESA NULL 
                {
                    itemsInventario[i] = null;
                }
            }
        }
    }
    
    #endregion
    




    //Creamos el metodo para mover item
    public void MoverItem(int indexInicial, int indexFinal)
    {
        if(itemsInventario[indexInicial] == null || itemsInventario[indexFinal] != null)
        {
            return;
        }
        //Creamos la logica para mover el item

        //Copiamos el item en el slot final

        InventarioItem itemPorMover = itemsInventario[indexInicial].CopiarItem();
        itemsInventario[indexFinal] = itemPorMover;
        InventarioUI.Instance.DibujarItemEnInventario(itemPorMover, itemPorMover.cantidad, indexFinal);

        //Borramos el item del slot inicial

        itemsInventario[indexInicial] = null;
        InventarioUI.Instance.DibujarItemEnInventario(null,0,indexInicial);

        GuardarInventario();
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
                EquiparItem(index);
                break;
            case TipoDeInteraccion.Remover:
                RemoverItem(index);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : Singleton<InventarioUI>
{
    //Creamos los campos para los slots de nuestro inventario
    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    //Creamos una lista para poder guardar todos los slots creados y saber cuales estan libres
    List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();
    // Start is called before the first frame update
    void Start()
    {
        InicializarInventario();
    }

    //Inicializamos el inventario
    private void InicializarInventario()
    {
        //Usamos un for para instanciar el slot ya prefabricador tantas veces como numero de slots tengamos
        for (int i = 0; i < Inventario.Instance.NumeroDeSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            //Añadimos al index de inventario slot la propiedas de cada uno
            nuevoSlot.Index = i;
            //Añadimos el nuevo slot disponible
            slotsDisponibles.Add(nuevoSlot);
        }
    }

    //Creamos el metodo que nos permita poner imagenes en el inventario
    public void DibujarItemEnInventario(InventarioItem itemPorAñadir,int cantidad, int itemIndex)
    {
        //obtenemos referencia del slot
        InventarioSlot slot = slotsDisponibles[itemIndex];
        //Comprobamos que el item no sea null
        if(itemPorAñadir != null)
        {
            //Activamos el slot
            slot.ActivarSlotUI(estado: true);
            slot.ActualizarSlot(itemPorAñadir, cantidad);
        }
        else
        {
            //Desactivamos el slot
            slot.ActivarSlotUI(estado: false);
          
        }
    }
   
}

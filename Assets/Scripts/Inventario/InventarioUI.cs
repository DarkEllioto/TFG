using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
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
   
}

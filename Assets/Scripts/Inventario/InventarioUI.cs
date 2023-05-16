using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.EventSystems;

public class InventarioUI : Singleton<InventarioUI>
{
    //Creamos un header para el panel inventario
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInventarioDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;
    //Creamos los campos para los slots de nuestro inventario
    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    //Creamos una lista para poder guardar todos los slots creados y saber cuales estan libres
    public InventarioSlot SlotSeleccionado { get;private set; }
    List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();

    //Creamos una propiedad para poder mover los objetos
    public int IndexSlotPorMover { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        InicializarInventario();
        IndexSlotPorMover = -1;
    }
    private void Update()
    {
        ActualizarSlotSeleccionado();
        //Verificamos que estemos moviendo la tecla para mover
        if (Input.GetKeyDown(KeyCode.M))
        {
            //Comprobamos si hay un slot seleccionado
            if(SlotSeleccionado != null)
            {
                IndexSlotPorMover = SlotSeleccionado.Index;
            }
        }
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
    //Creamos un metodo para actualizar el slot seleccionado
    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
       //Comprobamos que el slot seleccionado no esta vacio
        if(goSeleccionado == null)
        {
            return;
        }
        //Guardamos el slot en una variable para pasarselo a la variable slotseleccionado
        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if(slot != null)
        {
            SlotSeleccionado = slot;
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
    //Creamos el metodo para actualizar la descripcion
    private void ActualizarInventarioDescripcion(int index)
    {
        //Comprobamos que no sea null
        if (Inventario.Instance.ItemsInventario[index] != null)
        {
            //Referenciamos imagen, nombre del objeto y descripcion
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            //Activamos el panel que por defecto estara desactivado
            panelInventarioDescripcion.SetActive(true);
        }
        else
        {
            //Desctivamos el panel que por defecto estara desactivado
            panelInventarioDescripcion.SetActive(false);
        }
    }
    //Creamos un nuevo metodo que permita llamar al metodo para usar el item
    public void UsarItem()
    {
        if (SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }

    
    #region Evento
    //Creamos la igualdad para el evento

    private void SlotInteraccionRespuesta(TipoDeInteraccion tipo, int index)
    {
        //Nos aseguramos de estar llamando a un evento de tipo click
        if (tipo == TipoDeInteraccion.Click)
        {
            //Referenciamos los gameobject
            ActualizarInventarioDescripcion(index);
        }
    }

    //Añadimos los metodos para habilitad y deshabilitar el evento

    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionRespuesta;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionRespuesta;
    }

    #endregion
    
   

}

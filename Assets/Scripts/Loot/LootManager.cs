using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    //SE CREA LA CLASE Singleton PARA PODER LLAMAR METODOS SE OTRA CLASE
   //VARIABLES
   [Header("Config")]
    [SerializeField] private GameObject panelLoot;
    [SerializeField] private LootButton lootButtonPrefab;
    [SerializeField] private Transform lootContenedor;

    //SE CREA EL METODO MOSTRAR LOOP CON UN PARAMETRO DE TIPO ENEMIGO LOOP
     public void MostrarLoot(EnemigoLoot enemigoLoot)
    {
        panelLoot.SetActive(true);
        //SE COMPRUEBA SI EL CONTENEDOR ESTA OCUPADO 
       if (ContenedorOcupado())
        {
            //SE RECORRE CON UN FORECH
            foreach (Transform hijo in lootContenedor.transform)
            {
                //Y SE DESTRUYE EL HIJO
                Destroy(hijo.gameObject);
            }
        }
        //CON UN CICLO FOR SE RECORRE LA LISTA DEL LOOP QUE FUE CARGADO
        for (int i = 0; i < enemigoLoot.LootSeleccionado.Count; i++)
        {
            //SE LLAMA EL METODO POR CADA RECORRIDO
            CargarLootPanel(enemigoLoot.LootSeleccionado[i]);
        }
    }
    //METODO PARA CERRAR EL PANEL
       public void CerrarPanel()
    {
        //SE PASA EL PANEL LOOT A FALSE
        panelLoot.SetActive(false);
    }

    //METODO PARA CARGAR LOOT AL PANEL CON UN PARAMETRO DE TIPO DROP ITEM
    private void CargarLootPanel(DropItem dropItem)
    {
        //SE CONPRUEBA SI EL LOOT YA FUE RECOGIDO SE REGRESA
        if (dropItem.ItemRecogido)
        {
            return;
        }
        //SI NO ESTA RECOGIDO 
        //SE INSTANCIA EL PREFAB
        LootButton loot = Instantiate(lootButtonPrefab, lootContenedor);
        //SE ACCEDE A LOOP Y SE LLAMA AL METODO CONFIGURAR
        loot.ConfigurarLootItem(dropItem);
        //SE ACTUALIZA AL CONTENEDOR
        loot.transform.SetParent(lootContenedor);
    }

    //metodo para comprobar los hijos de un contenedor
    private bool ContenedorOcupado()
    {
        //SE CREA UN ARRAY PARA OBTENER LOS COMPONENTES
        LootButton[] hijos = lootContenedor.GetComponentsInChildren<LootButton>();
        //SE COMPRUEBA SI TIENE HIJOS
        if (hijos.Length > 0)
        {
            //REGRESA QUE VERDADERO 
            return true;
        }
        //SI NO FALSE
        return false;
    }

}

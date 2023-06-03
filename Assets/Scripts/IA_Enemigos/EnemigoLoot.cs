using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemigoLoot : MonoBehaviour
{


    //variables
      [Header("Exp")] 
    [SerializeField] private float expGanada;


    [Header("Loot")] 
    [SerializeField] private DropItem[] lootDisponible;
    //un array de loop disponibles

        private List<DropItem> lootSeleccionado = new List<DropItem>();
        public List<DropItem> LootSeleccionado => lootSeleccionado;
        public float ExpGanada => expGanada;


    //METODO DE INICIAR
    private void Start()
    {
        //LLAMA AL METODO SELECCIONAR LOOT
        SeleccionarLoot();
    }
    //METODO DE SELECCION LOOT
    private void SeleccionarLoot()
    {
        //SE RECORRE CON UN FOREACH
        foreach (DropItem item in lootDisponible)
        {
            //SE CREA LA PROBAVILIDAD DEL ITEM DE APARECER
            float probabilidad = Random.Range(0, 100);
            //SI LA PROPIEDAD PUEDE APARECER
            if (probabilidad <= item.PorcentajeDrop)
            {
                //SE AÑADE
                lootSeleccionado.Add(item);
            }
        }
    }
 }

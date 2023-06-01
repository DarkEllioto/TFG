using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    //SE CREA LA CLASE Singleton PARA PODER LLAMAR METODOS SE OTRA CLASE
   //VARIABLES
   [Header("Config")]
    [SerializeField] private GameObject panelLoot;

    //SE CREA EL METODO MOSTRAR LOOP CON UN PARAMETRO DE TIPO ENEMIGO LOOP
     public void MostrarLoot()
    {
        panelLoot.SetActive(true);
      /*  if (ContenedorOcupado())
        {
            foreach (Transform hijo in lootContenedor.transform)
            {
                Destroy(hijo.gameObject);
            }
        }

        for (int i = 0; i < enemigoLoot.LootSeleccionado.Count; i++)
        {
            CargarLootPanel(enemigoLoot.LootSeleccionado[i]);
        }*/
    }
}

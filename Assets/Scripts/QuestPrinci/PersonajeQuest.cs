using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
//Heredamos de la clase principal
public class PersonajeQuest : QuestDescripcion
{
    //Obtenemos las referencias del oro y experiencia
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;
    //Obtenemos las referencias del sprite que se va a actualizar
    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] TextMeshProUGUI recompensaItemCantidad;
    public override void ConfigurarQuestUI(Quest quest)
    {
        //Cargamos el oro de la quest a la recompensa
        recompensaOro.text = quest.RecompensaORO.ToString();
        //Cargamos la experiencia
        recompensaExp.text = quest.RecompensaExp.ToString();
        //Cargamos el objetivo de la mision
        tareaObjetivo.text = $"{quest.CantidadActual}/{quest.cantidadObjetivo}";
        //Actualizamos el sprite del item que damos como recompensa
        recompensaItemIcono.sprite = quest.RecompensaItem.Item.Icono;
        //Actualizamos la cantidad de item
        recompensaItemCantidad.text = quest.RecompensaItem.Cantidad.ToString();


    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectorQuestDescripcion : QuestDescripcion
{
    //Obtenemos las referencias de las recompensas
    [SerializeField] private TextMeshProUGUI questRecompensa;
    //Sobreescribimos el metodo de la clase padre
    public override void ConfigurarQuestUI(Quest quest)
    {
        base.ConfigurarQuestUI(quest);
        QuestCargado = quest;
        //Cargamos dentro del panel los datos
        questRecompensa.text = $"-{quest.RecompensaORO } oro" +
                               $"\n-{quest.RecompensaExp } exp" +
                               $"\n-{quest.RecompensaItem.Item.Nombre } x {quest.RecompensaItem.Cantidad}";
    }

    //Creamos un metodo nuevo para aceptar las quest
    public void AceptarQuest()
    {
        //Comprobamos que la mision no este cargada ya
        if(QuestCargado == null)
        {
            return;
        }
        //Añadimos a nuestro panel la mision
        QuestManager.Instance.AñadirQuest(QuestCargado);
        //La borramos del panel del inspector
        gameObject.SetActive(false);
    }
}

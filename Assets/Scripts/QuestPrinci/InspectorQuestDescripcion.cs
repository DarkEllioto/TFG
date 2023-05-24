using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectorQuestDescripcion : QuestDescripcion
{
    //Obtenemos las referencias de las recompensas
    [SerializeField] private TextMeshProUGUI questRecompensa;
    //Sobreescribimos el metodo de la clase padre
    public override void ConfigurarQuestUI(Quest questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        //Cargamos dentro del panel los datos
        questRecompensa.text = $"-{questPorCargar.RecompensaORO } oro" +
                               $"-{questPorCargar.RecompensaExp } exp" +
                               $"-{questPorCargar.RecompensaItem.Item.Nombre } x {questPorCargar.RecompensaItem.Cantidad}";
    }
}

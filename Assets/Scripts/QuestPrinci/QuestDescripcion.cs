using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questNombre;
    [SerializeField] TextMeshProUGUI questDescripcion;

    //Creamos un acceso
    public Quest QuestPorCompletar { get; set; }

    //Creamos el metodo para modificar estos campos

    public virtual void ConfigurarQuestUI(Quest quest)
    {
        //Cargamos el nombre y la descripcion de las quest
        QuestPorCompletar = quest;
        questNombre.text = quest.Nombre;
        questDescripcion.text = quest.Descripcion;
    }
}

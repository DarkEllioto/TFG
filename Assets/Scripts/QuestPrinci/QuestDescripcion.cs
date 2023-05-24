using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questNombre;
    [SerializeField] TextMeshProUGUI questDescripcion;
    
    //Creamos el metodo para modificar estos campos

    public virtual void ConfigurarQuestUI(Quest questPorCargar)
    {
        //Cargamos el nombre y la descripcion de las quest
        questNombre.text = questPorCargar.Nombre;
        questDescripcion.text = questPorCargar.Descripcion;
    }
}

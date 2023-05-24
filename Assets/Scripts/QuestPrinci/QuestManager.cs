using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    //Creamos un array donde guardar todas las quest
    [Header("Quest")]
    [SerializeField] private Quest[] questDisponibles;

    //Creamos un campo donde obtener la referencia del prefab
    [Header("InspectorQuest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;
    
    // Start is called before the first frame update
    void Start()
    {
        CargarQuestEnInspector();
    }

   //Creamos el metodo que nos permita cargar las quest en el inspector
   private void CargarQuestEnInspector()
    {
        //recorremos todas las misiones disponibles
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            //Instanciamos el inspector
            InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
            //Pasamos la informacion de cada index
            nuevoQuest.ConfigurarQuestUI(questDisponibles[i]);
        }
    }

}

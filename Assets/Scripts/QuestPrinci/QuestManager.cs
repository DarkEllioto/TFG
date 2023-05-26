using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    //Creamos un array donde guardar todas las quest
    [Header("Quest")]
    [SerializeField] private Quest[] questDisponibles;

    //Creamos un campo donde obtener la referencia del prefab
    [Header("InspectorQuest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;
    //Creamos las referencias de nuestro personaje Quest
    [Header("PErsonajeQuest")]
    [SerializeField] private PersonajeQuest PersonajeQuestPrefab;
    [SerializeField] private Transform PersonajeQuestContenedor;

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
    //Creamos el metodo para cargar las misiones en el personaje
    private void AñadirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuest nuevoQuest = Instantiate(PersonajeQuestPrefab, PersonajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    //Llamamos al meotdo para añadir las quest

    public void AñadirQuest(Quest questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }
}

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            A�adirProgreso("Matar10", 1);
            A�adirProgreso("Matar25", 1);
            A�adirProgreso("Matar50", 1);
        }
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
    private void A�adirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuest nuevoQuest = Instantiate(PersonajeQuestPrefab, PersonajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    //Llamamos al meotdo para a�adir las quest

    public void A�adirQuest(Quest questPorCompletar)
    {
        A�adirQuestPorCompletar(questPorCompletar);
    }
    //Creamos el metodo que me permita a�adir progreso
    public void A�adirProgreso(string questID,int cantidad)
    {
        //Verificamos si la mision existe
        Quest questPorActualizar = QuestExiste(questID);
        questPorActualizar.a�adirProgreso(cantidad);
    }

    //Buscamos la referencia de la quest para comprobar que existe
    private Quest QuestExiste(string questId)
    {
        //Revisamos los index de nuestro array Quest
        for (int i = 0; i < questDisponibles.Length; i++)
        {
            //Si alguno coincide lo devolvemos
            if (questDisponibles[i].ID == questId)
            {
                //Devolvemos su referencia si coindice
                return questDisponibles[i];
            }
        }
        return null;
    }
}

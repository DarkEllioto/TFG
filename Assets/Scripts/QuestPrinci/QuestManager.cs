using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    //Obtenemos la referencia de nuestro personaje
    [Header("Personaje")]
    [SerializeField] private Personaje personaje;
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

    //A�adimos las referencias del panel 
    [Header("Panel Quest Completado")]
    [SerializeField] private GameObject panelQuestCompletado;
    [SerializeField] private TextMeshProUGUI questNombre;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExpe;
    [SerializeField] private TextMeshProUGUI questRecompensaItemCantidad;
    [SerializeField] private Image questRecompensaItemIcono;

    public Quest QuestPorReclamar { get;private set; }


    // Start is called before the first frame update
    void Start()
    {
        CargarQuestEnInspector();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("Matar10", 1);
            AñadirProgreso("Matar25", 1);
            AñadirProgreso("Matar50", 1);
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
    private void AñadirQuestPorCompletar(Quest questPorCompletar)
    {
        PersonajeQuest nuevoQuest = Instantiate(PersonajeQuestPrefab, PersonajeQuestContenedor);
        nuevoQuest.ConfigurarQuestUI(questPorCompletar);
    }

    //Llamamos al meotdo para a�adir las quest

    public void AñadirQuest(Quest questPorCompletar)
    {
        AñadirQuestPorCompletar(questPorCompletar);
    }
    //Creamos el metodo que nos permita reclamar la recompensa
    public void ReclamarRecompensa()
    {
        if(QuestPorReclamar == null)
        {
            return;
        }
        MonedasManager.Instance.AñadirMonedas(QuestPorReclamar.RecompensaORO);
        personaje.PersonajeExperiencia.AñadirExp(QuestPorReclamar.RecompensaExp);
        Inventario.Instance.AñadirItem(QuestPorReclamar.RecompensaItem.Item, QuestPorReclamar.RecompensaItem.Cantidad);
        //Desactivamos el panel y recibimos las recompensas
        panelQuestCompletado.SetActive(false);
        QuestPorReclamar = null;
    }
    //Creamos el metodo que me permita a�adir progreso
    public void AñadirProgreso(string questID,int cantidad)
    {
        //Verificamos si la mision existe
        Quest questPorActualizar = QuestExiste(questID);
        questPorActualizar.añadirProgreso(cantidad);
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

    //Creamos el metodo que nos permita actualizar el panel recompensas

    private void MostrarQuestCompletado(Quest questCompletado)
    {
        panelQuestCompletado.SetActive(true);
        //llamamos a las variables para actualizar los datos
        questNombre.text = questCompletado.Nombre;
        questRecompensaOro.text = questCompletado.RecompensaORO.ToString();
        questRecompensaExpe.text = questCompletado.RecompensaExp.ToString();
        questRecompensaItemCantidad.text = questCompletado.RecompensaItem.Cantidad.ToString();
        questRecompensaItemIcono.sprite = questCompletado.RecompensaItem.Item.Icono;
    }

    //A�adimos los metodos para poder activar el evento 
    private void QuestCompletadoRespuesta(Quest QuestCompletado)
    {
        //Verificamos que exista la mision completada
        QuestPorReclamar = QuestExiste(QuestCompletado.ID);
        //Si no es null mostramos el panel
        if(QuestPorReclamar != null)
        {
            MostrarQuestCompletado(QuestPorReclamar);
        }
    }
    private void OnEnable()
    {
        
        Quest.EventoQuestCompletado += QuestCompletadoRespuesta;
    }

    

    private void OnDisable()
    {
        Quest.EventoQuestCompletado -= QuestCompletadoRespuesta;
    }
}

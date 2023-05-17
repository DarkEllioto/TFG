<<<<<<< Updated upstream
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcono;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    public NPCInteraccion NPCDisponible { get; set; }

    private Queue<string> dialogosSecuencia;
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoManager : Singleton<DialogoManager>
{
    //Obtenemos la referencia de nuestro panel de dialogo e icono 
    [SerializeField] private GameObject panelDialogo;
    [SerializeField] private Image npcIcon;
    [SerializeField] private TextMeshProUGUI npcNombreTMP;
    [SerializeField] private TextMeshProUGUI npcConversacionTMP;

    //Creamos una propiedad que muestre de que NPC muestra su dialogo
    public NPCInteraccion NPCDisponible { get; set; }
    //Creamos una queue(cola) que almacene los dialogos
    private Queue<string> dialogosSecuencia;
    //Creamos la animacion del dialogo para que se vaya cargando poco a poco
>>>>>>> Stashed changes
    private bool dialogoAnimado;
    private bool despedidaMostrada;

    private void Start()
    {
        dialogosSecuencia = new Queue<string>();
    }

    private void Update()
    {
<<<<<<< Updated upstream
        if (NPCDisponible == null)
        {
            return;
        }

=======
        //Verificamos si tenemos la informacion de algun NPC
        if(NPCDisponible == null)
        {
            return;
        }
        //Activamos que con la letra E se abra el dialogo
>>>>>>> Stashed changes
        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPanel(NPCDisponible.Dialogo);
        }

<<<<<<< Updated upstream
        if (Input.GetKeyDown(KeyCode.Space))
        {
=======
        //Continuamos el dialogo
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Comprobamos si la despedida se ha mostrado
>>>>>>> Stashed changes
            if (despedidaMostrada)
            {
                AbrirCerrarPanelDialogo(false);
                despedidaMostrada = false;
                return;
            }
<<<<<<< Updated upstream

            if (NPCDisponible.Dialogo.ContieneInteraccionExtra)
            {
                UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.InteraccionExtra);
                AbrirCerrarPanelDialogo(false);
                return;
            }
            
=======
            //Verificamos que el dialogo anterior se haya terminado de animar
>>>>>>> Stashed changes
            if (dialogoAnimado)
            {
                ContinuarDialogo();
            }
        }
    }
<<<<<<< Updated upstream

=======
    //Creamos el metodo que nos permita abrir y cerrar el panel de dialogo
>>>>>>> Stashed changes
    public void AbrirCerrarPanelDialogo(bool estado)
    {
        panelDialogo.SetActive(estado);
    }

<<<<<<< Updated upstream
    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        CargarDialogosSencuencia(npcDialogo);
        
        npcIcono.sprite = npcDialogo.Icono;
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        MostrarTextoConAnimacion(npcDialogo.Saludo);
    }

    private void CargarDialogosSencuencia(NPCDialogo npcDialogo)
    {
        if (npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }

=======
    //Creamos la configuracion para el panel pasandole el NPC como parametro
    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        //Abrimos el panel de dialogo
        AbrirCerrarPanelDialogo(true);
        //Cargamos la secuencia de dialogo
        CargarDialogosSecuencia(npcDialogo);
        //Seteamos icono y nombre 
        npcIcon.sprite = npcDialogo.Icono;
        npcNombreTMP.text = $"{npcDialogo.Nombre}:";
        MostrarTextoConAnimacion(npcDialogo.Saludo);
        
    }

    private void CargarDialogosSecuencia(NPCDialogo npcDialogo)
    {
        //Comprobamos que nuestro dialogo no sea nulo
        if(npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
            return;
        }
        //Recorremos las conversaciones
>>>>>>> Stashed changes
        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogosSecuencia.Enqueue(npcDialogo.Conversacion[i].Oracion);
        }
<<<<<<< Updated upstream
    }

    private void ContinuarDialogo()
    {
        if (NPCDisponible == null)
        {
            return;
        }

=======


    }
    //Creamos el metodo para continuar el dialogo
    private void ContinuarDialogo()
    {
        //Verificamos que el npc siga teniendo un dialogo
        if(NPCDisponible == null)
        {
            return;
        }
>>>>>>> Stashed changes
        if (despedidaMostrada)
        {
            return;
        }

<<<<<<< Updated upstream
        if (dialogosSecuencia.Count == 0)
=======
        if(dialogosSecuencia.Count == 0)
>>>>>>> Stashed changes
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoConAnimacion(despedida);
            despedidaMostrada = true;
            return;
        }
<<<<<<< Updated upstream
        
        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }
    
    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false;
        npcConversacionTMP.text = "";
        char[] letras = oracion.ToCharArray();
        for (int i = 0; i < letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            yield return new WaitForSeconds(0.03f);
=======

        //Cargamos el siguiente dialogo
        string siguienteDialogo = dialogosSecuencia.Dequeue();
        MostrarTextoConAnimacion(siguienteDialogo);
    }
    //Creamos la animacion
    private IEnumerator animarTexto(string oracion)
    {
        dialogoAnimado = false;
        //Comprobamos el total de letras que tenemos en el dialogo
        npcConversacionTMP.text = "";
        char[] letras = oracion.ToCharArray();
        //Añadimos letra por letra a nuestra conversacion
        for (int i = 0; i < letras.Length; i++)
        {
            npcConversacionTMP.text += letras[i];
            //Esperamos unos segundos
            yield return new WaitForSeconds(0.03f);

>>>>>>> Stashed changes
        }

        dialogoAnimado = true;
    }

<<<<<<< Updated upstream
    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }
}

=======
    //Mostramos el texto
    private void MostrarTextoConAnimacion(string oracion)
    {
        StartCoroutine(animarTexto(oracion));
    }
}
>>>>>>> Stashed changes

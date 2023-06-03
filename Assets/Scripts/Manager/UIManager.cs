using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


//Heredamos la instancia UIManager desde el singleton 
public class UIManager : Singleton<UIManager>
{
    //Creamos la cabecera y referencia a nuestro scriptableobjet personaje stats
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;

    [Header("Paneles")]
    [SerializeField] private GameObject panelStat;
    [SerializeField] private GameObject panelTienda;
    [SerializeField] private GameObject panelInventario;
    [SerializeField] private GameObject panelMisiones;
    [SerializeField] private GameObject panelMisionesPersonaje;
    [SerializeField] private GameObject panelInicio;

    //Creamos unas referencias para obtener la imagen de la vida,el mana, exp y el texto

    [Header("Barra")]
    [SerializeField] private Image vidaJugador;
    [SerializeField] private Image ManaJugador;
    [SerializeField] private Image ExpJugador;


    [Header("Texto")]
    [SerializeField] private TextMeshProUGUI vidaTMP;
    [SerializeField] private TextMeshProUGUI ManaTMP;
    [SerializeField] private TextMeshProUGUI ExpTMP;
    [SerializeField] private TextMeshProUGUI NivelTMP;
    [SerializeField] private TextMeshProUGUI MonedasTMP;

    //Creamos un header para los stats y  vamos creamos su campo de texto
    [Header("Stats")]
    [SerializeField] private TextMeshProUGUI statDañoTMP;
    [SerializeField] private TextMeshProUGUI statNivelTMP;
    [SerializeField] private TextMeshProUGUI statDefensaTMP;
    [SerializeField] private TextMeshProUGUI statCriticoTMP;
    [SerializeField] private TextMeshProUGUI statDañoCriticoTMP;
    [SerializeField] private TextMeshProUGUI statEsquivarTMP;
    [SerializeField] private TextMeshProUGUI statBloquearTMP;
    [SerializeField] private TextMeshProUGUI statAgilidadTMP;
    [SerializeField] private TextMeshProUGUI statExpTMP;
    [SerializeField] private TextMeshProUGUI statSigNivelTMP;
    [SerializeField] private TextMeshProUGUI AtributoDestrezaTMP;
    [SerializeField] private TextMeshProUGUI AtributoInteligenciaTMP;
    [SerializeField] private TextMeshProUGUI AtributoAgilidadTMP;
    [SerializeField] private TextMeshProUGUI AtributoDCriticoTMP;
    [SerializeField] private TextMeshProUGUI AtributoPCriticoTMP;
    [SerializeField] private TextMeshProUGUI AtributoDefensaTMP;
    [SerializeField] private TextMeshProUGUI AtributosDispobiblesTMP;

    //Creamos las referencias para obtener la salud,el mana,exp y ambos maxima

    private float vidaActual;
    private float vidaMax;

    private float manaActual;
    private float manaMax;

    private float expActual;
    private float expRequeridaNewLevel;

    // Update is called once per frame
    void Update()
    {
        //Actualizamos la interfaz del personaje
        ActualizarUIPersonaje();
        ActualizarPanelStats();
    }
    //Creamos un metodo para actualizar el texto de la vida
    private void ActualizarUIPersonaje()
    {
        //Modificamos la barra de vida
        vidaJugador.fillAmount = Mathf.Lerp(vidaJugador.fillAmount,vidaActual/vidaMax,10f*Time.deltaTime);
        //Modificamos la barra de mana
        ManaJugador.fillAmount = Mathf.Lerp(ManaJugador.fillAmount, manaActual / manaMax, 10 * Time.deltaTime);
        //Modificamos la barra de exp
        ExpJugador.fillAmount = Mathf.Lerp(ExpJugador.fillAmount, expActual / expRequeridaNewLevel, 10 * Time.deltaTime);
       

        //Modificamos el texto de la barra de vida
        vidaTMP.text = $"{vidaActual}/{vidaMax}";
        //Modificamos el texto de la barra de mana
        ManaTMP.text = $"{manaActual}/{manaMax}";
        //Modificamos el texto de la barra de exp
        ExpTMP.text = $"{((expActual/expRequeridaNewLevel)*100):F2}%";
        //Obtenemos el texto del nivel
        NivelTMP.text = $"Nivel {stats.Nivel}";
        //Actualizamos las monedas
        MonedasTMP.text = MonedasManager.Instance.MonedasTotales.ToString();
    }
    //Creamos el metodo para actualizar el panel de stats
    private void ActualizarPanelStats()
    {
        //Comprobamos que el panel este activo para poder actualizarlo
        if (panelStat.activeSelf == false)
        {
            return;
        }
        //A�adimos el texto a los stats
        statDañoTMP.text = stats.Daño.ToString();
        statDefensaTMP.text = stats.Defensa.ToString();
        statExpTMP.text = stats.ExpActual.ToString();
        statSigNivelTMP.text = stats.ExpRequerida.ToString();
        statAgilidadTMP.text = stats.Agilidad.ToString();
        statNivelTMP.text = stats.Nivel.ToString();
        statDañoCriticoTMP.text = $"{stats.DCritico}%";
        statCriticoTMP.text = $"{stats.Critico}%";
        statEsquivarTMP.text = $"{stats.Esquivar}%";
        statBloquearTMP.text = $"{stats.Bloquear}%";

        //A�adimos texto a los atributos

        AtributoDestrezaTMP.text = stats.Destreza.ToString();
        AtributoDefensaTMP.text = stats.Defensa2.ToString();
        AtributoAgilidadTMP.text = stats.Agilidad2.ToString();
        AtributoInteligenciaTMP.text = stats.Inteligencia.ToString();
        AtributoDCriticoTMP.text = stats.Dcritico.ToString();
        AtributoPCriticoTMP.text = $"{stats.Pcritico}%";
        AtributosDispobiblesTMP.text = $"Puntos: {stats.PuntosDisponibles}";
    }

    //Creamos un metodo para actualizar la vida actual/maxima
    public void ActualizarVidaPersonaje(float pVidaAc, float pVidaMax)
    {
        vidaActual = pVidaAc;
        vidaMax = pVidaMax;
    }

    //Creamos un metodo para actualizar el mana actual/maxima
    public void ActualizarManaPersonaje(float pManaaAc, float pManaMax)
    {
        manaActual = pManaaAc;
        manaMax = pManaMax;
    }
    //Creamos el metodo para abrir y cerrar el panel de misiones del personaje
    public void abrirCerrarPersonajeMisiones()
    {
        panelMisionesPersonaje.SetActive(!panelMisionesPersonaje.activeSelf);
    }
<<<<<<< Updated upstream
    public void abrirCerrarInicio()
=======
    public void CerrarPanelInicio()
>>>>>>> Stashed changes
    {
        panelInicio.SetActive(!panelInicio.activeSelf);
    }
    //Creamos un metodo para actualizar la exp actual del personaje
    public void ActualizarExpPersonaje(float pExpAc, float pExpReq)
    {
        expActual = pExpAc;
        expRequeridaNewLevel= pExpReq;
    }

    //Creamos una region para nuestros paneles de stats, inventario y quest
    #region Paneles
    //Creamos el metodo para abrir y cerrar el panel de Stats
    public void AbrirCerrarPanelStats()
    {
        //llamamos al panel stats para activarlo o desactivarlo, si esta activo nos devuelve true
        panelStat.SetActive(!panelStat.activeSelf);
    }

      //SE CREA EL METODO ABRIR CERRAR EL PANEL DE LA TIENDA
    public void AbrirCerrarPanelTienda()
    {
        //SE LLAMA AL PANEL Y LO QUE HACE ES DEVOLVER O CAMBIAR EL ESTADO DE ESTA ES DECIR ACTIVARLO O DESACTIVARLO
        panelTienda.SetActive(!panelTienda.activeSelf);
       
    }

    //Creamos el metodo para abrir y cerrar el panel inventario
    public void AbrirCerrarInventario()
    {
        panelInventario.SetActive(!panelInventario.activeSelf);
    }
    public void abrirCerrarPanelQuest()
    {
        panelMisiones.SetActive(!panelMisiones.activeSelf);
    }

    public void AbrirPanelInteraccion(InteraccionExtraNPC tipoInteraccion)
    {
        switch (tipoInteraccion)
        {
                case InteraccionExtraNPC.Quests:
                abrirCerrarPanelQuest();
                break;
            case InteraccionExtraNPC.Tienda:
                AbrirCerrarPanelTienda();
                break;
           /* case InteraccionExtraNPC.Crafting:
                break;*/
        }
    }

    //Creamos el metodo para abrir y cerrar el panel de quest
    

    //Creamos el metodo para abrir el panel correspondiente

    
    #endregion
}

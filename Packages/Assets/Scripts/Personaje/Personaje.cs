using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Personaje : MonoBehaviour
    {
    //Creamos una referencia a nuestros stats
    [SerializeField] private PersonajeStats stats;

    //Creamos unos getter/setter para dar acceso a otras clases a nuestras propiedades
    public PersonajeVida PersonajeVida { get; private set; }
    //Creamos otros getter/setter para dar valores
    public PersonajeAnimaciones PersonajeAnimaciones { get; private set; }
    //Creamos una referencia para acceder a la clase personaje mana
    public PersonajeMana PersonajeMana { get; set; }
    //Creamos el metodo awake para tener la referencia de la clase personaje vida
    private void Awake()
    {
        PersonajeVida = GetComponent<PersonajeVida>();
        PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
        PersonajeMana = GetComponent<PersonajeMana>();
    }

    //Creamos un metodo public para restaurar al personaje
    public void RestaurarPersonaje()
    {
        PersonajeVida.RevivirPersonaje();
        PersonajeAnimaciones.RevivirPersonaje();
        PersonajeMana.RestableceMana();
    }

    //Creamos la referencia para el evento que estamos llamando de stats
    private void AtributoRespuesta(TipoAtributo tipo)
    {   
        //Comprobamos que tenemos puntos disponibles para aumentar nuestras stats
        if(stats.PuntosDisponibles <= 0)
        {
            return;
        }
        //Añadimos los atributos en funcion del boton pulsado
        switch (tipo)
        {
            case TipoAtributo.Destreza:
                stats.AñadirBonusPorAtributoDestreza();
                stats.Destreza++;
                stats.PuntosDisponibles-=1;
                break;
            case TipoAtributo.Inteligencia:
                stats.AñadirBonusPorAtributoInteligencia();
                stats.Inteligencia++;
                stats.PuntosDisponibles -= 1;
                break;
            case TipoAtributo.Agilidad2:
                stats.AñadirBonusPorAtributoAgilidad();
                stats.Agilidad2++;
                stats.PuntosDisponibles -= 1;
                break;
            case TipoAtributo.Dcritico:
                stats.AñadirBonusPorAtributoDCritico();
                stats.Dcritico++;
                stats.PuntosDisponibles -= 1;
                break;
            case TipoAtributo.Defensa2:
                stats.AñadirBonusPorAtributoDefensa();
                stats.Defensa2++;
                stats.PuntosDisponibles -= 1;
                break;
            case TipoAtributo.Pcritico:
                stats.AñadirBonusPorAtributoPcritico();
          
                stats.PuntosDisponibles -= 1;
                break;
        }
    }
    //Llamamos a los metodos para aumentar nuestros stats
    private void OnEnable()
    {
        AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
    }

    private void OnDisable()
    {
        AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
    }

}


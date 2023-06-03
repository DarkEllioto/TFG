using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    //Creamos una referencia hacia personaje stats
    [Header("Stats")]
    [SerializeField] private PersonajeStats stats;
    
    
    [Header("Configuracion")]


    // Creamos las variables para nuestra barra de experiencia
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

   

    //Creamos varias variables para controlar la experiencia
   
    private float expRequeridaSiguienteNivel;
    private float expActual;

    void Start()
    {
       
        stats.Nivel = 1;
        expRequeridaSiguienteNivel = expBase;
        
        //Actualizamos en nuestro panel de estadisticas la experiencia requerida
        stats.ExpRequerida = expRequeridaSiguienteNivel;
        //Inicializamos la barra de experiencia
        ActualizarBarraExp();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExp(6f);
        }
        
    }

    //Creamos un metodo para a�adir exp a nuestro personaje

    public void AñadirExp( float expObtenida)
    {
        
        //Verificamos que la exp no sea 0
        if(expObtenida <= 0)
        {
            return;
        }
        expActual += expObtenida;
        //Mostramos la experiencia actual
        stats.ExpActual = expActual;

        //Comprobamos si hemos igualado la experiencia requerida
        if(expActual == expRequeridaSiguienteNivel)
        {
            ActualizarNivel();
        }
        else if (expActual > expRequeridaSiguienteNivel)
        {
            float diff = expActual - expRequeridaSiguienteNivel;
            ActualizarNivel();
            AñadirExp(diff);
        }
        ActualizarBarraExp();
    }

    //Creamos un nuevo metodo para actualizar el nivel de nuestro personaje
    private void ActualizarNivel()
    {
        //Comprobamos que podamos subir de nivel
        if (stats.Nivel < nivelMax)
        {
            //Subimos de nivel
            stats.Nivel++;
            stats.ExpActual = 0;
            expActual = 0;
            //Actualizamos la experiencia para el siguiente nivel
            expRequeridaSiguienteNivel *= valorIncremental;
            //Actualizamos en nuestro panel de estadisticas la experiencia requerida
            stats.ExpRequerida = expRequeridaSiguienteNivel;
            //A�adimos puntos para aumentar atributos
            stats.PuntosDisponibles += 3;
        }
    } 

    //Creamos un metodo para actualizar la barra de experiencia
    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActual,expRequeridaSiguienteNivel);
    }
    //METODO DE RESPUESTA DE EXPERIENCIA GANADA CON UN PARAMETRO DE TIPO FLOAT
    private void RespuestaEnemigoDerrotado(float exp)
    {
        AñadirExp(exp);
    }


     private void OnEnable()
    {
        //se llama al evento enemigo derrotado
        EnemigoVida.EventoEnemigoDerrotado += RespuestaEnemigoDerrotado;
    }

    private void OnDisable()
    {
        EnemigoVida.EventoEnemigoDerrotado -= RespuestaEnemigoDerrotado;
    }
}

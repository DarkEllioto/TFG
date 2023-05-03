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
    private float expActualTemp;
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
            AñadirExp(10f);
        }
        
    }

    //Creamos un metodo para añadir exp a nuestro personaje

    public void AñadirExp( float expObtenida)
    {
        
        //Comprobamos que la experiencia recibida es mayor a 0
        if (expObtenida > 0f)
        {
            //Creamos una nueva variable para calcular la exp requerida para el siguiente nivel
            float expRestanteNuevoNivel = expRequeridaSiguienteNivel - expActualTemp;
            //Comprobamos si con la experiencia obtenida subimos de nivel
            if (expObtenida >= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;
                expActual = 0;
                expActual += expObtenida;
                ActualizarNivel();
                //Utilizamos recursion para añadir la experiencia que nos sobro al restar la requerida
                AñadirExp(expObtenida);
            }
            else
            {
                //Si por el contrario no subimos de nivel almacenamos dicha experiencia

                expActual += expObtenida;
                expActualTemp += expObtenida;
                //Y si al actualizar dicha exp topamos con el limite, actualizamos nivel
                if(expActualTemp == expRequeridaSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
        }
        //Actualizamos la barra de experiencia
        stats.ExpActual = expActual;
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
            //Actualizamos la experiencia actual
            expActualTemp = 0f;
            //Actualizamos la experiencia para el siguiente nivel
            expRequeridaSiguienteNivel *= valorIncremental;
            //Actualizamos en nuestro panel de estadisticas la experiencia requerida
            stats.ExpRequerida = expRequeridaSiguienteNivel;
            //Añadimos puntos para aumentar atributos
            stats.PuntosDisponibles += 3;
        }
    } 

    //Creamos un metodo para actualizar la barra de experiencia
    private void ActualizarBarraExp()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActualTemp,expRequeridaSiguienteNivel);
    }
}

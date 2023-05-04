using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Añadimos la propiedad para los assets del menu, fuera de nuestra clase principal
[CreateAssetMenu(menuName ="Stats")]
//Convertimos nuestra clase en una clase Scriptableobjet
public class PersonajeStats : ScriptableObject
{
    //Creamos los parametros que habran en nuestro menu
    [Header("Stats")]
    public float Nivel;
    public float Daño = 5f;
    public float Defensa = 2f;
    public float Agilidad = 5f;
    public float ExpActual;
    public float ExpRequerida;
    [Range(0f, 100f)] public float Critico;
    [Range(0f, 100f)] public float Esquivar;
    [Range(0f, 100f)] public float Bloquear;
    [Range(0f, 100f)] public float DCritico;


    [Header("Atributos")]

    public int Destreza;
    public int Defensa2;
    public int Agilidad2;
    public int Inteligencia;
    public int Dcritico;
    public float Pcritico;

    [HideInInspector]
    public int PuntosDisponibles;

    //Creamos los bonus por añadir atributos
    public void AñadirBonusPorAtributoDestreza()
    {
        Daño += 2f;
        Defensa += 1f;
        Bloquear += 0.03f;
    }

    public void AñadirBonusPorAtributoInteligencia()
    {
        Daño += 3.5f;
        Defensa += 0.6f;
        DCritico += 0.15f;
    }

    public void AñadirBonusPorAtributoDefensa()
    {
        Daño += 1.2f;
        Defensa += 3.5f;
        Bloquear += 0.18f;

    }

    public void AñadirBonusPorAtributoAgilidad()
    {
        Daño += 2f;
        DCritico = 0.25f;
        Pcritico = 0.15f;
        Agilidad = 0.1f;

    }

    public void AñadirBonusPorAtributoDCritico()
    {
        Daño += 2.5f;
        DCritico += 0.3f;
        Pcritico += 0.05f;

    }

    public void AñadirBonusPorAtributoPcritico()
    {
        Daño += 2.5f;
        DCritico += 0.15f;
        Pcritico += 0.1f;

    }
    //Creamos un metodo para resetar los valores
    public void ResetarValores()
    {
        //Stats
        Daño = 5f;
        Defensa = 2f;
        Agilidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpRequerida = 0f;
        Bloquear = 0f;
        Critico = 0f;
        DCritico = 0f;
        Esquivar = 0f;
        //Atributos
        Destreza=0;
        Defensa2=0;
        Agilidad2=0;
        Inteligencia=0;
        Dcritico=0;
        Pcritico=0;

        PuntosDisponibles = 0;
}
   
}

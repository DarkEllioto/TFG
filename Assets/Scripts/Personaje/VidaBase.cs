using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    // Creamos las variables base y maxima de la vida
    [SerializeField] protected float VidaInicio;
    [SerializeField] protected float VidaMaxima;

    //Creamos un getter/setter para modificar nuestras vidas
    public float Vida { get; protected set; }
    protected virtual void Start()
    {
        Vida = VidaInicio;
        ActualizarBVida(Vida,VidaMaxima);
    }

    // Creamos el metodo para recibir da�o
    public void RecibirDaño(float cantidad)
    {
        //Comprobamos que la cantidad de da�o no sea nula 
        if (cantidad <= 0)
        {
            return;
        }
        //Comprobamos la cantidad de vida que vamos a restar siempre que sea superior a 0
        if(Vida > 0f)
        {
            Vida -= cantidad;
            ActualizarBVida(Vida, VidaMaxima);
            if(Vida <= 0f)
            {
                Vida=0f;
                ActualizarBVida(Vida, VidaMaxima);
                PersonajeDone();
            }
        }
    }
    //Creamos un metodo protected para actualizar la barra de vida de nuestro personaje
    protected virtual void ActualizarBVida(float vidaActual, float vidaMax) 
    {
            
    }
    //Creamos un metodo para saber que nuestro personaje ha sido derrotado
    protected virtual void PersonajeDone()
    {

    }
}

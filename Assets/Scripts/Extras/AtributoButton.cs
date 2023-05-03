using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creamos los atributos a incrementar con los puntos disponibles
public enum TipoAtributo
{
    Destreza,
    Defensa2,
    Agilidad2,
    Inteligencia,
    Dcritico,
    Pcritico,
}
public class AtributoButton : MonoBehaviour
{
    //Crreamos el evento para que al aumentar los puntos se aumente los stats
    public static Action<TipoAtributo> EventoAgregarAtributo;

    //Creamos una variable privada
    [SerializeField] private TipoAtributo tipo;

    //Creamos un metodo para lanzar el evento
    public void AgregaAtributo()
    {
        EventoAgregarAtributo?.Invoke(tipo);
    }
}

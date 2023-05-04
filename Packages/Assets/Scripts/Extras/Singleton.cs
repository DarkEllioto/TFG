using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Añadimos <T> a la clase singleton para hacerla heredable por cualquier clase
//Y el "where T : Component" para identificarlo como un componente
public class Singleton<T> : MonoBehaviour where T : Component
{
    //Creamos la instancia que sera heredada

    private static T _instance;

    public static T Instance
    {
        //Creamos un get que nos devuelva la instancia
        get
        {
            //Comprobamos que no sea nula
            if(_instance == null)
            {
                //Si es nula le asignamos el singleton
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    //En caso de seguir siendo nula creamos un nuevo game object y se la asignamos
                    GameObject nuevoGO = new GameObject();
                    _instance = nuevoGO.AddComponent<T>();
                }
            }
            //Devolvemos la instancia siempre que no sea nula y de serlo asi con el valor nuevo añadido
            return _instance;
        }
    }
    //Inicializamos la instancia

    protected virtual void Awake()
    {
        //igualamos la instancia al valor del objeto T 
        _instance = this as T;
    }
}

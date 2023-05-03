using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //VARIABLE QUE INDICA LA INSTANCIA
    [SerializeField] private int cantidadPorCrear;

//LISTA QUE GUARDA LAS INSTANCIAS QUE SE CREAN
    private List<GameObject> lista;
    //EN TIEMPO REAL PARA CONTENER LAS INSTANCIAS QUE SE VAN CREANDO
    public GameObject ListaContenedor { get; private set; }

// SE CREA UN METODO PARA CREAR EL POOLER 
    public void CrearPooler(GameObject objetoPorCrear) //SE PASA POR PARAMETRO EL OBJETO A CREAR
    {
        lista = new List<GameObject>(); //SE INICIALIZA LA LISTA
        ListaContenedor = new GameObject($"Pool - {objetoPorCrear.name}"); //SE LE ESPECIFICA EL NOMBRE DEL POOL 

        for (int i = 0; i < cantidadPorCrear; i++)//CON UN BUCLE FOR RECORRE LAS INSTANCIAS A CREAR 
        {
            lista.Add(AñadirInstancia(objetoPorCrear)); //SE AÑADE LA INSTANCIAS
        }
    }

    // PARA CREAR UNA INSTANCIA SE DEBE ESPECIFICAR QUE OBJETO SE DEBE CREAR 
    private GameObject AñadirInstancia(GameObject objetoPorCrear) //SE PASA POR PARAMETRO
    {
        GameObject nuevoObjeto = Instantiate(objetoPorCrear, ListaContenedor.transform); //SE CREA UN NUEVO OBJETO INSTANCIANDO  EL OBHJETO QUE SE PASA POR PARAMETRO Y LA LISTA 
        nuevoObjeto.SetActive(false); //CADA INSTANCIA QUE SE CREA DEBE ESTAR DESACTIVADA
        return nuevoObjeto; //SE REGRESA LA INSTANCIA
    }

//SE CREA UN METODO PARA OBTENER LA INSTANCIA
    public GameObject ObtenerInstancia()
    {
        //CON UN CICLO FOR SE RECORRE LA LISTA 
        for (int i = 0; i < lista.Count; i++)
        {
            if (lista[i].activeSelf == false)//SE BUSCA EL PRIMER OBJETO QUE ESTE DESACTIVADO 
            {
                return lista[i];// SE MUESTA Y SE REGRESA
            }
        }

        return null; //SI NO SE DEVUELVE NULL
    }
}

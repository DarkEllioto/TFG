using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaConfiner : MonoBehaviour
{
    //Creamos la variable para obtener la camara activa en cada momento
    [SerializeField] private CinemachineVirtualCamera camara;

    //Utilizamos triggers para detectar donde esta nuestro personaje
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Comparamos el tag de nuestro personaje y activamos la camara en caso de coincidir
        if (other.CompareTag("Player"))
        {
            camara.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Comparamos el tag de nuestro personaje y desactivamos la camara en caso de coincidir
        if (other.CompareTag("Player"))
        {
            camara.gameObject.SetActive(false);
        }
    }
}

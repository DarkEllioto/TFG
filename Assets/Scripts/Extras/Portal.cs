
using UnityEngine;

public class Portal : MonoBehaviour

//CLASE PARA TELETRANSPORTAR AL PERSONAJE 
{
    //SE CREA UNA VARIABLE PRIVADA PARA OBTENER LA POSICION DEL PERSONAJE
    [SerializeField] private Transform nuevaPos;


    //SE CREA UN METODO PARA REDIRIGIR O TELETRANSPORTAR AL PERSONAJE
    private void OnTriggerEnter2D(Collider2D other)
    {
        //SE VERIFICA SI SE ESTA COLICIONANDO CON EL OBJECTO QUE TENGA EL TAG PLAYER
        if (other.CompareTag("Player"))
        {
            //SE ACTUALIZA LA POCIDSION DEL PERSONAJE
            other.transform.localPosition = nuevaPos.position;
        }
    }
}


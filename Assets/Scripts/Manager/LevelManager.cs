using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LevelManager : MonoBehaviour 
    {
        //Obtenemos una referencia de nuestro personaje
        [SerializeField] private Personaje personaje;
        //Creamos el punto de resureccion de nuestro personaje
        [SerializeField] private Transform puntoResurec;
        
        //Comprobamos que nuestro personaje se encuentre vivo o muerto
        private void Update()
        {
            //Activamos que el personaje solo pueda revivir cuando pulsamos revivir
            if (Input.GetKeyDown(KeyCode.R))
            {
                 //Seteamos la posicion del personaje para cuando ha de revivir
                if (personaje.PersonajeVida.PersonajeKO)
                {
                    personaje.transform.localPosition = puntoResurec.position;
                    personaje.RestaurarPersonaje();
                }
        }
        }
}


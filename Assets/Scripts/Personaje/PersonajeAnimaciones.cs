using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAnimaciones : MonoBehaviour
{
    // Creamos la variable Animator
    private Animator _animator;
    //Creamos la referencia hacia personajemover para obtener el movimiento
    private PersonajeMover _personajeMover;
    //
    private PersonajeAtaque _personajeAtaque;
    //Creamos un hash para las direcciones de movimiento
    private readonly int direccionX = Animator.StringToHash(name:"X");
    private readonly int direccionY = Animator.StringToHash(name: "Y");
    //Creamos dos campos privados para las capas de animacion
    [SerializeField] private string LayerIdle;
    [SerializeField] private string LayerCaminar;
    //SE CREA EL LAYER DE ANIMACION DE ATACAR 
    [SerializeField] private string LayerAtacar;
    //Creamos el hash para la animacion de muerte
    private readonly int Muerte = Animator.StringToHash(name: "Muerte");


    //creamos el metodo awake para obtener la referencia
    private void Awake()
    {
        //llamamos al animator
        _animator = GetComponent<Animator>();
        //Llamamos a personaje mover
        _personajeMover = GetComponent<PersonajeMover>();
        _personajeAtaque= GetComponent<PersonajeAtaque>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Llamamos al actualizador de layers
        ActualizarLayers();
        //Creamos una condicion para establecer la direccion en la que mira nuestro personaje una vez que esta parado 
        if(_personajeMover.EnMov == false)
        {
            //devolvemos la ultima actualizacion y de la dejamos en stan by con la ultima
            return;
        }
        //accedemos a nuestro eje de coordenadas 
        _animator.SetFloat(direccionX,_personajeMover.DireccionMovimiento.x);
        _animator.SetFloat(direccionY,_personajeMover.DireccionMovimiento.y);
    }
    //Creamos un metodo que nos permita activar y desactivar layers en funcion de nuestro movimiento
    private void ActualizarLayers()
    {
        //SE VERIFICA SI SE ESTA ATACANDO 
        if(_personajeAtaque.Atacando){
            //SE LLAMA ACTIVAR LAYER DE ATACAR
              ActivarLayer(LayerAtacar);  
        }
        //SI NO SE ESTA ATACANDO
        //SE VERIFICA SI ESTA EN MOVIMIENTO 
       else if (_personajeMover.EnMov)
        {
            //SE ACTIVA EL LAYER DE CAMINAR
            ActivarLayer(LayerCaminar);
        }
        else
        {
            //DE LO CONTRARIO DE ACTIVA EL LAYER DE IDLE
            ActivarLayer(LayerIdle);
        }
    }



    //Creamos un metodo para activar nuestros layers
    private void ActivarLayer(string nombreLayer)
    {
        //Desactivamos cualquier layer activo
        for(int i = 0; i < _animator.layerCount; i++)
        {
            _animator.SetLayerWeight(i,0);
        }

        _animator.SetLayerWeight(_animator.GetLayerIndex(nombreLayer), 1);
    }
    //Creamos el metodo para la animacion
    private void EventoDeadRespuesta()
    {
        //Comprobamos que la capa de animacion que esta activada es la correcta para mostrar nuestra animacion de muerte
        if (_animator.GetLayerWeight(_animator.GetLayerIndex(LayerIdle)) == 1)
        {
            //Setteamos la animacion de muerte
            _animator.SetBool(Muerte, true);
        }
        else
        {
            ActivarLayer(LayerIdle);
            _animator.SetBool(Muerte, true);
        }
    }
    //Creamos un nuevo metodo para que nuestro personaje recupere la animacion principal al renacer
    public void RevivirPersonaje()
    {
        //Activamos la capa de layeridle y decimos que nuestro personaje ya no esta derrotado
        ActivarLayer(LayerIdle);
        _animator.SetBool(Muerte,false);
    }
    //Creamos un evento para saber cuando la clase se ha activado e inicia el evento
    private void OnEnable()
    {
        PersonajeVida.EventoDead += EventoDeadRespuesta;
    }
    //Creamos un evento para saber cuando la clase se ha desactivado y desactiva el evento
    private void OnDisable()
    {
        PersonajeVida.EventoDead -= EventoDeadRespuesta;
    }
}

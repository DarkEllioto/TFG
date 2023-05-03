using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//Añadimos la herencia de la clase principal
public class PersonajeVida : VidaBase
    {
    //Creamos un evento para cuando nuestro personaje muere
     public static Action EventoDead;
    //Creamos una variable para desactivar el colisionador
    private BoxCollider2D _boxCollider2D;
    //Creamos una nueva propiedad que nos permita saber si nuestro personaje ha muerto o no
     public bool PersonajeKO { get; private set; }
    //Creamos un booleano para saber si nuestro personaje tiene menos vida de la maxima para curarle
    public bool PuedeCurarse => Vida < VidaMaxima;
    //Probamos hacer daño a nuestro personaje para testear 

    //Creamos un metodo awake para el colisionador 
    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        //Al presionar la tecla T recibiremos daño
        if (Input.GetKeyDown(KeyCode.T)){
            RecibirDaño(10);
        }
        //Al pulsar la tecla R restauraremos vida
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarV(10);
        }
    }
    //Creamos un metodo para restaurar vida
    public void RestaurarV(float Cantidad)
    {
        //Evitamos recuperar vida con el personaje muerto
        if (PersonajeKO)
        {
            return;
        }
        //Comprobamos que el personaje pueda curarse y tras esto que no supere la vida maxima
        if(PuedeCurarse)
        {
            Vida += Cantidad;
            if (Vida > VidaMaxima)
            {
                Vida = VidaMaxima;
            }
            ActualizarBVida(Vida, VidaMaxima);
        }
    }

    //Creamos un nuevo metodo para revivir a nuestro personaje en caso de que haya muerto

    public void RevivirPersonaje()
    {
        //Desactivamos el colisionador de nuestro personaje en caso de que haya revivido
        _boxCollider2D.enabled = true;
        //Devolvemos false en caso de que el personaje haya revivido
        PersonajeKO = false;
        //Restauramos nuestra salud al valor inicial
        Vida = VidaInicio;
        //Actualizamos la barra de vida de nuevo
        ActualizarBVida(Vida, VidaInicio);
    }

    //Sobreescribimso el metodo personaje derrotado
    protected override void PersonajeDone()
    {
        //Desactivamos el colisionador de nuestro personaje en caso de que haya muerto
        _boxCollider2D.enabled = false;
        //Devolvemos true en caso de que el personaje haya muerto
        PersonajeKO = true;
        //Invocamos el evento personaje dead para confirmar que este no es nulo y puede ejercer su funcion
        EventoDead?.Invoke();
       
    }
    //Sobreescribimos el metodo actualizar vida
    protected override void ActualizarBVida(float vidaActual, float vidaMax)
    {
        UIManager.Instance.ActualizarVidaPersonaje(vidaActual, vidaMax);
    }
    //Sobreescribimos el start de vidabase
    protected override void Start()
    {
        base.Start();
    }
}


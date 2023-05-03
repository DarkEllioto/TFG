using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    // Creamos los campos iniciales del mana
    [SerializeField] private float manaInicial;
    [SerializeField] private float manaMax;
    [SerializeField] private float RegenPSec;
    //Creamos geter/seter para obtener el mana actual
    public float ManaActual { get; private set; }

    //Obtenemos una referencia de la clase vida para comprobar que nuestro personaje no esta muerto
    private PersonajeVida _personajeVida;

    //Instanciamos la clase personaje vida
    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
    }
    void Start()
    {
        //incializamos el mana actual
        ManaActual = manaInicial;
        //Actualizamos la barra de mana
        ActualizarBarraMana();
        //Llamamos al metodo con InvokeRepeating para no forzar el sistema 
        //Primero metemos el metodo que queremos llamar, luego el numero de veces y luego el espacio de tiempo
        //En segundos que este va a estar repitiendose
        InvokeRepeating(nameof(RegenerarMana),1, 1);

    }

    //Comprobamos si estamos usando el mana
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMana(10f);
        }
    }
    //Creamos un metodo que nos permita usar mana
    public void UsarMana(float Cantidad)
    {
        //Comprobamos que tenemos mas mana del que queremos gastar
        if (ManaActual >= Cantidad)
        {
            //Restamos el coste a la cantidad actual
            ManaActual -= Cantidad;
            //Actualizamos la barra de mana
            ActualizarBarraMana();
        }
    }
    //Creamos la clase para regenerar mana de forma automatica
    private void RegenerarMana()
    {
        //Comprobamos que el personaje no esta muerto y no tiene el mana al maximo
        if(_personajeVida.Vida>0f && ManaActual < manaMax)
        {
            //Actualizamos el mana
            ManaActual += RegenPSec;
            ActualizarBarraMana();
        }
    }

    //Creamos un metodo que para cuando reviva nuestro personaje reviva con el mana a tope
    public void RestableceMana()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }
    //Creamos el metodo  que actualice el mana
    private void ActualizarBarraMana()
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual, manaMax);
    }
}

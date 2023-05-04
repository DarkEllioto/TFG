using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMover : MonoBehaviour
{
    
    //Creamos la variable Rigidbody
    private Rigidbody2D _RigidBody2D;
    //Creamos los vectores de input
    private Vector2 _input;
    //Creamos la direccion de movimiento
    private Vector2 _direccionMov;
    //Creamos la velocidad
    [SerializeField] private float velocidad;
    //Creamos la propiedad publica para tener un getter de movimiento y añadirlo a otras clases
    public Vector2 DireccionMovimiento => _direccionMov;
    //Creamos una propiedad booleana que nos diga si estamos en movimiento
    public bool EnMov => _direccionMov.magnitude > 0f;
   
    //Creamos el metodo para obtener la referencia del rigidbody
    private void Awake()
    {
        //Llamamos al rigidbody
        _RigidBody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Obtenemos el axis de movimiento horizontal
        _input = new Vector2(x:Input.GetAxisRaw("Horizontal"),y:Input.GetAxisRaw("Vertical"));

        //Izquierda o Derecha
        if(_input.x > 0.1f)
        {
            //Derecha
            _direccionMov.x = 1f;
        }else if(_input.x < 0f)
        {
            //Izquierda
            _direccionMov.x = -1f;
        }
        else
        {
            //Stand by

            _direccionMov.x = 0f;
        }

        //Arriba o Abajo

        if (_input.y > 0.1f)
        {
            //Arriba
            _direccionMov.y = 1f;
        }
        else if (_input.y < 0f)
        {
            //Abajo
            _direccionMov.y = -1f;
        }
        else
        {
            //Stand by

            _direccionMov.y = 0f;
        }


    }

    private void FixedUpdate()
    {
        //correjimos la velocidad, movimiento y posicion actual
        _RigidBody2D.MovePosition(_RigidBody2D.position + _direccionMov * velocidad * Time.fixedDeltaTime);
    }
    
}

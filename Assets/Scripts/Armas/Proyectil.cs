using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    //VARIABLE PARA LA VELOCIDAD
    [Header("Config")]
    [SerializeField] private float velocidad;

    //VARIABLES
    private Rigidbody2D _rigidbody2D;
    private Vector2 direccion;
    private EnemigoInteraccion enemigoObjetivo;

    
    private void Awake()
    {
        //SE OBTIENE EL COMPONENTE DEL RIGIDBODY 
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

     private void FixedUpdate()
    {
        //SI NO SE TIENE UN ENEMIGO DE OBJETIVO 
        if (enemigoObjetivo == null)
        {
            //SE REGRESA
            return;
        }
        //SI NO SE LLAMA EL METODO
        MoverProyectil();
    }

    //METODO DE MOVIMIENTO DEL PROYECTIL
     private void MoverProyectil()
    {
        //SE OBTIENE LA DIRECCION 
        direccion = enemigoObjetivo.transform.position - transform.position;
        //SE OBTIENE EL ANGULO CON LA CLASE MATHF SE LE PASA LA DIRECCION EN X -Y Y SE MULTIPLICA CON EL ANGULO DE IUNITY
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        //DESPUES DE OBTENER EL ANGULO SE HACE LA TRANSFORMACION DEL MOVIMIENTO
        transform.rotation = Quaternion.AngleAxis(angulo, Vector3.forward);
        //SE HACE EL MOVIMIENTO EN POCISIONES 
        _rigidbody2D.MovePosition(_rigidbody2D.position + direccion.normalized * velocidad * Time.fixedDeltaTime);
    }

    //METODO INICIALIZAR PROYECTIL SE LE PASA UN PARAMETRO 
    public void InicializarProyectil(EnemigoInteraccion enemigo)
    {
        //
        enemigoObjetivo = enemigo;
       // enemigoObjetivo = ataque.EnemigoObjetivo;
    }

    //SE AÑADE UN METODO PARA PODER DESTRUIR EL PROYECTIL
    private void OnTriggerEnter2D(Collider2D other)
    {
        //SI COLICIONA CON EL ENEMIGO
        if (other.CompareTag("Enemigo"))
        {
            /*
            float daño = PersonajeAtaque.ObtenerDaño();
            enemigoObjetivo.GetComponent<EnemigoVida>().RecibirDaño(daño);
            PersonajeAtaque.EventoEnemigoDañado?.Invoke(daño);*/
            //SE DESACTIVA EL OBJECT
            gameObject.SetActive(false);
        }
    }
}

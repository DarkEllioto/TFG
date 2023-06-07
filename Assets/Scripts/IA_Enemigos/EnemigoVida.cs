using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemigoVida : VidaBase
{
    public static Action<float> EventoEnemigoDerrotado;

    //VARIABLES 
    [Header("Vida")]
   [SerializeField] private EnemigoBarraVida barraVidaPrefab;
    [SerializeField] private Transform barraVidaPosicion;

     [Header("Rastros")] 
    [SerializeField] private GameObject rastros;

    //REFERENCIAS
    private EnemigoBarraVida _enemigoBarraVidaCreada;
    private EnemigoInteraccion _enemigoInteraccion;
    private EnemigoMovimiento _enemigoMovimiento;
    private EnemigoLoot _enemigoLoot;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private IA_Controller _controller;

    //SE SOBREESCRIBE EL METODO
     protected override void Start()
    {
        //SE LLAMAN LOS METODOS
        base.Start();
        CrearBarraVida();
    }


    private void Awake()
    {
        //SE OBTIENE LA REFERENCIA DE LOS COMPONENTES
       _enemigoInteraccion = GetComponent<EnemigoInteraccion>();
       _enemigoMovimiento = GetComponent<EnemigoMovimiento>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
         _enemigoLoot = GetComponent<EnemigoLoot>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _controller = GetComponent<IA_Controller>();
    }

     private void CrearBarraVida()
    {
        //SE INSTANCIA LA POCISOIN DE BARRA VIDA Y SE GUARDA EN LA VARIABLE
        _enemigoBarraVidaCreada = Instantiate(barraVidaPrefab, barraVidaPosicion);
        //SE LLAMA AL METODO ACTUALIZAR BARRA VIDA
        ActualizarBVida(Vida, VidaMaxima);
    }

    //SE SOBREESCRIE EL METODO ACTUALIZAR BARRA VIDA
     protected override void ActualizarBVida(float saludActual, float saludMax)
    {
        //SE LE MODIFICA LA SALUD 
        _enemigoBarraVidaCreada.ModificarSalud(saludActual, saludMax);
    }
    //SE SOBREESCRIBE EL METODO DE PERSONAJE DERROTADO
    protected override void PersonajeDone()
    {
        //SE LLAMA A DESACTIVAR ENEMIGO
        DesactivarEnemigo();
        //SE INVOCA AL EVENTO
         EventoEnemigoDerrotado?.Invoke(_enemigoLoot.ExpGanada);
        //SE AÑADEN PROGRESOS A LAS MISIONES 
        QuestManager.Instance.AñadirProgreso("Matar10", 1);
        QuestManager.Instance.AñadirProgreso("Matar25", 1);
        QuestManager.Instance.AñadirProgreso("Matar50", 1);
    }

    //METODO PARA DESACTIVAR EL ENEMIGO 
    private void DesactivarEnemigo()
    {
        //SE DESACTIVA LOS CONTROLES Y RENDERED, LA BARRA Y EL MOVIMIENTO DEL ENEMIGO
        rastros.SetActive(true);
        _enemigoBarraVidaCreada.gameObject.SetActive(false);
        _spriteRenderer.enabled = false;
       _enemigoMovimiento.enabled = false;
        _controller.enabled = false;
        _boxCollider2D.isTrigger = true;
        _enemigoInteraccion.DesactivarSpritesSeleccion();
    }
}

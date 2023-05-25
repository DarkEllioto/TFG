using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ContenedorArma :  Singleton<ContenedorArma>
{
    [SerializeField] private Image armaIcono;
    [SerializeField] private Image armaSkillIcono;
  

  //SE CREA LA PROPIEDAD PARA GUARDAR EL ARMA EQUIPADA
    public ItemArma ArmaEquipada { get; set; }

    //METODO PARA PODER EQUIPAR EL ARMA
    //SE PASA POR PARAMETRO LA REFERNCIA DEL ITEM-ARMA 
    public void EquiparArma(ItemArma itemArma) 
    {
        //SE GUARDA EN UNA VARIABLE EL ARMA QUE TENEMOS SELECCIONADA
        ArmaEquipada = itemArma;
        //SE GUARDA ELICONO DEL ARMA
        armaIcono.sprite = itemArma.Arma.ArmaIcono; 
        //SE ACTIVA LA IMAGEN
        armaIcono.gameObject.SetActive(true); 

        //SE COMPRUEBA SI EL ARMA ES DE TIPO MAGIA PARA PODER ACTIVAR EL SKILLL
       if (itemArma.Arma.Tipo == TipoArma.Magia) 
        {
            //SE GUARDA EL SKILL DEL ARMA
            armaSkillIcono.sprite = itemArma.Arma.IconoSkill; 
            //SE ACTIVA EL SKILL
            armaSkillIcono.gameObject.SetActive(true); 
      }
        //SE LLAMA A EL METODO EQUIPAR ARMA INVOCANDO LAS CLASES RELACIONADAS
       Inventario.Instance.Personaje.PersonajeAtaque.EquiparArma(itemArma);
    }

    // METODO PARA REMOVER EL ARMA
    public void RemoverArma()
    {
        //SE DESACTIVA EL ICONO Y EL SKILLL
        armaIcono.gameObject.SetActive(false);
        armaSkillIcono.gameObject.SetActive(false);
        //SE PONE EN NULL EL ARMA 
        ArmaEquipada = null;
        
      //SE LLAMA EL METODO REMOVER ARMA
       Inventario.Instance.Personaje.PersonajeAtaque.RemoverArma();
    }



}

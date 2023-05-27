using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoDeteccion
{
    Rango,
    Melee
}

public class EnemigoInteraccion : MonoBehaviour
{
    //VARIABLES TIPO GAME OBJECT
    [SerializeField] private GameObject seleccionRangoFX;
    [SerializeField] private GameObject seleccionMeleeFX;

    //METODO PARA PODER MOSTRAR EL ENEMIGO SELECCIONADO  
    public void MostrarEnemigoSeleccionado(bool estado, TipoDeteccion tipo)
    {
        //SI EL ESTADO ES VERDADERO LO MUESTRA SI ES FALSE LO OCULTA 
        //SI EL TIPO ES DE TIPO RANGO 
       if (tipo == TipoDeteccion.Rango)
        {
            //SELECIONA ES DE RANGO FX
            seleccionRangoFX.SetActive(estado);
        }
        else
        {   //O DE SELECCION MELEE FX
            seleccionMeleeFX.SetActive(estado);
        }
    }

    //METODO PARA DESACTIVAR LOS SPRITES 
    public void DesactivarSpritesSeleccion()
    {
        seleccionMeleeFX.SetActive(false);
        seleccionRangoFX.SetActive(false);
    }
}

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
    [SerializeField] private GameObject seleccionRangoFX;
    [SerializeField] private GameObject seleccionMeleeFX;

    //METODO PARA PODER MOSTRAR EL ENEMIGO SELECCIONADO  
    public void MostrarEnemigoSeleccionado(bool estado, TipoDeteccion tipo)
    {
      /*  if (tipo == TipoDeteccion.Rango)
        {
            //SI EL ESTADO ES VERDADERO LO MUESTRA SI ES FALSE LO OCULTA */
            seleccionRangoFX.SetActive(estado);
       /* }
        else
        {
            seleccionMeleeFX.SetActive(estado);
        }
    }

    public void DesactivarSpritesSeleccion()
    {
        seleccionMeleeFX.SetActive(false);
        seleccionRangoFX.SetActive(false);*/
    }
}

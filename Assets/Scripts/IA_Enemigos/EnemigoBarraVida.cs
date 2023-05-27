using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoBarraVida : MonoBehaviour
{
   //VARIABLE PARA LA REFERENCIA DE BARRA VIDA
    [SerializeField] private Image barraVida;

    //SE CREAN LAS VARIABLES PARA LA SALUD ACTUAL Y SALUD MAXIMA DEL ENEMIGO
    private float saludActual;
    private float saludMax;

    //SE CREA EL METODO PARA ACTUALIZAR 
    private void Update()
    {
        //SE ACTUALIZA LA BARRA DE LA VIDA 
        barraVida.fillAmount = Mathf.Lerp(barraVida.fillAmount, 
            saludActual / saludMax, 10f * Time.deltaTime);
    }

    //SE CREA EL METODO PARA MODIFICAR LA SALUD DEL ENEMIGO SE LE PASA COMO PARAMETROS LA SALUD ACTUAL Y LA SALUD MAX
    public void ModificarSalud(float pSaludActual, float pSaludMax)
    {
        //SE OBTIENE LA REFERENCIA DE DICHOS VALORES DE LA VIDA DEL ENEMIGO 
        saludActual = pSaludActual;
        saludMax = pSaludMax;
    }
}

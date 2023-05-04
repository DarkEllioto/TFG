using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//CLASE DE CANVAS
public class TextoAnimacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dañoTexto;

    public void EstablecerTexto(float cantidad)
    {
        dañoTexto.text = cantidad.ToString();
    }
}

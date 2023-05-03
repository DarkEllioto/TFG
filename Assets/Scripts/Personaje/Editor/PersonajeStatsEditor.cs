using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//Indicamos que va a ser un editor para personaje stats
[CustomEditor(typeof(PersonajeStats))]
public class PersonajeStatsEditor : Editor
{
    // Indicamos el target de este editor y creamos un enlace para poder acceder a otras clases
    public PersonajeStats StatsTarget => target as PersonajeStats;
    //Sobreescribimos el inspector para añadir un boton
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Resetear Valores"))
        {
            //Reseteamos los valores
            StatsTarget.ResetarValores();
        }
    }
}

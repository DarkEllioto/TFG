using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint WaypointTarget => target as Waypoint;
    //Creamos un metodo para poder mover los puntos a la hora de crear los movimientos automatizados
    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        //Verificamos si tenemos puntos con los que trabajar
        if(WaypointTarget.Puntos == null)
        {
            return;
        }

        //Recorremos todos los puntos existentes
        for (int i = 0; i < WaypointTarget.Puntos.Length; i++)
        {
            //Verificamos cualquier cambio hecho en el editor para modificar la ruta
            EditorGUI.BeginChangeCheck();
            //Obtenemos la posicion actual de cada punto en la escena
            Vector3 puntoActual = WaypointTarget.PosicionActual + WaypointTarget.Puntos[i];
            //Añadimos la posicion en la que cada punto de nuestra ruta estara ubicado
            Vector3 nuevoPunto = Handles.FreeMoveHandle(puntoActual,Quaternion.identity,0.7f,new Vector3(0.3f,0.3f,0.3f), Handles.SphereHandleCap);
            //Creamos un texto debajo de cada punto para decirnos cuantos puntos tenemos en la zona
            GUIStyle texto = new GUIStyle();
            texto.fontStyle = FontStyle.Bold;
            texto.fontSize = 16;
            texto.normal.textColor = Color.black;
            //Asignamos el numero a la parte inferior derecha
            Vector3 alineamiento = Vector3.down * 0.3f + Vector3.right * 0.3f;
            //Creamos el texto
            Handles.Label(WaypointTarget.PosicionActual + WaypointTarget.Puntos[i] + alineamiento, $"{i + 1}", texto);
            //Verificamos si se han hecho cambios en el editor para guardarlos
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target,name:"Puntos de movimiento libre");
                WaypointTarget.Puntos[i] = nuevoPunto - WaypointTarget.PosicionActual;
            }
        }
    }

}

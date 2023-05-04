using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable] 
public class IA_Transicion 

//CONTROLA LA TRANCISION DE UN ESTADO A OTRO 
//SE LE DA EL ATRIBUTO  Serializable QUE SE ENCUENTRA EN EL  NAMESPACE SYSTEM, PARA QUE SE PUEDA VER EN EL INSPECTOR 
{
    //SE CREAN LAS VARIABLES PARA CONOCER EL ESTADO Y LA DECISION
    public IA_Decision Decision;
    public IA_Estado EstadoVerdadero; //SE GUARDA SI LA DECISION REGRESA VERDADERO
    public IA_Estado EstadoFalso; //SE GUARDA SI LA DECISION REGRESA FALSO
    
}
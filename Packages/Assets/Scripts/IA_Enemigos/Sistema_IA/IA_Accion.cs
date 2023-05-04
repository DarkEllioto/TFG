using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//LO QUE PUEDE EJECUTAR UN ENEMIGO
//SE EJECUTA COMO CLASE BASE
public abstract class IA_Accion : ScriptableObject
{
    //METODO ABSTRACTO PARA INVOCARLO CUANDO SE LLAME A LA CLASE 
   public abstract void Ejecutar(IA_Controller controller); 
 
}

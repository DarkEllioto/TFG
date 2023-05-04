using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SE EJECUTA COMO CLASE BASE
public  abstract class IA_Decision : ScriptableObject
{
    //METODO ABSTRACTO PARA IMPLEMENTARLO CUANDO SE LLAME A LA CLASE 
  public abstract bool Decidir(IA_Controller controller);
}
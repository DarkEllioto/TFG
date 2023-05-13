using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Creamos una enumeracion para todos los tipos de item
public enum tiposDeItems{
    Armas,
    Pociones,
    Pergaminos,
    Ingredientes,
    Tesoros

}
public class InventarioItem : ScriptableObject
{
    //Creamos nuestros headers para el inspector
    [Header("Parametros")]
    public string ID;
    public string Nombre;
    public Sprite Icono;
    //Añadimos un text area para poder añadir mas texto en caso de necesitarlo
    [TextArea]public string Descripcion;

    [Header("Informacion")]
    public tiposDeItems Tipo;
    //Creamos las variables que nos digan si son consumibles o equipables
    public bool esConsumible;
    public bool esAcumulable;
    //Ajustamos la cantidad maxima que podemos añadir en un slot de un solo objeto
    public int AcumulacionMax;
    [HideInInspector] public int cantidad;

    //Creamos un nuevo metodo que nos retorne una instancia por cada metodo que añadamos
    public InventarioItem CopiarItem()
    {
        InventarioItem nuevaInstancia = Instantiate(original: this);
        return nuevaInstancia;
    }
}

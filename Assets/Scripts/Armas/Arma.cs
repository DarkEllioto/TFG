using UnityEngine;

//SE AÑADE UN ENUMERADOR PARA LOS TIPOS DE ARMA
public enum TipoArma
{
    Magia,
    Melee
}

//se le da un atributo de CREATEASSETMENU
[CreateAssetMenu(menuName = "Personaje/Arma")]
public class Arma : ScriptableObject  
{
    //VARIABLES DE CONFIGURACION
    [Header("Config")] 
    public Sprite ArmaIcono;
    public Sprite IconoSkill;
    public TipoArma Tipo;
    public float Daño;

    //VARIABLES DE ARMA MAGICA
    [Header("Arma Magica")] 
  //  public Proyectil ProyectilPrefab;
    public float ManaRequerida;

    //VARIABLES PARA PONCERTAJE DE CRITICO Y BLOQUEO
    [Header("Stats")] 
    public float ChanceCritico;
    public float ChanceBloqueo;
}

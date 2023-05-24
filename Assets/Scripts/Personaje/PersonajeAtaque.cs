using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeAtaque : MonoBehaviour
{
    
    [Header("Stats")] 
    [SerializeField] private PersonajeStats stats;

    //SE OBTIENE LA REFERENCIA
     [Header("Pooler")] 
    [SerializeField] private ObjectPooler pooler;

    //SE CREA UNA PROPIEDAD DE TIPO ARMA
     public Arma ArmaEquipada { get; private set; }

     

    //SE CREA EL METODO EQUIPAR ARMA SE PASA COMO PARAMETRO UN ITEM ARMA
     public void EquiparArma(ItemArma armaPorEquipar)
    {
        //SE DECLARA QUE ARMA EQUIPADA ES IGUAL A ARMA EQUIPADA
        ArmaEquipada = armaPorEquipar.Arma;
        //SE VERIFICA SI EL ARMA ES DE TIPO MAGIA 
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            //SE CREA EL POOLER Y SE LE PASA EL PROYECTIL
            pooler.CrearPooler(ArmaEquipada.ProyectilPrefab.gameObject);
        }
        //SE LLAMA AL METODO DE LA CLASE STATS Y SE LE PASA LA REFERENCIA DEL ARMA
       stats.AñadirBonusPorArma(ArmaEquipada);
    }

    //METODO PARA REMOVER ARMA
     public void RemoverArma()
    {
        //SE VERIFICA SI EXISTE UN ARMA EQUIPADA 
        if (ArmaEquipada == null)
        {
            //SI ESTA NUL SE REGRESA
            return;
        }
        //CUANDO SE REMUEVE EL ARMA TAMBIEN SE REMUEVE EL POOLER 
        if (ArmaEquipada.Tipo == TipoArma.Magia)
        {
            pooler.DestruirPooler();
        }
          //SE LLAMA AL METODO DE LA CLASE STATS Y SE LE PASA LA REFERENCIA DEL ARMA
       stats.RemoverBonusPorArma(ArmaEquipada);
        //SI ES DISTINTO A NUL SE PONE EN NULO 
        ArmaEquipada = null;
    }
   
}

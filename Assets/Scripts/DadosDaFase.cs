using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DadosDaFase : MonoBehaviour{

    //Variavel estatica para acessar de qualquer lugar
    public static DadosDaFase main; 

    //Transform é usado para posição do objeto
    public Transform pontoInicio;
    public Transform[] caminho;

    private void Awake() {
        main = this;
    }

}

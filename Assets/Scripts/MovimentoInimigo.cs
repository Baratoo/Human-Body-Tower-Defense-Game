using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Video;

public class MovimentoInimigo : MonoBehaviour{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float velocidade = 2f;

    private Transform alvo;
    private int indexCaminho = 0;

    private void Start() {
        alvo = DadosDaFase.main.caminho[indexCaminho];
    }
    private void Update() {
        if(Vector2.Distance(alvo.position, transform.position) <= 0.1f){
            indexCaminho++;
            
            //Se chegar ao final destroi o inimigo
            if(indexCaminho == DadosDaFase.main.caminho.Length){
                SpawnDeInimigos.quandoInimigoMorrer.Invoke();
                Destroy(gameObject);
                return;
            } else {
                alvo = DadosDaFase.main.caminho[indexCaminho];
            }
        }
    }

    private void FixedUpdate() {
        Vector2 direcao = (alvo.position - transform.position).normalized;

        rb.velocity = direcao * velocidade;    
    }


}

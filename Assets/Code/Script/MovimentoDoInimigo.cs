using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoDoInimigo : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Rigidbody2D rb; // Referência ao Rigidbody2D do inimigo

    [Header("Atributos")]
    [SerializeField] private float velocidadeMovimento = 2f; // Velocidade de movimento do inimigo

    private Transform alvo; // Próximo ponto do caminho que o inimigo segue
    private int indiceCaminho = 0;// Índice do ponto atual no caminho
    private float velocidadeBase; // Armazena a velocidade base para reiniciar quando necessário

    // Variável estática para contar quantos inimigos chegaram ao fim
    public static int inimigosQueChegaramAoFim;

    private void Start() {
        velocidadeBase = velocidadeMovimento; // Armazena a velocidade base
        alvo = GerenciaNivel.main.caminho[indiceCaminho]; // Define o primeiro alvo do caminho
    }

    private void Update() {
        // Verifica se o inimigo está próximo ao ponto atual do caminho
        if (Vector2.Distance(alvo.position, transform.position) <= 0.1f) {
            indiceCaminho++; // Avança para o próximo ponto

            // Se o inimigo chegou ao fim do caminho
            if (indiceCaminho == GerenciaNivel.main.caminho.Length) {
                inimigosQueChegaramAoFim++; // Incrementa o contador de inimigos que chegaram ao fim
                GeradorDeInimigos.onEnemyDestroy.Invoke(); // Invoca o evento de destruição do inimigo
                Destroy(gameObject); // Destrói o inimigo
                return;
            } else {
                // Atualiza o alvo para o próximo ponto do caminho
                alvo = GerenciaNivel.main.caminho[indiceCaminho];
            }
        }
    }

    private void FixedUpdate() {
        // Calcula a direção para o próximo ponto e aplica a velocidade
        Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.velocity = direcao * velocidadeMovimento; // Move o inimigo na direção calculada
    }

    // Atualiza a velocidade de movimento do inimigo
    public void AtualizarVelocidade(float novaVelocidade) {
        velocidadeMovimento = novaVelocidade;
    }

    // Reseta a velocidade do inimigo para a velocidade base original
    public void ResetarVelocidade() {
        velocidadeMovimento = velocidadeBase;
    }
} 
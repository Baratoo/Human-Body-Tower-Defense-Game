using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TorretaLenta : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private LayerMask mascaraDeInimigos; // Máscara de detecção dos inimigos

    [Header("Atributos")]
    [SerializeField] private float alcanceAlvo = 5f; // Alcance para detectar inimigos
    [SerializeField] private float aps = 4f; // Ataques por segundo
    [SerializeField] private float tempoCongelamento = 1f; // Duração do efeito de lentidão

    private float tempoAteProximoAtaque; // Tempo até o próximo ataque

    private void Update() {
        tempoAteProximoAtaque += Time.deltaTime;

        // Verifica se é hora de aplicar o efeito de lentidão
        if (tempoAteProximoAtaque >= 1f / aps) {
            AplicarLentidaoNosInimigos();
            tempoAteProximoAtaque = 0f;
        }
    }

    private void AplicarLentidaoNosInimigos() { 
        // Faz um CircleCast ao redor da torreta para encontrar inimigos dentro do alcance
        RaycastHit2D[] acertos = Physics2D.CircleCastAll(transform.position, alcanceAlvo, (Vector2)transform.position, 0f, mascaraDeInimigos);

        if (acertos.Length > 0) {
            // Aplica lentidão a todos os inimigos detectados
            for (int i = 0; i < acertos.Length; i++) {
                RaycastHit2D acerto = acertos[i];

                MovimentoDoInimigo mi = acerto.transform.GetComponent<MovimentoDoInimigo>();
                mi.AtualizarVelocidade(0.5f); // Reduz a velocidade do inimigo

                StartCoroutine(ResetarVelocidadeInimigo(mi)); // Reseta a velocidade após o tempo de congelamento
            }
        }
    }

    private IEnumerator ResetarVelocidadeInimigo(MovimentoDoInimigo mi) {
        yield return new WaitForSeconds(tempoCongelamento); // Espera pelo tempo de congelamento

        mi.ResetarVelocidade(); // Reseta a velocidade do inimigo
    }

    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, alcanceAlvo); // Desenha um círculo no editor mostrando o alcance da torreta
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    [Header("Atributos")]
    [SerializeField] private int pontosDeVida = 2; // Pontos de vida do objeto
    [SerializeField] private int valorMoeda = 50; // Valor em moeda que o inimigo dá ao ser destruído

    private bool estaDestruido = false; // Verifica se o inimigo já foi destruído

    public void TomarDano(int dano) {
        pontosDeVida -= dano; // Reduz os pontos de vida do objeto

        // Se os pontos de vida chegarem a zero e o inimigo não estiver destruído
        if (pontosDeVida <= 0 && !estaDestruido) {
            GeradorDeInimigos.onEnemyDestroy.Invoke(); // Invoca o evento de destruição do inimigo
            GerenciaNivel.main.AumentarMoeda(valorMoeda); // Aumenta a quantidade de moeda do jogador
            estaDestruido = true; // Marca o inimigo como destruído
            Destroy(gameObject); // Destrói o inimigo
        }
    }
}
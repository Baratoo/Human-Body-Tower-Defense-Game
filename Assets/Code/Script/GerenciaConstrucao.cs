using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GerenciaConstrucao : MonoBehaviour
{
    public static GerenciaConstrucao main;

    [Header("Referências")]
    [SerializeField] private Torre[] torres; // Array de torres disponíveis para construção

    private int torreSelecionada = 0; // Índice da torre atualmente selecionada

    private void Awake() {
        main = this; // Define esta instância como a instância principal do GerenteDeConstrucao
    }

    public Torre ObterTorreSelecionada() {
        return torres[torreSelecionada]; // Retorna a torre atualmente selecionada
    }

    public void SelecionarTorre(int _torreSelecionada) {
        torreSelecionada = _torreSelecionada; // Define a torre selecionada com base no índice fornecido
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Terreno : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private SpriteRenderer sr; // Referência ao SpriteRenderer do terreno
    [SerializeField] private Color corAoPassarMouse; // Cor que aparece quando o mouse passa sobre o terreno

    public GameObject torreObj; // A torre que será construída no terreno
    public Torreta torreta;
    private Color corInicial; // Cor original do terreno

    private void Start() {
        corInicial = sr.color; // Armazena a cor original do terreno
    }

    private void OnMouseEnter() {
        if (torreObj != null) return; // Se já houver uma torre, não muda a cor do terreno

        sr.color = corAoPassarMouse; // Muda a cor do terreno ao passar o mouse
    }

    private void OnMouseExit() {
        sr.color = corInicial; // Volta para a cor original quando o mouse sai
    }

    private void OnMouseDown() {
        if (GerenciaUI.main.EstaAtivoUI()) return; //verificação para abrir/fechar botão de upgrade 

        if (torreObj != null) {
            torreta.AbrirAtualizarUI();
            return; // Verifica se já tem uma torre construída
        } 

        Torre torreParaConstruir = GerenciaConstrucao.main.ObterTorreSelecionada();

        // Verifica se o jogador tem dinheiro suficiente para construir a torre
        if (torreParaConstruir.custo > GerenciaNivel.main.moeda) {
            Debug.Log("Você não pode pagar por essa torre");
            return;
        }

        GerenciaNivel.main.GastarMoeda(torreParaConstruir.custo);

        // Constrói a torre no terreno
        torreObj = Instantiate(torreParaConstruir.prefab, transform.position, Quaternion.identity);
        torreta = torreObj.GetComponent<Torreta>();
        sr.color = corInicial; // Volta a cor normal do terreno após a construção da torre
    }
}
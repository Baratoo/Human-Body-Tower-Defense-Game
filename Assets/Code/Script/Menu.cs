using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] TextMeshProUGUI moedaUI; // Interface que exibe a quantidade de moeda
    [SerializeField] Animator anim; // Controlador de animações para o menu

    private bool menuAberto = true; // Verifica se o menu está aberto ou fechado

    public void AlternarMenu() {
        menuAberto = !menuAberto; // Alterna entre abrir e fechar o menu
        anim.SetBool("MenuOpen", menuAberto); // Define o estado do menu na animação
    }

    private void OnGUI() { 
        moedaUI.text = GerenciaNivel.main.moeda.ToString(); // Atualiza o texto da moeda na interface gráfica
    }

    public void DefinirSelecionado() {
        // Função para definir um item como selecionado (não implementada)
    }
}
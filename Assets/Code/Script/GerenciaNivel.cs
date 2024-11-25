using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciaNivel : MonoBehaviour
{
    public static GerenciaNivel main;

    public Transform pontoDeInicio;
    public Transform[] caminho;

    public int moeda;

    public int ondasConcluidas = 0;
    public int limiteOndas = 10;
    public int inimigosPermitidos = 100;

    private void Awake() {
        main = this;
    }

    private void Start() {
        moeda = 100;
    }

    public void AumentarMoeda(int quantidade) {
        moeda += quantidade;
    }

    public bool GastarMoeda(int quantidade) {
        if (quantidade <= moeda) {
            moeda -= quantidade;
            return true;
        } else {
            Debug.Log("Você não tem o suficiente para montar esse item");
            return false;
        }
    }

    public void VerificarEstadoJogo() {
        if (MovimentoDoInimigo.inimigosQueChegaramAoFim >= inimigosPermitidos) {
            Debug.Log("Você perdeu o jogo!");
            SceneManager.LoadScene("GameOverScene"); // Carrega a tela de derrota
        }

        if (ondasConcluidas >= limiteOndas) {
            Debug.Log("Você venceu o jogo!");
            SceneManager.LoadScene("VictoryScene"); // Carrega a tela de vitória
        }
    }
}
//using System.Threading.Tasks.Dataflow;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using Unity.VisualScripting;

public class Torreta : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform pontoDeRotacaoDaTorreta; // Ponto de rotação da torreta
    [SerializeField] private LayerMask mascaraDeInimigos; // Máscara de detecção dos inimigos
    [SerializeField] private GameObject prefabBala; // Prefab da bala que a torreta dispara
    [SerializeField] private Transform pontoDeDisparo; // Ponto de origem do disparo da torreta
    [SerializeField] private GameObject AtualizarUI; //Melhorar a Torre
    [SerializeField] private Button btnAtualizar; //Botão para melhorar a torre

    [Header("Atributos")]
    [SerializeField] private float alcanceAlvo = 5f; // Alcance da torreta para detectar inimigos
    [SerializeField] private float velocidadeRotacao = 5f; // Velocidade de rotação da torreta
    [SerializeField] private float tps = 1f; // Tiros por segundo (Tiros Por Segundo)
    [SerializeField] private int custoUpgradeBase = 100;

    private float tpsBase;
    private float alcanceAlvoBase;
    private Transform alvo; // Referência ao alvo atual da torreta
    private float tempoAteDisparo; // Tempo até o próximo disparo

    private int nivel = 1;

    void Start() {
        tpsBase = tps;
        alcanceAlvoBase = alcanceAlvo;

        btnAtualizar.onClick.AddListener(Upgrade);
      
    }
    

    private void Update() {
        if (alvo == null) {
            EncontrarAlvo(); // Procura um alvo se não houver
            return;
        }

        // Gira em direção ao alvo
        // RotateTowardsTarget();

        if (!VerificarSeAlvoEstaNoAlcance()) {
            alvo = null; // Se o alvo sair do alcance, deseleciona o alvo
        } else {
            tempoAteDisparo += Time.deltaTime;

            if (tempoAteDisparo >= 1f / tps) {
                Disparar(); // Dispara no alvo
                tempoAteDisparo = 0f;
            }
        }
    }

    private void Disparar() {
        GameObject objetoBala = Instantiate(prefabBala, pontoDeDisparo.position, Quaternion.identity); // Instancia a bala no ponto de disparo
        Bala scriptBala = objetoBala.GetComponent<Bala>();
        scriptBala.DefinirAlvo(alvo); // Define o alvo da bala
    }

    private void EncontrarAlvo() {
        // Faz um CircleCast ao redor da torreta para encontrar inimigos dentro do alcance
        RaycastHit2D[] acertos = Physics2D.CircleCastAll(transform.position, alcanceAlvo, (Vector2)transform.position, 0f, mascaraDeInimigos);

        if (acertos.Length > 0) {
            alvo = acertos[0].transform; // Define o primeiro inimigo encontrado como alvo
        }
    }

    private bool VerificarSeAlvoEstaNoAlcance() {
        return Vector2.Distance(alvo.position, transform.position) <= alcanceAlvo; // Verifica se o alvo ainda está dentro do alcance
    }

    private void RotateTowardsTarget() {
        // Calcula o ângulo para girar a torreta em direção ao alvo
        float angulo = Mathf.Atan2(alvo.position.y - transform.position.y, alvo.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion rotacaoAlvo = Quaternion.Euler(new Vector3(0f, 0f, angulo));
        pontoDeRotacaoDaTorreta.rotation = Quaternion.RotateTowards(pontoDeRotacaoDaTorreta.rotation, rotacaoAlvo, velocidadeRotacao * Time.deltaTime);
    }

    public void AbrirAtualizarUI () {
        AtualizarUI.SetActive(true);
    }

    public void FecharAtualizarUI () {
        AtualizarUI.SetActive(false);
        GerenciaUI.main.AlteraStatusUI(false);
    }

    public void Upgrade() {
        Debug.Log("Entrou no Upgrade");
        if (CalculaCusto() > GerenciaNivel.main.moeda) return;

        GerenciaNivel.main.AumentarMoeda(CalculaCusto());

        nivel++;
        tps = CalculaTPS();
        alcanceAlvo = CalculaAlcanceAlvo();

        FecharAtualizarUI();
        Debug.Log("Saiu fo Upgrade");
    }

    private int CalculaCusto() {
        return Mathf.RoundToInt(custoUpgradeBase * Mathf.Pow(nivel, 0.8f));
    }

    private float CalculaTPS() {
        return tpsBase * Mathf.Pow(nivel, 0.5f);
    }

    private float CalculaAlcanceAlvo() {
        return alcanceAlvoBase * Mathf.Pow(nivel, 0.4f);
    }

    // Desenha uma linha de gizmo no editor para mostrar o alcance da torreta
    private void OnDrawGizmosSelected() {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, alcanceAlvo);
    }
}
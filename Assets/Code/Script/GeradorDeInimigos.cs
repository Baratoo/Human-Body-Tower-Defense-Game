    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GeradorDeInimigos : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private GameObject[] inimigoPrefabs; // Array de prefabs dos inimigos

    [Header("Atributos")]
    [SerializeField] private int inimigosBase = 8; // Número base de inimigos por onda
    [SerializeField] private float inimigosPorSegundo = 0.5f; // Inimigos gerados por segundo
    [SerializeField] private float tempoEntreOndas = 5f; // Tempo de espera entre as ondas
    [SerializeField] private float fatorDeDificuldade = 0.75f; // Fator de escala da dificuldade

    [Header("Eventos")]
    public static UnityEvent onEnemyDestroy = new UnityEvent(); // Evento acionado quando um inimigo é destruído

    private int ondaAtual = 1; // Número da onda atual
    private float tempoDesdeUltimoSpawn; // Tempo desde o último inimigo gerado
    private int inimigosVivos; // Número de inimigos vivos
    private int inimigosRestantesParaSpawn; // Número de inimigos restantes para serem gerados
    private bool estaGerando = false; // Indica se a geração de inimigos está ativa

    private void Awake() {
        onEnemyDestroy.AddListener(inimigoDestruido); // Escuta o evento de destruição de inimigos
    }

    private void Start() {
        StartCoroutine(IniciarOnda()); // Inicia a primeira onda
    }

    private void Update() {
        if (!estaGerando) return;

        tempoDesdeUltimoSpawn += Time.deltaTime;

        // Verifica se é hora de gerar outro inimigo e se ainda há inimigos para gerar
        if (tempoDesdeUltimoSpawn >= (1f / inimigosPorSegundo) && inimigosRestantesParaSpawn > 0) {
            gerarInimigo();
            inimigosRestantesParaSpawn--;
            inimigosVivos++;
            tempoDesdeUltimoSpawn = 0f;
        }

        // Verifica se todos os inimigos da onda foram gerados e destruídos
        if (inimigosVivos == 0 && inimigosRestantesParaSpawn == 0) {
            FinalizarOnda();
        }
    }

    private void inimigoDestruido() {
        inimigosVivos--; // Decrementa o número de inimigos vivos quando um inimigo é destruído
    }

    private IEnumerator IniciarOnda() {
        Debug.Log("Inicia Onda");
        yield return new WaitForSeconds(tempoEntreOndas); // Espera um tempo antes de iniciar a próxima onda

        estaGerando = true;
        inimigosRestantesParaSpawn = inimigosPorOnda(); // Define quantos inimigos serão gerados nesta onda
    }

    private void FinalizarOnda() {
        estaGerando = false;
        tempoDesdeUltimoSpawn = 0f;
        ondaAtual++;
        GerenciaNivel.main.ondasConcluidas++; // Incrementa as ondas concluídas
        GerenciaNivel.main.VerificarEstadoJogo(); // Verifica se o jogo foi vencido
        StartCoroutine(IniciarOnda());
    }

    private void gerarInimigo() {
        int indice = Random.Range(0, inimigoPrefabs.Length); // Escolhe um prefab de inimigo aleatório
        GameObject prefabParaGerar = inimigoPrefabs[indice]; 
        Instantiate(prefabParaGerar, GerenciaNivel.main.pontoDeInicio.position, Quaternion.identity); // Gera o inimigo no ponto de início do nível
    }

    private int inimigosPorOnda() {
        // Calcula o número de inimigos nesta onda com base na dificuldade e na onda atual
        return Mathf.RoundToInt(inimigosBase * Mathf.Pow(ondaAtual, fatorDeDificuldade));
    }
}
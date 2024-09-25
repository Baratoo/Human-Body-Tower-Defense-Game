using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnDeInimigos : MonoBehaviour
{


    [Header("References")]
    [SerializeField] private GameObject[] prefabsDeInimigos;


    [Header("Attributes")]

    //Numero de inimigos da primeira wave
    [SerializeField] private int inimigosBase = 8;
    [SerializeField] private float inimigosPorSegundo = 0.5f;
    [SerializeField] private float tempoEntreWaves = 5f;
    [SerializeField] private float escalaDificuldade = 0.75f;

    [Header("Events")]
    public static UnityEvent quandoInimigoMorrer = new UnityEvent(); //Evento

    private int waveAtual = 1;
    private float tempoDesdeUltimoSpawn;
    private int inimigosVivos;
    private int inimigosFaltamSpanwnar;
    private bool gerandoInimigos = false;

    private void Awake() {
        //Evento para contabilizar que o inimigo morreu, chamado no "MovimentoInimigo",
        //Utilizando Invoke()
        quandoInimigoMorrer.AddListener(InimigoMorto);
    }

    private void Start() {
        StartCoroutine(StartWave());
    }

    

    private void Update() {
         if(!gerandoInimigos) return;

         tempoDesdeUltimoSpawn += Time.deltaTime;

         if(tempoDesdeUltimoSpawn >= (1f / inimigosPorSegundo) && inimigosFaltamSpanwnar > 0){
            SpawnaInimigo();
            inimigosFaltamSpanwnar--;
            inimigosVivos++;
            tempoDesdeUltimoSpawn = 0f;
         }

         if(inimigosVivos == 0 && inimigosFaltamSpanwnar == 0){
            FimDaWave();
         }

    }

    //IEnumerator é usado para que possa ocorrer uma pausa durante o jogo
    private IEnumerator StartWave() {
        yield return new WaitForSeconds(tempoEntreWaves);
        gerandoInimigos = true;
        inimigosFaltamSpanwnar = InimigosPorWave();
    }

    private void FimDaWave(){
        
        gerandoInimigos = false;
        tempoDesdeUltimoSpawn = 0f;
        waveAtual++;
        StartCoroutine(StartWave());
    }

    private void InimigoMorto(){
        inimigosVivos--;
    }

    private void SpawnaInimigo(){
        GameObject PrefabParaSpawn = prefabsDeInimigos[0];
        //Instantiate - pega as prefabs dos inimigos
        Instantiate(PrefabParaSpawn, DadosDaFase.main.pontoInicio.position, Quaternion.identity); 
    }

    private int InimigosPorWave(){
        //Aumenta o numero de inimigos
        return Mathf.RoundToInt(inimigosBase * Mathf.Pow(waveAtual, escalaDificuldade));
    }

}

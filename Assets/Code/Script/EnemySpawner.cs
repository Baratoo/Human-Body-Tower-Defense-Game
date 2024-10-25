    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
 [Header("References")]
 [SerializeField] private GameObject[] enemyPrefabs;

 [Header("Atributes")]
 [SerializeField] private int baseEnemys = 8;
 [SerializeField] private float enemiesPerSecond = 0.5f; 
 [SerializeField] private float timesBetweenWave = 5f;
 [SerializeField] private float difficultyScalingFactor = 0.75f;

[Header("Events")]
 public static UnityEvent onEnemyDestroy = new UnityEvent();

 private int currentWave = 1;
 private float timeSinceLastSpawn;
 private int enemiesAlive;
 private int enemiesLeftToSpawn;
 private bool isSpawning = false;


 private void Awake() {
    onEnemyDestroy.AddListener(enemyDestroyed);
 }

private void Start() {
    StartCoroutine(StartWave());
}

private void Update() {
    if(!isSpawning) return;

    timeSinceLastSpawn += Time.deltaTime;

    if(timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0) {
        spawnEnemy();
        enemiesLeftToSpawn --;
        enemiesAlive ++;
        timeSinceLastSpawn = 0f;
    }

    if(enemiesAlive == 0 && enemiesLeftToSpawn == 0) {
        EndWave();
    }
}

private void enemyDestroyed() {
    enemiesAlive --;
}

 private IEnumerator StartWave() {
    yield return new WaitForSeconds(timesBetweenWave);

    isSpawning = true;
    enemiesLeftToSpawn = enemiesPerWave();
 }

 private void EndWave() {
    isSpawning = false;
    timeSinceLastSpawn = 0f;
    currentWave ++;
    StartCoroutine(StartWave());
 }

 private void spawnEnemy() {
    int index = Random.Range(0, enemyPrefabs.Length);
    GameObject prefabToSpawn = enemyPrefabs[index];
    Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
 }

 private int enemiesPerWave() {
    return Mathf.RoundToInt(baseEnemys * Mathf.Pow(currentWave, difficultyScalingFactor));
 }

}
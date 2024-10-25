using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretSlomo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; //Attack per second
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;

    private void Update() {

        timeUntilFire += Time.deltaTime;

        if(timeUntilFire >= 1f / aps) {
            //Debug.Log("Lentidão");
            freezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void freezeEnemies() { 
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if(hits.Length > 0) {
            for (int i = 0; i < hits.Length; i++)
            {
                
                RaycastHit2D hit = hits[i];

                EnemyMovments em = hit.transform.GetComponent<EnemyMovments>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovments em) {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

    private void OnDrawGizmosSelected() {

        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}

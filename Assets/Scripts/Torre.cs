using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Torre : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private GameObject bulletPrefab; // Projétil
    [SerializeField] private Transform firingPoint;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 2f; //Alcance da torre
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float bps = 1f; //Bulelts Per Second

   private Transform target;
    private float timeUntilFire;

    // Start is called before the first frame update
    void Start()
    {

        
    }

	//Faz um desenho em volta da torre, do alcance dela
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);

    }

    //Atualiza a torre para que se não tiver um alvo, procurar por outro
    void Update()
    {
        if (target == null || Vector2.Distance(transform.position, target.position) > targetingRange)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else {
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / bps) {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    }

    private void Shoot() {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
    }
	
	//Ajusta a torre para olhar para o alvo
    private void RotateTowardsTarget()
    {/*
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = targetRotation;*/

        if (target == null) return; // Verifica se há um alvo

            // Calcula a direção do alvo
            Vector2 direction = target.position - transform.position;

            // Converte a direção em um ângulo em graus
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Cria uma rotação a partir do ângulo calculado
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

            // Aplica a rotação ao transform da torre
            transform.rotation = targetRotation;
    }

    private bool CheckTargetIsInRange() {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

	//Procura pelo inimigo dentro da area determinada
    private void FindTarget() {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0) {
            target = hits[0].transform;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Rigidbody2D rb; // Referência ao componente Rigidbody2D

    [Header("Atributos")]
    [SerializeField] private float velocidadeDaBala = 5f; // Velocidade da bala
    [SerializeField] private int danoDaBala = 1; // Dano que a bala causa

    public Transform alvo; // Referência ao alvo da bala

    public void DefinirAlvo(Transform _alvo) {
        alvo = _alvo; // Define o alvo da bala
    }

    private void FixedUpdate() {
        if (!alvo) return; // Se não houver alvo, não faz nada

        // Calcula a direção do alvo e move a bala
        Vector2 direcao = (alvo.position - transform.position).normalized;
        rb.velocity = direcao * velocidadeDaBala; // Define a velocidade da bala na direção do alvo
    }

    private void OnCollisionEnter2D(Collision2D outro) {
        // Quando a bala colide com algo, causa dano ao inimigo e se destrói
        outro.gameObject.GetComponent<Vida>().TomarDano(danoDaBala);
        Destroy(gameObject); // Destrói a bala após causar dano
    }

    /*private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(alvo.position.y - transform.position.y, alvo.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }*/
}
    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

     [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1; 

     public Transform target;

    public void SetTarget(Transform _target) {
        target = _target;
    }

     private void FixedUpdate() {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject./*transform.*/GetComponent<Health>().TakeDamage(bulletDamage);
        Destroy(gameObject);
    }

    /*private void RotateTowardsTarget() {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        turretRotationPoint.rotation = Quaternion.RotateTowards(turretRotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }*/
}

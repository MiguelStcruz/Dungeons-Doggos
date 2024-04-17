using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private Vector2 attackRange = new Vector2(1.5f, 2.0f);

    public Transform attackPoint;
    public LayerMask enemyLayer;

    void Damage()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapBoxAll(attackPoint.position, attackRange, 0.0f, enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            enemy.GetComponent<Enemy>().TakeDamage();
        }
    }

    void Done()
    {
        Destroy(gameObject);
    }
}

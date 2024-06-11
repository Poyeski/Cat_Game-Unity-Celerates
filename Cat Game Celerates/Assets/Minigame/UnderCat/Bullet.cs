using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public int damage = 10;

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            int finalDamage = damage;
            BulletHellManager bulletHellManager = FindObjectOfType<BulletHellManager>();
            if (bulletHellManager.isDefensePhase)
            {
                finalDamage = Mathf.CeilToInt(damage * 0.5f);
            }
            other.GetComponent<Player>().TakeDamage(finalDamage);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Boundary"))
        {
            Destroy(gameObject);
        }
    }
}
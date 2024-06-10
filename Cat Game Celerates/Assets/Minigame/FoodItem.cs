using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public int points = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bowl"))
        {
            GameManager.instance.AddScore(points);
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            GameManager.instance.MissFood();
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorControl : MonoBehaviour
{
    public GameObject Floor;
    private Animator FloorAnimator;

    void Start()
    {
        if (Floor != null)
        {
            FloorAnimator = Floor.GetComponent<Animator>();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (FloorAnimator != null)
            {
                FloorAnimator.SetBool("OnFloor", true);
            }
        }
    }
}


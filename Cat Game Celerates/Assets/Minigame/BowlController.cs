using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlController : MonoBehaviour
{
    public float speed = 40f;

    void Update()
    {
        float move = Input.GetAxis("Mouse X") * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(move, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, -7.5f, 7.5f);
        transform.position = newPosition;
    }
}
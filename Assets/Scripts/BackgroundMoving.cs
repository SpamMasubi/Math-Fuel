using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoving : MonoBehaviour
{

    public float speed = 1f; //for background speedmovement
    public float clamppos; // for clamping position

    private Vector3 startPos; //get our first position
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, clamppos);
        transform.position = startPos + Vector3.left * newPosition;
    }
}

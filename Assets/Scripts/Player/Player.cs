using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    private float hMove, vMove;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        hMove = Input.GetAxis("Horizontal");
        vMove = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(hMove, vMove) * speed;
    }
}

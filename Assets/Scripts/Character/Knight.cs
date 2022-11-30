using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private ParticleSystem bloodPS;

    [SerializeField]
    private float speed = 5f;
    private float hMove, vMove;
    private bool movable;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bloodPS = GetComponentInChildren<ParticleSystem>();

        hMove = vMove = 0f;
        movable = true;
    }

    private void Update()
    {
        if (movable)
        {
            hMove = Input.GetAxis("Horizontal");
            vMove = Input.GetAxis("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(GetStuck());
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            bloodPS.Play();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movable ? new Vector2(hMove, vMove).normalized * speed : Vector2.zero;
    }

    private IEnumerator GetStuck()
    {
        StartStuck();
        yield return new WaitForSeconds(0.2f);
        RecoverFromStuck();
    }

    public void StartStuck()
    {
        animator.speed = 0f;
        movable = false;
    }

    public void RecoverFromStuck()
    {
        animator.speed = 1f;
        movable = true;
    }
}

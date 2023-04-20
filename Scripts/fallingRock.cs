using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingRock : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * fallSpeed * (Time.fixedDeltaTime * 100);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<axeController>().dieImmediate();
        }
        else if (collision.tag.Equals("deathCol"))
        {
            Destroy(gameObject);
        }
    }
}

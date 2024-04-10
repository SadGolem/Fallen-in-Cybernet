using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadItem : MonoBehaviour
{
    private DropsMiniGameController controller;
    private void Start()
    {
        controller = FindObjectOfType<DropsMiniGameController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            controller.UnCounter();
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            controller.UnCounter();
            Destroy(this.gameObject);
        }
    }
}

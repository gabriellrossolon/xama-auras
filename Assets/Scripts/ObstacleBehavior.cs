using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.GetComponent<PlayerMove>();

            if (playerMove.isDashing)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(ReenableCollider());
            }
        }
    }

    private IEnumerator ReenableCollider()
    {
        yield return new WaitForSeconds(0.5f); // Tempo antes de reativar o collider
        GetComponent<BoxCollider2D>().enabled = true; // Reativa o collider
    }
}

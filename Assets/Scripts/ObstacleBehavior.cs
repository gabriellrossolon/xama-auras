using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    private BoxCollider2D obstacleCollider;

    private void Start()
    {
        obstacleCollider = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.GetComponent<PlayerMove>();

            if (playerMove.isDashing)
            {
                obstacleCollider.enabled = false;
                StartCoroutine(ReenableCollider());
            }
        }
    }

    private IEnumerator ReenableCollider()
    {
        yield return new WaitForSeconds(0.5f);
        obstacleCollider.enabled = true;
    }
}

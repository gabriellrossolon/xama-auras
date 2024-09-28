using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyMove : MonoBehaviour
{
    private GameObject playerObject;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (playerObject != null)
        {
            // Verifica a posição do jogador em relação ao inimigo
            if (playerObject.transform.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // Olhar para a direita
            }
            else
            {
                spriteRenderer.flipX = true; // Olhar para a esquerda
            }
        }
    }
}

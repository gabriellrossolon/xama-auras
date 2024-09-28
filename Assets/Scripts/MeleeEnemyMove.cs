using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMove : MonoBehaviour
{
    private EntityStats entityStats;
    private Animator anim;
    private GameObject playerObject;
    private SpriteRenderer spriteRenderer;
    public float detectionRange = 5f;

    [HideInInspector] public Rigidbody2D rb; // Referência ao Rigidbody2D

    void Start()
    {
        entityStats = GetComponent<EntityStats>();
        playerObject = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>(); // Obtém o Rigidbody2D
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FlipSprite();
        // Verifica a distância até o jogador
        float distanceToPlayer = Vector2.Distance(transform.position, playerObject.transform.position);

        if (distanceToPlayer < detectionRange)
        {
            MoveTowardsPlayer();
        }
    }
    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerObject.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, playerObject.transform.position);

        if (distanceToPlayer > detectionRange)
        {
            rb.velocity = Vector2.zero; // Para o movimento se fora do alcance
            anim.SetBool("isWalk", false);
        }
        else
        {
            rb.velocity = direction * entityStats.moveSpeed;
            anim.SetBool("isWalk", true);
        }
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

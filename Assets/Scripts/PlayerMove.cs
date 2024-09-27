using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerMove : MonoBehaviour
{
    private EntityStats entityStats;
    private Rigidbody2D rb;
    private SkillController skillController;
    private Animator anim;

    public float dashForce = 10f; // Força do dash
    public float dashDuration = 0.2f; // Duração do dash
    public bool isDashing = false;
    public bool canDash = true;
    private float dashCooldown;
    public int dashCdr;

    private float lastInputX;
    private float lastInputY;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        entityStats = GetComponent<EntityStats>();
        skillController = GetComponent<SkillController>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDashing)
        {
            Movement();
            Dash();
        }
        DashCooldown();

        Animate();
    }

    private void Movement()
    {
        // Captura as entradas do jogador
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Cria um vetor de movimento
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // Define a velocidade do Rigidbody com base nos stats da entidade
        rb.velocity = movement * entityStats.moveSpeed;

        if (moveHorizontal == 0 && moveVertical == 0)
        {
            anim.SetBool("isWalking", false);
            return;
        }

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            anim.SetBool("isWalking", true);

            lastInputX = moveHorizontal;
            lastInputY = moveVertical;
        }   
    }

    private void Animate()
    {
        anim.SetFloat("MoveX", lastInputX);
        anim.SetFloat("MoveY", lastInputY);
    }

    private void Dash()
    {
        // Captura as entradas do jogador
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        // Cria um vetor de movimento
        Vector2 movement = new Vector2(moveHorizontal, moveVertical).normalized;

        // Verifica se o jogador pressionou a tecla de dash e se está se movendo
        if (movement.magnitude > 0 && Input.GetKeyDown(KeyCode.Space) && canDash && skillController.canUseDash) // Mude a tecla conforme necessário
        {
            StartCoroutine(PerformDash(movement));
            canDash = false;
            dashCooldown = 0;
        }
    }

    private IEnumerator PerformDash(Vector2 direction)
    {
        isDashing = true;

        // Adiciona força na direção do dash
        rb.AddForce(direction * dashForce, ForceMode2D.Impulse);

        // Espera durante a duração do dash
        yield return new WaitForSeconds(dashDuration);

        // Opcional: pode retornar a velocidade para 0 após o dash
        rb.velocity = Vector2.zero;

        isDashing = false;
    }

    private void DashCooldown()
    {
        if (dashCooldown >= dashCdr && !canDash)
        {
            canDash = true;
            dashCooldown = 0;
        } else
        {
            dashCooldown += Time.deltaTime;
        }
    }
}


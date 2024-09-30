using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStats : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public int maxHp;
    public int hp;

    public int attackDamage;

    public int armorPoints;

    public int moveSpeed;

    public float attackSpeed;

    private Color originalColor;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        hp = maxHp;
    }

    public void DoDamage(int attackGet)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            if (armorPoints > attackGet)
            {
                Debug.Log("Não levou dano");
            }
            else
            {
                originalColor = this.spriteRenderer.color;

                spriteRenderer.color = Color.red;
                StartCoroutine(BackOriginalColor());
                hp -= (attackGet - armorPoints);
            }
        }
        else
        {
            hp -= attackGet;

            spriteRenderer.color = Color.red;
            StartCoroutine(BackToWhiteColor());
        }

        Death();
    }

    private IEnumerator BackOriginalColor()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;  
    }

    private IEnumerator BackToWhiteColor()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private void Death()
    {
        if (hp <= 0)
        {
            if (this.gameObject.CompareTag("Player"))
            {
                HudManager.Instance.GameOverHud.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                if (this.gameObject.GetComponent<EnemyRangedAttack>() != null)
                {
                    this.gameObject.GetComponent<EnemyRangedAttack>().enabled = false;
                }
                
                if (this.gameObject.GetComponent<EnemyMeeleAttack>() != null)
                {
                    this.gameObject.GetComponent<EnemyMeeleAttack>().enabled = false;
                }
                
                if (this.gameObject.GetComponent<MeleeEnemyMove>() != null)
                {
                    this.gameObject.GetComponent<MeleeEnemyMove>().enabled = false;
                }

                if (this.gameObject.GetComponent<RangedEnemyMove>() != null)
                {
                    this.gameObject.GetComponent<RangedEnemyMove>().enabled = false;
                }

                if(this.gameObject.GetComponent<CapsuleCollider2D>() != null)
                {
                    this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                }

                if (this.gameObject.GetComponent<BoxCollider2D>() != null)
                {
                    this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }



                //Destroy(this.gameObject, 1f);
            }
            this.GetComponent<Animator>().SetTrigger("isDeath");
        }
    }
}


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

            originalColor = this.spriteRenderer.color;

            spriteRenderer.color = Color.red;
            StartCoroutine(BackOriginalColor());
        }

        
    }

    private IEnumerator BackOriginalColor()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = originalColor;  
    }
}

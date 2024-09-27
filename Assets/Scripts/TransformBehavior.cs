using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBehavior : MonoBehaviour
{
    private SkillController skillController;
    private EntityStats entityStats;
    private SpriteRenderer spriteRenderer;

    public bool canTransform = true;
    private float transformCooldown;
    public int transformCdr;

    public string actualTransformation;

    private void Start()
    {
        canTransform = true;
        skillController = GetComponent<SkillController>();
        entityStats = GetComponent<EntityStats>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        TransformationCooldown();
        TransformationExecuting();     
    }

    private void TransformationCooldown()
    {
        if (transformCooldown >= transformCdr && !canTransform)
        {
            canTransform = true;
            transformCooldown = 0;
        }
        else
        {
            transformCooldown += Time.deltaTime;
        }
    }

    private void TransformationExecuting()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && canTransform)
        {
            Debug.Log("Transformou em Urso");
            canTransform = false;
            transformCooldown = 0;

            actualTransformation = "Urso";

            TransformationVerifs();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canTransform)
        {
            Debug.Log("Transformou em Tartaruga");
            canTransform = false;
            transformCooldown = 0;

            actualTransformation = "Tartaruga";

            TransformationVerifs();
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canTransform)
        {
            Debug.Log("Transformou em Leopardo");
            canTransform = false;
            transformCooldown = 0;

            actualTransformation = "Leopardo";

            TransformationVerifs();
        }


    }

    private void TransformationVerifs()
    {
        if (actualTransformation == "Leopardo")
        {
            entityStats.moveSpeed = 6;
            entityStats.armorPoints = 0;
            skillController.canUseDash = true;
            spriteRenderer.color = new Color(0.831f, 0.831f, 0f);
        }
        else if (actualTransformation == "Tartaruga")
        {
            entityStats.moveSpeed = 1;
            entityStats.armorPoints = 5;
            skillController.canUseDash = false;
            spriteRenderer.color = new Color(0.073f, 0.408f, 0.063f);
        }
        else if (actualTransformation == "Urso")
        {
            entityStats.moveSpeed = 3;
            entityStats.armorPoints = 0;
            skillController.canUseDash = false;
            spriteRenderer.color = new Color(0.333f, 0.176f, 0.059f);
        }
        else if (actualTransformation == "Human")
        {
            spriteRenderer.color = Color.white;
            entityStats.moveSpeed = 4;
        }
    }
}

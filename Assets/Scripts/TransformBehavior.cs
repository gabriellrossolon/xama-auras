using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBehavior : MonoBehaviour
{
    private SkillController skillController;
    private EntityStats entityStats;

    public bool canTransform = true;
    private float transformCooldown;
    public int transformCdr;

    public string actualTransformation;

    private void Start()
    {
        skillController = GetComponent<SkillController>();
        entityStats = GetComponent<EntityStats>();
    }
    void Update()
    {
        TransformationCooldown();
        TransformationExecuting();

        TransformationVerifs();
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
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && canTransform)
        {
            Debug.Log("Transformou em Tartaruga");
            canTransform = false;
            transformCooldown = 0;

            actualTransformation = "Tartaruga";
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3) && canTransform)
        {
            Debug.Log("Transformou em Leopardo");
            canTransform = false;
            transformCooldown = 0;

            actualTransformation = "Leopardo";
        }
    }

    private void TransformationVerifs()
    {
        if (actualTransformation == "Leopardo")
        {
            entityStats.moveSpeed = 6;
            skillController.canUseDash = true;
        }
        else if (actualTransformation == "Tartaruga")
        {
            entityStats.moveSpeed = 1;
        }
        else if (actualTransformation == "Urso")
        {
            entityStats.moveSpeed = 3;
        }
        else
        {
            entityStats.moveSpeed = 4;
            skillController.canUseDash = false;
        }
    }
}

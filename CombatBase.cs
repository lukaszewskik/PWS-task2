﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    float health = 100;
    float damage = 20;


    List<CombatBase> targets = new List<CombatBase>();

    public void TryAttack()
    {        
        if (targets.Count > 0)
        {
            float distance;
            float minDistance = (targets[0].transform.position - this.transform.position).magnitude;
            CombatBase currentTarget = targets[0];

            foreach (CombatBase target in targets)
            {
                distance = (target.transform.position - this.transform.position).magnitude;

                if (distance < minDistance)
                {
                    currentTarget = target;
                }
            }

            currentTarget.TakeDamage(damage);
            if (currentTarget.IsDead())
            {
                targets.Remove(currentTarget);
            }
        }       
    }

    void TakeDamage(float dmg)
    {
        health -= dmg;

        if (IsDead())
        {
            Destroy(this.gameObject);
        }
   
    }

    public bool IsDead()
    {
        bool dead = false;
        if (health <= 0)
        {
            dead = true;
        }
        return dead;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject target = collision.gameObject;


        if (target.GetComponent<CombatBase>() && !target.CompareTag(this.gameObject.tag))
        {
            targets.Add(target.GetComponent<CombatBase>());
        }       
    }

    private void OnCollisionExit(Collision collision)
    {
        targets.Remove(collision.gameObject.GetComponent<CombatBase>());
    }
}

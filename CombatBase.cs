using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    float health;
    float damage;
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
        }       
    }

    void TakeDamage(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            Debug.Log("dead");
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        if (collision.gameObject.GetComponent<CombatBase>())
        {
            targets.Add(collision.gameObject.GetComponent<CombatBase>());
        }       
    }

    private void OnCollisionExit(Collision collision)
    {
        targets.Remove(collision.gameObject.GetComponent<CombatBase>());
    }
}

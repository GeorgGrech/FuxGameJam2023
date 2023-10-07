using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFighter : MonoBehaviour
{
    [SerializeField] public int damageAmount = 10;
    public GameObject hitbox;

    public LayerMask enemyLayer; // Set this in the Inspector to the layer where your enemies are.

    [SerializeField] private float attackFrequency;

    private Coroutine attackCycle;
    
    void Attack()
    {
        // Play the attack animation
        //anim.SetTrigger("attack");

        // Get the position of the hitbox.
        Vector3 hitboxPosition = hitbox.transform.position;

        // Detect enemies in front of the character using Physics.OverlapBox.
        Collider[] hitColliders = Physics.OverlapBox(hitboxPosition, hitbox.transform.localScale / 2, Quaternion.identity, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the hit object is an enemy 
            if (/*hitCollider.CompareTag("Enemy") && */ !hitCollider.isTrigger)
            {
                // Deal damage to the enemy 
                Health Health = hitCollider.GetComponent<Health>();
                if (Health != null)
                {
                    Debug.Log("Dealing Damage");

                    Health.TakeDamage(damageAmount);
                }
            }
        }
    }

    public void StartAttack()
    {
        if (attackCycle == null)
        {
            attackCycle = StartCoroutine(AttackCycle());
        }
    }

    public void StopAttack()
    {
        if(attackCycle != null)
        {
            StopCoroutine(attackCycle);
        }
        attackCycle = null;
    }

    private IEnumerator AttackCycle()
    {
        while (true)
        {
            Attack();
            Debug.Log("Attack! "+name);
            yield return new WaitForSeconds(attackFrequency);
        }
    }

}

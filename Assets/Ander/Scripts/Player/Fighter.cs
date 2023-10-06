using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public GameObject hitbox; // Reference to the hitbox GameObject.

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    [SerializeField] private float attackCooldown = 0.5f;
    private bool canAttack = true;
    public LayerMask enemyLayer; // Set this in the Inspector to the layer where your enemies are.

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }
    }

    void Attack()
    {
        // Play the attack animation here if you have one.

        // Get the position of the hitbox.
        Vector3 hitboxPosition = hitbox.transform.position;

        // Detect enemies in front of the character using Physics.OverlapBox.
        Collider[] hitColliders = Physics.OverlapBox(hitboxPosition, hitbox.transform.localScale / 2, Quaternion.identity, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the hit object is an enemy (you can use tags or layers to identify enemies).
            if (hitCollider.CompareTag("Enemy"))
            {
                // Deal damage to the enemy (you need to implement this part in your enemy script).
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    Debug.Log("Enemy Taking Damage");
                    enemyHealth.TakeDamage(10); // Adjust the damage amount as needed.
                }
            }
        }

        // Set a cooldown to limit the attack rate.
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
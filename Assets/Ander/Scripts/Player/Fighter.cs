using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private Animator anim;
    public GameObject hitbox;
    public GameObject defendSphere;
    public PlayerMoveDemoAnder playerMoveDemoAnder; // Reference to the PlayerMoveDemoAnder script.
    [SerializeField] public int damageAmount = 10;

    private Coroutine AttackCourotine;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    [SerializeField] private float attackCooldown = 0.5f;
    public bool canAttack = true;
    public bool isDefending = false;
    public LayerMask enemyLayer; // Set this in the Inspector to the layer where your enemies are.


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Defend();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopDefending();
        }
    }

    void Attack()
    {
        // Play the attack animation


        // Get the position of the hitbox.
        Vector3 hitboxPosition = hitbox.transform.position;

        // Detect enemies in front of the character using Physics.OverlapBox.
        Collider[] hitColliders = Physics.OverlapBox(hitboxPosition, hitbox.transform.localScale / 2, Quaternion.identity, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the hit object is an enemy 
            if (hitCollider.CompareTag("Enemy"))
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

        // Set a cooldown to limit the attack rate.
        AttackCourotine = StartCoroutine(AttackCooldown());


    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }


     void Defend()
    {
        canAttack = false;
        // Set the character to defend.
        isDefending = true;

        StopCoroutine(AttackCourotine); // Check if AttackCoroutine is not null before stopping it.
        
        playerMoveDemoAnder.DefendingSpeed(); // Call the DefendingSpeed method from PlayerMoveDemoAnder.                                      
        defendSphere.SetActive(true); // Show the defense sphere.

    }

    void StopDefending()
    {
        // Reset character's state to normal.
        StartCoroutine(AttackCooldown());
        canAttack = true;
        isDefending = false;

        playerMoveDemoAnder.ReturnToNormalSpeed(); // Call the ReturnToNormalSpeed method from PlayerMoveDemoAnder.
        defendSphere.SetActive(false);// Show the defense sphere.

    }

   
}
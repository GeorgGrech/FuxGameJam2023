using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public GameObject defendSphere;
    private PlayerMove playerMove; // Reference to the playerMove script.

    [SerializeField] public int damageAmount = 10;

    public GameObject hitbox;
    private Vector3 originalHitboxPosition; // Store the original position of the hitbox

    private int originalDamageAmount;



    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        originalHitboxPosition = hitbox.transform.position;

        playerMove = GetComponent<PlayerMove>();

        originalDamageAmount = damageAmount;

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
        anim.SetTrigger("attack");

        // Get the position of the hitbox.
        Vector3 hitboxPosition = hitbox.transform.position;

        // Detect enemies in front of the character using Physics.OverlapBox.
        Collider[] hitColliders = Physics.OverlapBox(hitboxPosition, hitbox.transform.localScale / 2, Quaternion.identity, enemyLayer);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the hit object is an enemy 
            if (hitCollider.CompareTag("Enemy") && !hitCollider.isTrigger)
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
        StartCoroutine(AttackCooldown());


    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;

        // Check if the character is defending before allowing the cooldown to end.
        while (isDefending)
        {
            yield return null; // Wait for the next frame.
        }

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }


    void Defend()
    {
        canAttack = false;
        // Set the character to defend.
        anim.SetBool("shieldActive", true);
        isDefending = true;

        StopCoroutine(AttackCooldown()); // Check if AttackCoroutine is not null before stopping it.

        playerMove.DefendingSpeed(); // Call the DefendingSpeed method from playerMove.
        defendSphere.SetActive(true); // Show the defense sphere.

        // Calculate the new hitbox position relative to the player
        Vector3 defendedHitboxOffset = new Vector3(0f, 2f, 0f); // Offset of 2 units up on the Y-axis
        Vector3 defendedHitboxPosition = transform.position + defendedHitboxOffset;
        hitbox.transform.position = defendedHitboxPosition;
    }

    void StopDefending()
    {
        // Reset character's state to normal.
        StartCoroutine(AttackCooldown());
        canAttack = true;
        anim.SetBool("shieldActive", false);
        isDefending = false;

        playerMove.ReturnToNormalSpeed(); // Call the ReturnToNormalSpeed method from playerMove.
        defendSphere.SetActive(false); // Hide the defense sphere.

        // Calculate the new hitbox position in front of the player based on the player's facing direction.
        Vector3 originalHitboxPositionInFrontOfPlayer = transform.position + transform.forward * originalHitboxPosition.z;
        hitbox.transform.position = originalHitboxPositionInFrontOfPlayer;
    }

    public void ApplyDamageIncrease(int increaseAmount)
    {
        // Increase the player's damage amount.
        damageAmount += increaseAmount;
        Debug.Log("Applied damage increase: " + damageAmount);

    }

    public void RevertDamageIncrease()
    {
        damageAmount = originalDamageAmount;
    }

}
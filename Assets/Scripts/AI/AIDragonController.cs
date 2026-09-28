using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class AIDragonController : MonoBehaviour
{
    private enum AIState
    {
        Idle,
        Chase,
        Attack
    }

    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 4f;
    [SerializeField] private float rotationSpeed = 8f;
    [SerializeField] private float attackDistance = 10f;

    [Header("Abilities")]
    [SerializeField] private FireAttack fireAttack;
    [SerializeField] private TailAttack tailAttack;
    [SerializeField] private FlyAttack flyAttack;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private CharacterController characterController;

    private Health health;
    private Health playerHealth;

    private AIState currentState = AIState.Idle;

    private bool isDead;
    private bool playerIsDead;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        health = GetComponent<Health>();

        if (player != null)
            playerHealth = player.GetComponent<Health>();
    }

    private void OnEnable()
    {
        if (health != null)
            health.OnDeath += HandleDeath;

        if (playerHealth != null)
            playerHealth.OnDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnDeath -= HandleDeath;

        if (playerHealth != null)
            playerHealth.OnDeath -= HandlePlayerDeath;
    }

    private void Update()
    {
        // AI is dead.
        if (isDead)
            return;

        // Player is dead, so stop fighting.
        if (playerIsDead)
            return;

        if (player == null)
            return;

        float distance =
            Vector3.Distance(transform.position, player.position);

        switch (currentState)
        {
            case AIState.Idle:
                HandleIdle(distance);
                break;

            case AIState.Chase:
                HandleChase(distance);
                break;

            case AIState.Attack:
                HandleAttack(distance);
                break;
        }
    }

    private void HandleIdle(float distance)
    {
        if (distance <= attackDistance)
        {
            currentState = AIState.Attack;
        }
        else
        {
            currentState = AIState.Chase;
        }
    }

    private void HandleChase(float distance)
    {
        if (distance <= attackDistance)
        {
            currentState = AIState.Attack;
            return;
        }

        MoveTowardsPlayer();
    }

    private void HandleAttack(float distance)
    {
        if (distance > attackDistance)
        {
            currentState = AIState.Chase;
            return;
        }

        if (animator != null)
            animator.SetFloat("Speed", 0f);

        FacePlayer();
        ChooseAttack(distance);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
        {
            if (animator != null)
                animator.SetFloat("Speed", 0f);

            return;
        }

        direction.Normalize();

        characterController.Move(
            direction * moveSpeed * Time.deltaTime
        );

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        if (animator != null)
            animator.SetFloat("Speed", 1f);
    }

    private void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void ChooseAttack(float distance)
    {
        if (distance > 7f)
        {
            fireAttack.TryUse();
        }
        else if (distance > 3f)
        {
            flyAttack.TryUse();
        }
        else
        {
            tailAttack.TryUse();
        }
    }

    private void HandleDeath()
    {
        isDead = true;

        currentState = AIState.Idle;

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Die");
        }
    }

    private void HandlePlayerDeath()
    {
        playerIsDead = true;

        currentState = AIState.Idle;

        if (animator != null)
            animator.SetFloat("Speed", 0f);
    }
}
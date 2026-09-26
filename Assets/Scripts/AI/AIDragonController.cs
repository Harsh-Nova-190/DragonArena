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

    private CharacterController characterController;
    private AIState currentState = AIState.Idle;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
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

        FacePlayer();
        ChooseAttack(distance);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.01f)
            return;

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
}
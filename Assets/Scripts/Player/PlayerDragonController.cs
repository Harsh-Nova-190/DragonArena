using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerDragonController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float rotationSpeed = 12f;

    [Header("Abilities")]
    [SerializeField] private FireAttack fireAttack;
    [SerializeField] private TailAttack tailAttack;
    [SerializeField] private FlyAttack flyAttack;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    private CharacterController characterController;
    private Health health;

    private bool isDead;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        if (health != null)
            health.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnDeath -= HandleDeath;
    }

    private void Update()
    {
        if (isDead)
            return;

        HandleMovement();
        HandleAbilities();
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection =
            new Vector3(horizontal, 0f, vertical);

        inputDirection = Vector3.ClampMagnitude(
            inputDirection,
            1f
        );

        if (animator != null)
        {
            animator.SetFloat(
                "Speed",
                inputDirection.magnitude
            );
        }

        if (inputDirection.sqrMagnitude > 0.01f)
        {
            characterController.Move(
                inputDirection * moveSpeed * Time.deltaTime
            );

            Quaternion targetRotation =
                Quaternion.LookRotation(inputDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }

    private void HandleAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            fireAttack.TryUse();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            tailAttack.TryUse();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            flyAttack.TryUse();
        }
    }

    private void HandleDeath()
    {
        isDead = true;

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetTrigger("Die");
        }
    }
}
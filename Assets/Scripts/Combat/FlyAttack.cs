using System.Collections;
using UnityEngine;

public class FlyAttack : Ability
{
    [Header("Fly Attack")]
    [SerializeField] private float attackRadius = 4f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float flightHeight = 5f;
    [SerializeField] private float flightDuration = 0.8f;
    [SerializeField] private Animator animator;

    private Vector3 groundPosition;

    protected override void Awake()
    {
        base.Awake();

        abilityName = "Fly Attack";
        damage = 50f;
        cooldown = 7f;
        range = 5f;

        groundPosition = transform.position;
    }

    protected override void Use()
    {
        StartCoroutine(FlyAttackRoutine());
    }

    private IEnumerator FlyAttackRoutine()
    {
        if(animator != null)
            animator.SetTrigger("Fly");

        groundPosition = transform.position;

        Vector3 airPosition =
            groundPosition + Vector3.up * flightHeight;

        float timer = 0f;

        // Take off
        while (timer < flightDuration)
        {
            timer += Time.deltaTime;

            float t = timer / flightDuration;

            transform.position =
                Vector3.Lerp(groundPosition, airPosition, t);

            yield return null;
        }

        // Attack
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            attackRadius,
            targetLayer
        );

        foreach (Collider hit in hits)
        {
            Health target = hit.GetComponentInParent<Health>();

            if (target != null && target.gameObject != gameObject)
            {
                target.TakeDamage(damage);
            }
        }

        // Land
        timer = 0f;

        while (timer < flightDuration)
        {
            timer += Time.deltaTime;

            float t = timer / flightDuration;

            transform.position =
                Vector3.Lerp(airPosition, groundPosition, t);

            yield return null;
        }

        transform.position = groundPosition;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position,
            attackRadius
        );
    }
}
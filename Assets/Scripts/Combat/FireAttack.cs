using UnityEngine;

public class FireAttack : Ability
{
    [Header("Fire Attack")]
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask targetLayer;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("VFX")]
    [SerializeField] private ParticleSystem fireBreathVFX;
    [SerializeField] private Transform fireBreathPoint;

    protected override void Awake()
    {
        base.Awake();

        abilityName = "Fire Attack";
        damage = 25f;
        cooldown = 3f;
        range = 12f;
    }

    protected override void Use()
    {
        if (animator != null)
            animator.SetTrigger("FireAttack");

        // Spawn fire effect independently from the dragon.
        if (fireBreathVFX != null && fireBreathPoint != null)
        {
            ParticleSystem effect = Instantiate(
                fireBreathVFX,
                fireBreathPoint.position,
                fireBreathPoint.rotation
            );

            effect.Play();

            Destroy(effect.gameObject, 2f);
        }

        Vector3 origin = transform.position + Vector3.up * 1f;

        RaycastHit[] hits = Physics.SphereCastAll(
            origin,
            attackRadius,
            transform.forward,
            range,
            targetLayer
        );

        foreach (RaycastHit hit in hits)
        {
            Health target = hit.collider.GetComponentInParent<Health>();

            if (target != null && target.gameObject != gameObject)
            {
                target.TakeDamage(damage);

                Debug.Log(
                    $"{abilityName} hit {target.gameObject.name} for {damage} damage."
                );

                break;
            }
        }

        Debug.Log($"{abilityName} used!");
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * 1f;

        Gizmos.DrawWireSphere(
            origin + transform.forward * range,
            attackRadius
        );

        Gizmos.DrawLine(
            origin,
            origin + transform.forward * range
        );
    }
}
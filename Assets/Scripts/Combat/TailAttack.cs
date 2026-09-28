using UnityEngine;

public class TailAttack : Ability
{
    [Header("Tail Attack")]
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private LayerMask targetLayer;

    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("VFX")]
    [SerializeField] private ParticleSystem impactVFX;

    protected override void Awake()
    {
        base.Awake();

        abilityName = "Tail Attack";
        damage = 35f;
        cooldown = 2f;
        range = 2.5f;
    }

    protected override void Use()
    {
        Vector3 attackCenter =
            transform.position + transform.forward * range;

        if (animator != null)
            animator.SetTrigger("TailAttack");

        Collider[] hits = Physics.OverlapSphere(
            attackCenter,
            attackRadius,
            targetLayer
        );

        foreach (Collider hit in hits)
        {
            Health target = hit.GetComponentInParent<Health>();

            if (target != null && target.gameObject != gameObject)
            {
                target.TakeDamage(damage);

                SpawnImpactVFX(target.transform.position);

                break;
            }
        }

        Debug.Log($"{abilityName} used!");
    }

    private void SpawnImpactVFX(Vector3 position)
    {
        if (impactVFX == null)
        {
            Debug.LogWarning(
                $"{name}: Tail Impact VFX is NOT assigned!"
            );

            return;
        }

        ParticleSystem effect = Instantiate(
            impactVFX,
            position + Vector3.up,
            Quaternion.identity
        );

        effect.Play();

        Destroy(effect.gameObject, 2f);

        Debug.Log("Tail Impact VFX spawned!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position + transform.forward * range,
            attackRadius
        );
    }
}
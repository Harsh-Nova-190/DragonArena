using UnityEngine;

public class TailAttack : Ability
{
    [Header("Tail Attack")]
    [SerializeField] private float attackRadius = 2f;
    [SerializeField] private LayerMask targetLayer;

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
            }
        }

        Debug.Log($"{abilityName} used!");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(
            transform.position + transform.forward * range,
            attackRadius
        );
    }
}
using UnityEngine;

public class FireAttack : Ability
{
    [Header("Fire Attack")]
    [SerializeField] private float attackRadius = 1.5f;
    [SerializeField] private LayerMask targetLayer;

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

                // Only damage the first valid target.
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
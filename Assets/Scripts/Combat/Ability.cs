using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [Header("Ability")]
    [SerializeField] protected string abilityName;
    [SerializeField] protected float damage = 25f;
    [SerializeField] protected float cooldown = 2f;
    [SerializeField] protected float range = 3f;

    protected float cooldownTimer;

    public string AbilityName => abilityName;
    public float Damage => damage;
    public float Cooldown => cooldown;
    public float Range => range;
    public float RemainingCooldown => Mathf.Max(0f, cooldownTimer);

    public bool IsReady => cooldownTimer <= 0f;


    protected virtual void Awake()
    {
        cooldownTimer = 0f;
    }

    protected virtual void Update()
    {
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    public void TryUse()
    {
        if (!IsReady)
            return;

        Use();

        cooldownTimer = cooldown;
    }

    protected abstract void Use();
}
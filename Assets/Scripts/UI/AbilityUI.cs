using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUI : MonoBehaviour
{
    public enum AbilityType
    {
        Fire,
        Tail,
        Fly
    }

    [Header("Ability")]
    [SerializeField] private AbilityType abilityType;

    [SerializeField] private PlayerDragonController player;

    [Header("Cooldown UI")]
    [SerializeField] private Image cooldownOverlay;
    [SerializeField] private TMP_Text cooldownText;

    private Ability ability;

    private void Awake()
    {
        if (player == null)
        {
            Debug.LogError($"{name}: Player Dragon is not assigned.");
            return;
        }

        ability = abilityType switch
        {
            AbilityType.Fire => player.GetComponent<FireAttack>(),
            AbilityType.Tail => player.GetComponent<TailAttack>(),
            AbilityType.Fly => player.GetComponent<FlyAttack>(),
            _ => null
        };

        if (ability == null)
        {
            Debug.LogError(
                $"{name}: Could not find {abilityType} ability on PlayerDragon."
            );
        }
    }

    private void Update()
    {
        if (ability == null)
            return;

        float remaining = ability.RemainingCooldown;

        if (remaining <= 0f)
        {
            if (cooldownOverlay != null)
                cooldownOverlay.fillAmount = 0f;

            if (cooldownText != null)
                cooldownText.text = "";
        }
        else
        {
            if (cooldownOverlay != null)
            {
                cooldownOverlay.fillAmount =
                    remaining / ability.Cooldown;
            }

            if (cooldownText != null)
            {
                cooldownText.text =
                    remaining.ToString("0.0");
            }
        }
    }
}
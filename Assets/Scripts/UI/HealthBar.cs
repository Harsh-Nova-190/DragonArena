using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Health targetHealth;

    private void Start()
    {
        if (targetHealth == null)
            return;

        targetHealth.OnHealthChanged += UpdateHealth;

        UpdateHealth(
            targetHealth.CurrentHealth,
            targetHealth.MaxHealth
        );
    }

    private void OnDestroy()
    {
        if (targetHealth != null)
        {
            targetHealth.OnHealthChanged -= UpdateHealth;
        }
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        if (slider == null)
            return;

        slider.value = currentHealth / maxHealth;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider Slider;
    [SerializeField] private Gradient grad;
    [SerializeField] private Image fill;
    public void SetMaxHealth(int health)
    {
        Slider.maxValue = health;
        Slider.value = health;
        fill.color = grad.Evaluate(1f);
    }

    public void SetHealth(int health)
    {
        Slider.value = health;
        fill.color = grad.Evaluate(Slider.normalizedValue);
    }
}

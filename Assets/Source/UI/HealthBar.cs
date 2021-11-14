using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private Actor _actor;

        private void OnEnable()
        {
            _actor.OnHealthChanged += GetDamageUI;
        }

        private void OnDisable()
        {
            _actor.OnHealthChanged -= GetDamageUI;
        }

        public void Initialize(float maxHealth)
        {
            _healthBarSlider.maxValue = maxHealth;
            _healthBarSlider.value = maxHealth;
        }

        private void GetDamageUI(float health)
        {
            _healthBarSlider.value = health;
        }
    }
}
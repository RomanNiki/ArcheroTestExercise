using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSlider;
        [SerializeField] private Actor _actor;

        private void OnEnable()
        {
            _actor.HealthChangedEvent += GetDamage;
        }

        private void OnDisable()
        {
            _actor.HealthChangedEvent -= GetDamage;
        }

        public void Initialize(float maxHealth)
        {
            _healthBarSlider.maxValue = maxHealth;
            _healthBarSlider.value = maxHealth;
        }

        private void GetDamage(float health)
        {
            _healthBarSlider.value = health;
        }
    }
}
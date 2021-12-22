using System;
using Source.Scripts.Interfaces;
using Source.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts
{
    public abstract class Actor : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float _health = 1f;
        [SerializeField] protected HealthBar _healthBar;
        private float _maxHealth;
        private bool Alive => _health > 0;
        public UnityEvent _deathEvent;
        public event Action<float> HealthChangedEvent;

        protected virtual void Start()
        {
            _maxHealth = _health;
            _healthBar.Initialize(_health);
        }

        private void OnDestroy()
        {
            _deathEvent.Invoke();
        }

        public virtual void ApplyDamage(float damage)
        {
            if (CanApplyDamage(damage))
            {
                _health -= damage;
                HealthChangedEvent?.Invoke(Mathf.Clamp(_health, 0, _maxHealth));
            }

            if (!Alive) 
            {
                Destroy(gameObject);
            }
        }
        protected bool CanApplyDamage(float damage)
        {
            return Alive && damage > 0;
        }
    }
}
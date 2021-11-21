﻿using System;
using Source.Interfaces;
using Source.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Source
{
    public abstract class Actor : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float _health = 1f;
        [SerializeField] protected HealthBar _healthBar;
        protected float _maxHealth;
        protected bool Alive => _health > 0;
        public UnityEvent OnDeath;
        public event Action<float> HealthChangedEvent;

        protected virtual void Start()
        {
            _maxHealth = _health;
            _healthBar.Initialize(_health);
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
                OnDeath.Invoke();
                Destroy(gameObject);
            }
        }
        protected bool CanApplyDamage(float damage)
        {
            return Alive && damage > 0;
        }
    }
}
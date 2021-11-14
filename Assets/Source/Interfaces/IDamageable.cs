using System;

namespace Source.Interfaces
{
    public interface IDamageable
    {
        void ApplyDamage(float damage);
        event Action OnDeath;
    }
}
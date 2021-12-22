using UnityEngine;

namespace Source.Scripts.Weapon
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected Transform _startPositionTransform;
        protected float _attackSpeed;
        protected float _nextShootTime;

        public void Setup(float attackSpeed)
        {
            _attackSpeed = attackSpeed;
        }

        public abstract void Attack(Vector3 enemyPos, object source);
    }
}
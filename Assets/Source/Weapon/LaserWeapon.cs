using Source.Interfaces;
using UnityEngine;

namespace Source.Weapon
{
    public class LaserWeapon : Weapon
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _laserDamage;

        public override void Attack(Vector3 enemyPos, object source)
        {
            var ray = new Ray(_startPositionTransform.position, enemyPos - _startPositionTransform.position);
            if (Physics.Raycast(ray, out var hitInfo, _maxDistance, _layerMask))
            {
                var enemy = hitInfo.transform.GetComponent<IDamageable>();
                enemy?.ApplyDamage(_laserDamage);
            }
        }
    }
}
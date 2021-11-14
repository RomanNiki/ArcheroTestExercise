using UnityEngine;

namespace Source.Weapon
{
    public class ProjectileWeapon : Weapon
    {
        [SerializeField] protected GameObject _bulletPrefab;

        public override void Attack(Vector3 enemyPos, object source)
        {
            if (Time.fixedTime <= _nextShootTime) return;

            var lastBullet = Instantiate(_bulletPrefab, _startPositionTransform.position,
                Quaternion.LookRotation(enemyPos));
            lastBullet.transform.LookAt(enemyPos);
            lastBullet.GetComponent<Bullet>().Setup(enemyPos - _startPositionTransform.position, source);
            _nextShootTime = Time.fixedTime + 1f / _attackSpeed;
        }
    }
}
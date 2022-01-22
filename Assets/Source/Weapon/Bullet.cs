using Source.Interfaces;
using UnityEngine;

namespace Source.Weapon
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _bulletSpeed;

        private Vector3 _direction;
        private object _source;

        private void Update()
        {
            transform.position += _direction * _bulletSpeed * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision other)
        {
            switch (_source)
            {
                case IPlayer _ when other.transform.GetComponent<IPlayer>() != null:
                case IEnemy _ when other.transform.GetComponent<IEnemy>() != null:
                    return;
            }
            
            if (other.transform.TryGetComponent<IDamageable>(out var component)) component.ApplyDamage(_damage);

            if (other.transform.GetComponent<Bullet>() != null) return;
            Destroy(gameObject);
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Destroy(gameObject);
        }

        public void Setup(Vector3 direction, object source)
        {
            Destroy(gameObject, 5f);
            _direction = direction.normalized;
            _source = source;
        }
    }
}
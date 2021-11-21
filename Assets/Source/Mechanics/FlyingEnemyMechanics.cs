using UnityEngine;

namespace Source.Mechanics
{
    public class FlyingEnemyMechanics : EnemyBaseMechanics
    {
        public override void TryOpenFire()
        {
            if (CanAttack) _rigidbody.velocity = new Vector3();

            base.TryOpenFire();
        }

        public override void Move(Vector3 direction)
        {
            if (CanAttack == false && Player.Instance != null)
            {
                RotateToEnemy();
                _rigidbody.velocity = direction;
            }
            else
            {
                _rigidbody.velocity = new Vector3();
            }
        }
    }
}
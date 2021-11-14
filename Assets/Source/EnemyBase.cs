using Source.Interfaces;

namespace Source
{
    public class EnemyBase : Actor, IEnemy
    {
        private void OnEnable()
        {
            OnDeath += CoinsCounter.Instance.AddCoins;
        }

        private void OnDisable()
        {
            OnDeath -= CoinsCounter.Instance.AddCoins;
        }
    }
}
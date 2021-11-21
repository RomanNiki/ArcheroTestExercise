using Source.Interfaces;

namespace Source
{
    public sealed class Player : Actor, IPlayer
    {
        public static Player Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance == this)
            {
                Destroy(gameObject);
            }
        }
    }
}
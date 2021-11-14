using Source.Interfaces;

namespace Source
{
    public class Player : Actor, IPlayer
    {
        public static Player Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}
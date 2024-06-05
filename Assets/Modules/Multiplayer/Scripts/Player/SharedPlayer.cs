using Fusion;

namespace Xennial.Multiplayer
{
    public class SharedPlayer : NetworkBehaviour
    {
        private bool _isSpawned = false;

        [Networked, Capacity(32)]
        public string PlayerName
        {
            get;
            set;
        }

        [Networked, Capacity(256)]
        public string AvatarUrl 
        { 
            get;
            set; 
        }

        [Networked]
        public NetworkBool IsAvatarVisible
        {
            get;
            set;
        } = true;

        public bool IsSpawned
        {
            get => _isSpawned;
        }

        public override void Spawned()
        {
            base.Spawned();
            DontDestroyOnLoad(this);
            _isSpawned = true;
            name = $"{PlayerName} Shared Player";
        }
    }
}
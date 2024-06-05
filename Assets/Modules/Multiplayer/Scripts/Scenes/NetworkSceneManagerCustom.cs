using Fusion;

namespace Xennial.Multiplayer
{
    public class NetworkSceneManagerCustom : NetworkSceneManagerDefault
    {
        public NetworkSceneManagerDefault SetAddressableScenePath(string path)
        {
            AddressableScenesLabel = path;
            return this;
        }
    }
}
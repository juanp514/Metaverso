namespace Xennial.XR.Player
{
    public class SpawnSettings
    {
        private int _spawnPointIndex;

        public int SpawnPointIndex
        {
            get => _spawnPointIndex;
        }

        public void SetSpawnPoint(int index)
        {
            _spawnPointIndex = index;
        }

        public void Reset()
        {
            _spawnPointIndex = -1;
        }
    }
}
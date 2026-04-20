using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    [SerializeField] private Transform _playerParent;
    [SerializeField] private Camera _camera;
    [SerializeField] private ObjectPool _objectPool;

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject playerObject = Runner.Spawn(
                PlayerPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            );

            // Set parent AFTER spawn
            if (_playerParent != null)
            {
                playerObject.transform.SetParent(_playerParent);
                playerObject.GetComponent<MainHeroPlayerCharacter>().SetCamera(_camera);
                playerObject.GetComponent<MainHeroPlayerCharacter>().SetBulletPool(_objectPool);

                
            }
        }
    }
}
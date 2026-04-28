using Fusion;
using UnityEngine;
using System;
using Cinemachine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;
    public Action<GameObject> PlayerSpawwnedAction;

    [SerializeField] private Transform _playerParent;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _camerasParent;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            NetworkObject playerObject = Runner.Spawn(
                PlayerPrefab,
                new Vector3(0, 0, 0),
                Quaternion.identity
            );

            HeroBuddy heroBuddy = playerObject.GetComponent<HeroBuddy>();
            heroBuddy.SetupParent(_playerParent);
            heroBuddy.SetupMainCamera(_camera);

            GameObject pivot = heroBuddy.GetCameraPivot();
            _cinemachineVirtualCamera.Follow = pivot.transform;
            _cinemachineVirtualCamera.LookAt = pivot.transform;

            _camerasParent.SetActive(true);


            /* MainHero mainHero = heroBuddy.GetHeroParent().GetComponent<MainHero>();
            mainHero.ActiveCamera();  */

            /* MainHero mainHero = playerObject.GetComponent<MainHero>();
            mainHero.ActiveCamera(); */

            // Set parent AFTER spawn
            if (_playerParent != null)
            {
                //playerObject.transform.SetParent(_playerParent);
                /* playerObject.GetComponent<MainHeroPlayerCharacter>().SetCamera(_camera);
                playerObject.GetComponent<MainHeroPlayerCharacter>().SetBulletPool(_objectPool);

                PlayerSpawwnedAction?.Invoke(playerObject.gameObject); */
            }
        }
    }
}
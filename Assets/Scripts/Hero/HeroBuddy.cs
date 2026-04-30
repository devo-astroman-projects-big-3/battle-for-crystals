using UnityEngine;
using System;

public class HeroBuddy : MonoBehaviour
{
    [SerializeField] private GameObject _cameraPivot;
    [SerializeField] private ThirdPersonAim _thirdPersonAim;
    [SerializeField] private GameObject _heroParent;
    [SerializeField] private GameObject _heroFsm;
    [SerializeField] private GameObject _cameras;

    public Action<int> ReceiveHitAction;    

    public void SetupParent(Transform parent)
    {
        _heroParent.transform.SetParent(parent);
        _heroFsm.transform.SetParent(parent);
        transform.SetParent(_heroParent.transform);

        _cameras.SetActive(true);
        _heroFsm.SetActive(true);
    }

    public void SetupMainCamera(Camera mainCamera)
    {
        _thirdPersonAim.SetCamera(mainCamera);
    }

    public void SetupBornPosition(Vector3 position)
    {
        _heroParent.GetComponent<MainHero>().SetLastCheckpoint(position);
    }

    public GameObject GetCameraPivot()
    {   
        return _cameraPivot;
    }

    public void ReceiveDamage(int damage)
    {   
        ReceiveHitAction?.Invoke(damage);
    }
    
}
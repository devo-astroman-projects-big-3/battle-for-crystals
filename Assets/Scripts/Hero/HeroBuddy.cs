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
        _heroParent.transform.parent = parent;
        _heroFsm.transform.parent = parent;
        transform.parent = _heroParent.transform;
        _cameras.SetActive(true);
    }

    public void SetupMainCamera(Camera mainCamera)
    {
        _thirdPersonAim.SetCamera(mainCamera);
    }

    public GameObject GetCameraPivot()
    {   
        return _cameraPivot;
    }

    public void ReceiveDamage(int damage)
    {   
        ReceiveHitAction?.Invoke(damage);
    }

    public void PlaceAtPoint(Vector3 point)
    {   
        gameObject.transform.position = point;
    }

    
}
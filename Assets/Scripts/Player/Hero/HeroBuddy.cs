using UnityEngine;
using System;

public class HeroBuddy : MonoBehaviour
{
    [SerializeField] private GameObject _cameraPivot;
    [SerializeField] private ThirdPersonAim _thirdPersonAim;
    [SerializeField] private GameObject _heroParent;
    [SerializeField] private GameObject _heroFsm;
    [SerializeField] private GameObject _cameras;
    [SerializeField] private GameObject _canvasGO;
    [SerializeField] private GameObject _controllersGO;
    [SerializeField] private GameObject _mainHeroGO;
    [SerializeField] private GameObject _heroFsmGO;

    private int _id = -100;
    private bool _isActive = false;

    public Action<int> ReceiveHitAction;

    public void SetId(int id)
    {
        _id = id;
        _heroParent.GetComponent<MainHero>().SetId(_id);        
    }

    public int GetId()
    {
        return _id;
    }

    public void SetupParent(Transform parent)
    {
        transform.SetParent(_heroParent.transform);
    }
    public void ActiveCamera()
    {
        _cameras.SetActive(true);
    }

    public void ActiveCanvas()
    {
        _canvasGO.SetActive(true);
    }

    public void ActiveHeroFsm()
    {
        _heroFsmGO.SetActive(true);
    }

    public void ActiveMainHero()
    {
        _mainHeroGO.SetActive(true);
    }

    public void ActiveControllers()
    {
        _controllersGO.SetActive(true);
    }

    public void ActiveHeroTagAndLayer()
    {
        gameObject.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player");
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
        if(_isActive)
            ReceiveHitAction?.Invoke(damage);
    }
    
}
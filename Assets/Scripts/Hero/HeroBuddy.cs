using UnityEngine;

public class HeroBuddy : MonoBehaviour
{
    [SerializeField] private GameObject _cameraPivot;
    [SerializeField] private ThirdPersonAim _thirdPersonAim;
    [SerializeField] private GameObject _heroParent;
    [SerializeField] private GameObject _heroFsm;
    

    public void SetupParent(Transform parent)
    {
        _heroParent.transform.parent = parent;
        _heroFsm.transform.parent = parent;
        transform.parent = _heroParent.transform;
        _heroParent.SetActive(true);
        _heroFsm.SetActive(true);
    }

    public void SetupMainCamera(Camera mainCamera)
    {
        _thirdPersonAim.SetCamera(mainCamera);
    }

    public GameObject GetCameraPivot()
    {   
        return _cameraPivot;
    }

    
}
using UnityEngine;

public class HeroBuddy : MonoBehaviour
{
    //[SerializeField] private Transform _heroParent;
    [SerializeField] private GameObject _cameraPivot;
    [SerializeField] private ThirdPersonAim _thirdPersonAim;
    

    public void SetupParent(Transform parent)
    {
        //_heroParent.transform.parent = parent;
        transform.parent = parent;
        //_heroParent.gameObject.SetActive(true);
    }

    public void SetupMainCamera(Camera mainCamera)
    {
        _thirdPersonAim.SetCamera(mainCamera);
    }

    /* public GameObject GetHeroParent()
    {
        
        return _heroParent.gameObject;
    } */

    public GameObject GetCameraPivot()
    {   
        return _cameraPivot;
    }

    
}
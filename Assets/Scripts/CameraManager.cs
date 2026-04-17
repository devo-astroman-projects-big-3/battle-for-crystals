using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _virtualCamera;
    
    public void SetVirtualFollow(Transform target)
    {
        _virtualCamera.Follow = target;
    }

    public CinemachineVirtualCamera GetVirtualCamera()
    {
        return _virtualCamera;
    }
}
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private ThirdPersonCameraPivot _thirdPersonCameraPivot;

    public void SetVirtualTargets(Transform followTarget, Transform lookAtTarget = null)
    {
        if (_virtualCamera == null)
        {
            Debug.LogWarning("Virtual camera is not assigned.");
            return;
        }
        
        _virtualCamera.Follow = followTarget;
        _virtualCamera.LookAt = lookAtTarget != null ? lookAtTarget : followTarget;
    }

    public CinemachineVirtualCamera GetVirtualCamera()
    {
        return _virtualCamera;
    }

    public ThirdPersonCameraPivot GetThirdPersonCameraPivot()
    {
        return _thirdPersonCameraPivot;
    }
}
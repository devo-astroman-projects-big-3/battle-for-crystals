using UnityEngine;

public class ThirdPersonAim : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private float _maxAimDistance = 1000f;
    [SerializeField] private LayerMask _aimLayerMask = Physics.DefaultRaycastLayers;

    public Vector3 GetAimPoint()
    {
        if (_mainCamera == null)
            return _muzzle.position + _muzzle.forward * _maxAimDistance;

        Ray ray = _mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, _maxAimDistance, _aimLayerMask))
        {
            return hit.point;
        }

        return ray.GetPoint(_maxAimDistance);
    }

    public Vector3 GetFireDirection()
    {
        Vector3 aimPoint = GetAimPoint();
        return (aimPoint - _muzzle.position).normalized;
    }

    public void SetCamera(Camera camera)
    {
        _mainCamera = camera;
    }
}
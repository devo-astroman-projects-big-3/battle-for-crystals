using Fusion;
using UnityEngine;

public class LookAtNetwork : NetworkBehaviour
{
    [SerializeField] private Transform _rootHeadTransform;

    [Networked]
    private Vector3 NetworkLookPosition { get; set; }

    public void SetLookPosition(Vector3 newPosition)
    {
        if (!Object.HasStateAuthority)
            return;

        NetworkLookPosition = newPosition;
    }

    public override void Render()
    {
        if (_rootHeadTransform == null)
            return;

        Vector3 direction = NetworkLookPosition - _rootHeadTransform.position;

        if (direction.sqrMagnitude > 0.001f)
        {
            _rootHeadTransform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
using Fusion;
using UnityEngine;

public class LeverAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetIsLeft(bool isLeft)
    {
        _animator.SetBool("IsLeft", isLeft);
    }
}
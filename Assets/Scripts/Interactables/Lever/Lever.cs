using UnityEngine;

public class Lever : MonoBehaviour
{
    #region Fields
    [SerializeField] LeverAnimator _leverAnimator;
    [SerializeField] LeverStateNetwork _leverStateNetwork;
    [SerializeField] Door _door;
    
    #endregion

    #region Private Fields
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _leverStateNetwork.LeverInteractedAction += HandleLeverInteracted;
    }

    void Update()
    {
        //if (Input.GetButtonDown("E"))
        if (Input.GetMouseButtonDown(0))
        {
            /* _EPressed = true;
            _leverAtLeft = !_leverAtLeft;

            _leverAnimator.RequestInteract(); */

            _leverStateNetwork.RequestInteract();
        }
    }

    void OnDestroy()
    {
        _leverStateNetwork.LeverInteractedAction -= HandleLeverInteracted;
    }
    #endregion

    #region Public Methods
    public void Interact()
    {
        
    }
    #endregion

    #region Private Methods
    private void HandleLeverInteracted(bool isLeft)
    {
        _leverAnimator.SetIsLeft(isLeft);
        if (isLeft)
        {
            _door.CloseDoor();
        }
        else
        {
            _door.OpenDoor();
        }
    }
    #endregion
}
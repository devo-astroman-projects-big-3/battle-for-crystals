using UnityEngine;

public class Lever : MonoBehaviour
{
    #region Fields
    [SerializeField] LeverAnimator _leverAnimator;
    #endregion

    #region Private Fields
    private bool _EPressed = false;
    private bool _leverAtLeft = false;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        
    }

    void Update()
    {
        //if (Input.GetButtonDown("E"))
        if (Input.GetMouseButtonDown(0))
        {
            _EPressed = true;
            _leverAtLeft = !_leverAtLeft;

            _leverAnimator.RequestInteract();
        }
    }

    void OnDestroy()
    {
    }
    #endregion

    #region Public Methods
    public void Interact()
    {
        
    }
    #endregion

    #region Private Methods
    #endregion
}
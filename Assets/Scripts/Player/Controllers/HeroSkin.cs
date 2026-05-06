using UnityEngine;

public class HeroSkin : MonoBehaviour
{
    #region Fields
    [SerializeField] private MeshRenderer[]  _skinMeshes;
    #endregion

    #region Private Fields    
    #endregion

    #region Unity Callbacks
    #endregion

    #region Public Methods
    public void HideSkin()
    {
        for(int i = 0; i<_skinMeshes.Length; i++)
        {
            _skinMeshes[i].enabled = false;
        }
    }

    public void ShowSkin()
    {
        for(int i = 0; i<_skinMeshes.Length; i++)
        {
            _skinMeshes[i].enabled = true;
        }
    }
    #endregion

    #region Private Methods
    #endregion
}
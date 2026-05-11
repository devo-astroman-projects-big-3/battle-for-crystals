using UnityEngine;
using System.Collections;
using TMPro;

public class NetUI : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private TMP_Text _netMsgsText;

    private Coroutine _hideNetMsgRoutine;
    #endregion

    #region Public Methods
    public void SetNetMsg(string msg)
    {
        _netMsgsText.text = msg;
        _hideNetMsgRoutine = StartCoroutine(HideNetMsgRoutine(2));
    }

    private IEnumerator HideNetMsgRoutine(int secs)
    {
        yield return new WaitForSeconds(secs);
        _netMsgsText.text = "";
        _hideNetMsgRoutine = null;
    }

    #endregion
}

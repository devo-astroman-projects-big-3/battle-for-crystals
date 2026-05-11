using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainResults : MonoBehaviour
{
    #region Fields
    #endregion

    #region Private Fields
    [SerializeField] private GameSceneManager _gameSceneManager;
    [SerializeField] private TMP_Text _winStatusText;
    [SerializeField] private TMP_Text _descriptionText;
    //[SerializeField] private TMP_Text _waitForOtherPlayerText;
    [SerializeField] private TMP_Text _waitForAdminToPlayAgainText;
    [SerializeField] private Button _playAgainButton;
    [SerializeField] private Button _exitButton;
    #endregion

    #region Public Fields
    #endregion


    #region Unity Callbacks
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Debug.Log("_playAgainButton.gameObject " + AllGameData.isAdmin);
        _playAgainButton.gameObject.SetActive(AllGameData.isAdmin);
        _waitForAdminToPlayAgainText.gameObject.SetActive(!AllGameData.isAdmin);

        SetResultMessages();
    }

    private void SetResultMessages()
    {
        int playerCrystals = Mathf.RoundToInt(AllGameData.nCrystalTaken);
        int totalCrystals = Mathf.RoundToInt(AllGameData.totalCrystals);
        int otherPlayerCrystals = Mathf.Max(totalCrystals - playerCrystals, 0);
        int crystalsDifference = Mathf.Abs(playerCrystals - otherPlayerCrystals);
        
        //_waitForOtherPlayerText.gameObject.SetActive(false);

        if (playerCrystals > otherPlayerCrystals)
        {
            SetWinMessages(crystalsDifference);
        }
        else if (playerCrystals < otherPlayerCrystals)
        {
            SetLoseMessages(crystalsDifference);
        }
        else
        {
            SetDrawMessages(playerCrystals);
        }
    }

    private void SetWinMessages(int nCrystals)
    {
            _winStatusText.text = "You are the Winner!";
            _descriptionText.text = $"You collected {nCrystals} more crystals than the other player";
    }

    private void SetLoseMessages(int nCrystals)
    {
            _winStatusText.text = "You Lose!";
            _descriptionText.text = $"You collected {nCrystals} fewer crystals than the other player";
    }

    private void SetDrawMessages(int nCrystals)
    {
            _winStatusText.text = "Draw!";
            _descriptionText.text = $"Both players collected {nCrystals} crystals";
    }

    private void OnDestroy()
    {
    }
    #endregion

    #region Private Methods
    #endregion
    #region Public Methods
    public void OnPlayAgainButtonClick()
    {
        _gameSceneManager.GoGameplay();
    }

    public void OnExitButtonClick()
    {
        _gameSceneManager.GoLobby();
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateRoomPanel : MonoBehaviour, IPanel
{
    [SerializeField] 
    private LobbyManager lobbyManager = null;

    [SerializeField] 
    private CanvasGroup canvasGroup = null;

    [SerializeField] 
    private TMP_InputField roomNameInputField = null;
    [SerializeField] 
    private TMP_InputField maxPlayerInputField = null;

    public void DisplayPanel(bool value)
    {
        canvasGroup.alpha = value ? 1 : 0;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;
    }

    public void OnBackBtnClicked()
    {
        lobbyManager.SetPairState(PairState.Lobby);
    }

    public async void OnCreateBtnClicked()
    {
        string roomName = roomNameInputField.text;
        // int maxPlayer = int.Parse(maxPlayerInputField.text);
        int maxPlayer = 10;

        await lobbyManager.CreateRoom(roomName, maxPlayer);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InRoomPanel : MonoBehaviour, IPanel
{
    [SerializeField] 
    private CanvasGroup canvasGroup = null;

    [SerializeField] 
    private TMP_Text roomNameTxt = null;

    [SerializeField] 
    private PlayerCell playerCellPrefab = null;
    [SerializeField] 
    private Transform contentTrans = null;

    private List<PlayerCell> playerCells = new List<PlayerCell>();

    private void Start()
    {
        GameManager.Instance.OnPlayerListUpdated += UpdatePlayerList;
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPlayerListUpdated -= UpdatePlayerList;
    }

    public void UpdatePlayerList()
    {
        foreach (var cell in playerCells)
        {
            Destroy(cell.gameObject);
        }

        playerCells.Clear();

        foreach (var player in GameManager.Instance.PlayerList)
        {
            var cell = Instantiate(playerCellPrefab, contentTrans);

            var playerData = player.Value;

            cell.SetInfo(playerData.PlayerName, playerData.IsReady);

            playerCells.Add(cell);
        }
    }

    public void DisplayPanel(bool value)
    {
        canvasGroup.alpha = value ? 1 : 0;
        canvasGroup.interactable = value;
        canvasGroup.blocksRaycasts = value;

        var runner = GameManager.Instance.Runner;

        roomNameTxt.text = runner.SessionInfo.Name;
    }

    public void OnReadyBtnClicked()
    {
        var runner = GameManager.Instance.Runner;

        if (GameManager.Instance.PlayerList.TryGetValue(runner.LocalPlayer, out NetworkPlayerData playerNetworkData))
        {
            playerNetworkData.SetReady_RPC(true);
        }
    }
}

using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Model = null;
    [SerializeField]
    private int modelCount = 0;


    private void Start()
    {
        // tankSpriteRenderer.color = GameManager.Instance.TankColor;
        // barrelSpriteRenderer.color = GameManager.Instance.BarrelColor;
    }

    public void OnSelcectCharacterChanged(GameObject obj)
    {
        // GameManager.Instance.TankColor.r = value / 255f;
        // tankSpriteRenderer.color = GameManager.Instance.TankColor;

        // GameManager.Instance.SetPlayerNetworkData();
        for (int i = 0; i < Model.Length; i++)
        {
            Model[i].SetActive(false);
            if (Model[i].name == obj.name)
            {
                Model[i].SetActive(true);
                GameManager.Instance.PlayerCharacter = modelCount;
                GameManager.Instance.SetPlayerNetworkData();
            }
        }
    }
    public void OnSelectLeft()
    {
        if (modelCount == 0)
        {
            modelCount = Model.Length;
        }
        else
            modelCount -= 1;
        Debug.Log($"{modelCount}");
        GameManager.Instance.PlayerCharacter =modelCount;
        GameManager.Instance.SetPlayerNetworkData();
    }
    public void OnSelectRight()
    {
        if (modelCount == Model.Length-1)
        {
            modelCount = 0;
        }
        else
            modelCount += 1;
        Debug.Log($"{modelCount}");

        GameManager.Instance.PlayerCharacter = modelCount;
        GameManager.Instance.SetPlayerNetworkData();
    }


    public void OnPlayerNameInputFieldChange(string value)
    {
        GameManager.Instance.PlayerName = value;
        GameManager.Instance.SetPlayerNetworkData();
    }
}

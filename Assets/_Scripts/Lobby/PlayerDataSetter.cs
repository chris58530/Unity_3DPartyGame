using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataSetter : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Model = null;
    [Networked]
    private int modelCount{get;set;}


    private void Start()
    {
        modelCount = 0;
        OnPlayerCharacterChange();
    }
    public void OnSelectLeft()//按鈕事件
    {
        if (modelCount - 1 >= 0)
        {
            modelCount --;
        }
        else
            modelCount = Model.Length - 1;
        OnPlayerCharacterChange();
    }
    public void OnSelectRight()//按鈕事件
    {
        if (modelCount+1< Model.Length)
        { 
             modelCount ++;
        }
        else
          modelCount = 0;
        OnPlayerCharacterChange();
    }
    public void OnPlayerCharacterChange()
    {
        for (int i = 0; i < Model.Length; i++)
        {
            Model[i].SetActive(false);
        }
        Model[modelCount].SetActive(true);
        GameManager.Instance.PlayerCharacter = modelCount;
        GameManager.Instance.SetPlayerNetworkData();
    }
    public void OnPlayerNameInputFieldChange(string value)
    {
        GameManager.Instance.PlayerName = value;
        GameManager.Instance.SetPlayerNetworkData();
    }
}

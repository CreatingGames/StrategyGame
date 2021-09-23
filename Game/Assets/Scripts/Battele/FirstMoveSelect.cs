using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstMoveSelect : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Button button;
    [SerializeField] Button senteButton;
    [SerializeField] Button goteButton;
    [SerializeField] GameObject GameObject;
    string my;
    string opponent;
    private void Start()
    {
        
    }
    public void FirstSelected()
    {
        if (my == null)
        {
            my = "���";
            text.text = "������I��ł��������i����j";
        }
        else
        {
            opponent = "���";
            showResult();
            button.interactable = true;
            senteButton.interactable = false;
            goteButton.interactable = false;
        }
    }
    public void NextSelected()
    {
        if (my == null)
        {
            my = "���";
            text.text = "������I��ł��������i����j";
        }
        else
        {
            opponent = "���";
            showResult();
            button.interactable = true;
            senteButton.interactable = false;
            goteButton.interactable = false;
        }
    }
    private void showResult()
    {
        BattleSceneController battleSceneController = GameObject.GetComponent<BattleSceneController>();
        text.text = "�����F";
        if (my == opponent)
        {
            if(UnityEngine.Random.value * 10 > 5)
            {
                if(my == "���")
                {
                    text.text += my;
                    text.text += "  ����F";
                    text.text += "���";
                    battleSceneController.FirstMove = true;
                }
                else
                {
                    text.text += my;
                    text.text += "  ����F";
                    text.text += "���";
                    battleSceneController.FirstMove = false;
                }
            }
            else
            {
                if (opponent == "���")
                {
                    text.text += "���";
                    text.text += "  ����F";
                    text.text += opponent;
                    battleSceneController.FirstMove = false;
                }
                else
                {
                    text.text += "���";
                    text.text += "  ����F";
                    text.text +=opponent;
                    battleSceneController.FirstMove = true;
                }
            }
        }
        else
        {
            text.text += my;
            text.text += "  ����F";
            text.text += opponent;
            if(my == "���")
            {
                battleSceneController.FirstMove = true;
            }
            else
            {
                battleSceneController.FirstMove = false;
            }
        }

    }
    public void OkButton()
    {
        Destroy(gameObject);
    }
}

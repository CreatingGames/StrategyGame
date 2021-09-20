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
            my = "先手";
            text.text = "先手後手を選んでください（相手）";
        }
        else
        {
            opponent = "先手";
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
            my = "後手";
            text.text = "先手後手を選んでください（相手）";
        }
        else
        {
            opponent = "後手";
            showResult();
            button.interactable = true;
            senteButton.interactable = false;
            goteButton.interactable = false;
        }
    }
    private void showResult()
    {
        BattleSceneController battleSceneController = GameObject.GetComponent<BattleSceneController>();
        text.text = "自分：";
        if (my == opponent)
        {
            if(UnityEngine.Random.value * 10 > 5)
            {
                if(my == "先手")
                {
                    text.text += my;
                    text.text += "  相手：";
                    text.text += "後手";
                    battleSceneController.FirstMove = true;
                }
                else
                {
                    text.text += my;
                    text.text += "  相手：";
                    text.text += "先手";
                    battleSceneController.FirstMove = false;
                }
            }
            else
            {
                if (opponent == "先手")
                {
                    text.text += "後手";
                    text.text += "  相手：";
                    text.text += opponent;
                    battleSceneController.FirstMove = false;
                }
                else
                {
                    text.text += "先手";
                    text.text += "  相手：";
                    text.text +=opponent;
                    battleSceneController.FirstMove = true;
                }
            }
        }
        else
        {
            text.text += my;
            text.text += "  相手：";
            text.text += opponent;
            if(my == "先手")
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

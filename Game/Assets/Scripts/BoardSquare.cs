using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquare : MonoBehaviour
{
    [SerializeField] int X;
    [SerializeField] int Y;
    [SerializeField] GameObject GameObject;
    BattleSceneController battleSceneController;
    private void Start()
    {
        battleSceneController = GameObject.GetComponent<BattleSceneController>();
    }
    public void OnClicked()
    {
        battleSceneController.OnBoardSquareClicked(X, Y);
    }
}

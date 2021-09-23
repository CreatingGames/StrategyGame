using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquare : MonoBehaviour
{
    // 升目がどこにあるか登録する。
    [SerializeField] int X;
    [SerializeField] int Y;
    [SerializeField] GameObject GameObject;
    BattleSceneController battleSceneController;
    private void Start()
    {
        battleSceneController = GameObject.GetComponent<BattleSceneController>();
    }
    // 座標を与えて、battleSceneControllerに渡す
    public void OnClicked()
    {
        battleSceneController.OnBoardSquareClicked(X, Y);
    }
}

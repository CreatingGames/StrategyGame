using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBoardSquare : MonoBehaviour
{
    // 升目がどこにあるか登録する。
    [SerializeField] int X;
    [SerializeField] int Y;
    [SerializeField] GameObject GameObject;
    CreateFormationPalette CreateFormationPalette;
    private void Start()
    {
        CreateFormationPalette = GameObject.GetComponent<CreateFormationPalette>();
    }
    // 座標を与えて、battleSceneControllerに渡す
    public void OnClicked()
    {
        CreateFormationPalette.OnBoardSquareClicked(X, Y);
    }
}

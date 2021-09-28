using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBoardSquare : MonoBehaviour
{
    // ���ڂ��ǂ��ɂ��邩�o�^����B
    [SerializeField] int X;
    [SerializeField] int Y;
    [SerializeField] GameObject GameObject;
    CreateFormationPalette CreateFormationPalette;
    private void Start()
    {
        CreateFormationPalette = GameObject.GetComponent<CreateFormationPalette>();
    }
    // ���W��^���āAbattleSceneController�ɓn��
    public void OnClicked()
    {
        CreateFormationPalette.OnBoardSquareClicked(X, Y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBoardSquare : MonoBehaviour
{
    // ¡–Ú‚ª‚Ç‚±‚É‚ ‚é‚©“o˜^‚·‚éB
    [SerializeField] int X;
    [SerializeField] int Y;
    [SerializeField] GameObject GameObject;
    CreateFormationPalette CreateFormationPalette;
    private void Start()
    {
        CreateFormationPalette = GameObject.GetComponent<CreateFormationPalette>();
    }
    // À•W‚ğ—^‚¦‚ÄAbattleSceneController‚É“n‚·
    public void OnClicked()
    {
        CreateFormationPalette.OnBoardSquareClicked(X, Y);
    }
}

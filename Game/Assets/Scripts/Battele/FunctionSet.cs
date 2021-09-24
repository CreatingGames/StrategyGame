using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ‹@”\‚Ì—ñ‹“Œ^
public enum Functions
{
    Move,
    Create,
    Evolve,
    Skip
}
public class FunctionSet : MonoBehaviour
{
    [SerializeField] Functions Function;
    [SerializeField] GameObject BattleSceneController;

    BattleSceneController battleSceneController;
    private void Start()
    {
        battleSceneController = BattleSceneController.GetComponent<BattleSceneController>();
    }
    public void OnClicked()
    {
        battleSceneController.Function = Function;
        switch (battleSceneController.Function)
        {
            case Functions.Move:
                break;
            case Functions.Create:
                Debug.Log("Create");
                battleSceneController.RestMovingPieceSelected();
                break;
            case Functions.Evolve:
                Debug.Log("Evolve");
                battleSceneController.RestMovingPieceSelected();
                break;
            case Functions.Skip:
                battleSceneController.RestMovingPieceSelected();
                battleSceneController.Skip();
                break;
        }
    }
}

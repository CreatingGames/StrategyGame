using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Functions
{
    Move,
    Create,
    Evolve
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
    }
}

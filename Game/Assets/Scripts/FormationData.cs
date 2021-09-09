using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FormationData : MonoBehaviour
{
    public PieceData[,] MyFormationBoard;
    public PieceData[,] OpponentFormationBoard;
    public bool InitializedMyformation = false;
    public bool InitializedOpponentformation = false;
    public void InitMyFormationData()
    {
        MyFormationBoard = new PieceData[2, 5];
        MyFormationBoard[0, 2] = gameObject.AddComponent<PieceData>();
        MyFormationBoard[0, 2].Forward = 3;
        MyFormationBoard[0, 2].Backward = 3;
        MyFormationBoard[0, 2].Right = 3;
        MyFormationBoard[0, 2].Left = 3;
        MyFormationBoard[0, 2].UpperLeft = 3;
        MyFormationBoard[0, 2].LowerLeft = 3;
        MyFormationBoard[0, 2].UpperRight = 3;
        MyFormationBoard[0, 2].LowerRight = 3;

        MyFormationBoard[0, 1] = gameObject.AddComponent<PieceData>();
        MyFormationBoard[0, 1].Forward = 1;
        MyFormationBoard[0, 1].Backward = 1;
        MyFormationBoard[0, 1].Right = 1;
        MyFormationBoard[0, 1].Left = 1;
        MyFormationBoard[0, 1].UpperLeft = 1;
        MyFormationBoard[0, 1].LowerLeft = 1;
        MyFormationBoard[0, 1].UpperRight = 1;
        MyFormationBoard[0, 1].LowerRight = 1;
        InitializedMyformation = true;

    }
    public void InitOpponentFormationData()
    {
        OpponentFormationBoard = new PieceData[2, 5];
        OpponentFormationBoard[0, 2] = gameObject.AddComponent<PieceData>();
        OpponentFormationBoard[0, 2].Forward = 0;
        OpponentFormationBoard[0, 2].Backward = 1;
        OpponentFormationBoard[0, 2].Right = 0;
        OpponentFormationBoard[0, 2].Left = 0;
        OpponentFormationBoard[0, 2].UpperLeft = 3;
        OpponentFormationBoard[0, 2].LowerLeft = 1;
        OpponentFormationBoard[0, 2].UpperRight = 3;
        OpponentFormationBoard[0, 2].LowerRight = 1;
        InitializedOpponentformation = true;
    }
}

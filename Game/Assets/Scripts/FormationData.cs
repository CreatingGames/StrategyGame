using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FormationData : MonoBehaviour
{
    public PieceData[,] shortBoard;
    public bool isComplete = false;
    public void InitFormationData()
    {
        shortBoard = new PieceData[2, 5];
        shortBoard[0, 2] = gameObject.AddComponent<PieceData>();
        shortBoard[0, 2].Forward = 3;
        shortBoard[0, 2].Backward = 3;
        shortBoard[0, 2].Right = 3;
        shortBoard[0, 2].Left = 3;
        shortBoard[0, 2].UpperLeft = 3;
        shortBoard[0, 2].LowerLeft = 3;
        shortBoard[0, 2].UpperRight = 3;
        shortBoard[0, 2].LowerRight = 3;
        isComplete = true;

    }

}

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
        shortBoard[0, 2].Forward = 1;
        shortBoard[0, 2].Backward = 1;
        shortBoard[0, 2].Right = 1;
        shortBoard[0, 2].Left = 1;
        shortBoard[0, 2].UpperLeft = 1;
        shortBoard[0, 2].LowerLeft = 1;
        shortBoard[0, 2].UpperRight = 1;
        shortBoard[0, 2].LowerLeft = 1;
        isComplete = true;

    }

}

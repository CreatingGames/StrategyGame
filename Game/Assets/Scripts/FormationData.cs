using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FormationData : MonoBehaviour
{
    public PieceData[,] MyFormationBoard;
    public bool isComplete = false;
    public void InitFormationData()
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
        isComplete = true;

    }

}

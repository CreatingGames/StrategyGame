using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FormationData : MonoBehaviour
{
    public PieceData[,] shortBoard;
    private void Start()
    {
        shortBoard = new PieceData[2, 5];
        shortBoard[1, 2] = gameObject.AddComponent<PieceData>();
        shortBoard[1, 2].Forward = 1;
        if (shortBoard[1, 2] == null)
        {
            print("This is NULL");
        }

    }
}

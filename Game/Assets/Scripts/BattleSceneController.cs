using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] GameObject ParentBoard;
    // Test
    [SerializeField] int x;
    [SerializeField] int y;

    private GameObject[,] ChildBoard;
    private void Start()
    {
        GetAllChildBoard();
        ChangeImageTransparency(x, y);
    }
    // インスペクターで取得したBoardから子要素のそれぞれのImageを取得する
    private void GetAllChildBoard()
    {
        int boardSize;
        if (ParentBoard.transform.childCount == 5 * 5)
        {
            boardSize = 5;
        }
        else if (ParentBoard.transform.childCount == 6 * 6)
        {
            boardSize = 6;
        }
        else
        {
            boardSize = 7;
        }
        ChildBoard = new GameObject[boardSize, boardSize];
        int index = 0;
        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                ChildBoard[i, j] = ParentBoard.transform.GetChild(index).gameObject;
                index++;
            }
        }
    }
    public void ChangeImageTransparency(int x,int y)
    {
        float red = ChildBoard[y, x].GetComponent<Image>().color.r;
        float green = ChildBoard[y, x].GetComponent<Image>().color.g;
        float blue = ChildBoard[y, x].GetComponent<Image>().color.b;
        float alfa = 255;
        ChildBoard[y, x].GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] GameObject ParentBoard;
    [SerializeField] GameObject Formation;
    [SerializeField] GameObject PiecePrefab;
    [SerializeField] GameObject Canvas;
    // Test
    [SerializeField] int x;
    [SerializeField] int y;

    private GameObject[,] ChildBoard;// 盤のマス
    public GameObject[,] GameBoard; // 盤面の管理
    private int BoardSize;// 盤のサイズ
    private Vector3[,] ChildBoardPosition;
    private float piecePositionZ = -50;
    private void Start()
    {
        if ((int)Mathf.Sqrt(ParentBoard.transform.childCount) == Mathf.Sqrt(ParentBoard.transform.childCount))
        {
            BoardSize = (int)Mathf.Sqrt(ParentBoard.transform.childCount);
        }

        GameBoard = new GameObject[BoardSize, BoardSize];
        GetAllChildBoard();
        GetAllChildBoardPosition();
        ChangeImageTransparency(x, y);
        LoadFormation();
    }
    // インスペクターで取得したBoardから子要素のそれぞれのImageを取得する
    private void GetAllChildBoard()
    {
        ChildBoard = new GameObject[BoardSize, BoardSize];
        int index = 0;
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                ChildBoard[i, j] = ParentBoard.transform.GetChild(index).gameObject;
                index++;
            }
        }
    }
    private void GetAllChildBoardPosition()
    {
        ChildBoardPosition = new Vector3[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                ChildBoardPosition[i, j] = ChildBoard[i, j].transform.position;
                ChildBoardPosition[i, j].z = piecePositionZ;
            }
        }
    }
    public void ChangeImageTransparency(int x, int y)
    {
        float red = ChildBoard[y, x].GetComponent<Image>().color.r;
        float green = ChildBoard[y, x].GetComponent<Image>().color.g;
        float blue = ChildBoard[y, x].GetComponent<Image>().color.b;
        float alfa = 255;
        ChildBoard[y, x].GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
    public void LoadFormation()
    {
        PieceData[,] shortBoard = Formation.GetComponent<FormationData>().shortBoard;
        // ここはいったんメタにループ回数を決める
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (shortBoard[i, j] != null)
                {
                    GameBoard[i + 3, j] = Instantiate(PiecePrefab, ChildBoardPosition[i + 3, j], Quaternion.identity);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitActionRange(shortBoard[i, j].UpperLeft, shortBoard[i, j].LowerLeft, shortBoard[i, j].UpperRight, shortBoard[i, j].LowerRight, shortBoard[i, j].Left, shortBoard[i, j].Right, shortBoard[i, j].Forward, shortBoard[i, j].Backward);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitPosition(i + 3, j);
                }
            }
        }
    }
}

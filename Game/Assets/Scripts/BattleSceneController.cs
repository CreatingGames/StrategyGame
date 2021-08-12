using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] GameObject Board;
    [SerializeField] GameObject Formation;
    [SerializeField] GameObject MyPiecePrefab;
    [SerializeField] GameObject OpponentPiecePrefab;
    [SerializeField] GameObject Canvas;
    [Header("Test用変数")]
    [SerializeField] int x;
    [SerializeField] int y;
    [Header("升目の透明度")]
    [SerializeField] float Opacity; // 不透明にするときの値
    [SerializeField] float Transparency;// 透明にするときの値

    private GameObject[,] BoardSquar;// 盤のマス
    public GameObject[,] GameBoard; // 盤面の管理
    public int BoardSize;// 盤のサイズ
    private Vector3[,] BoardSquarPosition;// 升目の座標
    private Vector3 piecePositionZ = new Vector3(0.0f, 0.0f, -0.46296296296f);// 生成されるオブジェクト位置をz軸-50にするためにメタ的にこうしてる。
    private Color defaultColor;
    private Color opponentOpaqueColor;
    private Color myOpaqueColor;
    /*
     * 現在の設定だと生成するオブジェクトをCanvasに追加して、ｚ軸を動かそうとすると１０８倍される。
     * 原因は不明、調査が必要。
     */
    private void Start()
    {
        if ((int)Mathf.Sqrt(Board.transform.childCount) == Mathf.Sqrt(Board.transform.childCount))
        {
            BoardSize = (int)Mathf.Sqrt(Board.transform.childCount);
        }
        InitBoardSquarColor();
        GameBoard = new GameObject[BoardSize, BoardSize];
        GetAllBoardSquar();
        GetAllBoardSquarPosition();
        //MakeBoardSquarOpaque(x, y);
        StartCoroutine(LoadMyFormation());
        StartCoroutine(LoadOpponentFormation());
    }
    // 升目の色合いの調整
    private void InitBoardSquarColor()
    {
        Opacity = 200;
        Transparency = 30;
        defaultColor = new Color(0, 1, 1, Transparency / 255);
        opponentOpaqueColor = new Color(1, 0, 1, Opacity / 255);
        myOpaqueColor = new Color(0, 1, 1, Opacity / 255);
    }
    // インスペクターで取得したBoardから子要素のそれぞれのImageを取得する
    private void GetAllBoardSquar()
    {
        BoardSquar = new GameObject[BoardSize, BoardSize];
        int index = 0;
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                BoardSquar[i, j] = Board.transform.GetChild(index).gameObject;
                index++;
            }
        }
    }
    private void GetAllBoardSquarPosition()
    {
        BoardSquarPosition = new Vector3[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                BoardSquarPosition[i, j] = BoardSquar[i, j].transform.position + piecePositionZ;

            }
        }
    }
    // 升目を不透明にする
    public void MakeBoardSquarOpaque(int x, int y, bool opponent)
    {
        if (opponent) BoardSquar[y, x].GetComponent<Image>().color = opponentOpaqueColor;
        else BoardSquar[y, x].GetComponent<Image>().color = myOpaqueColor;
    }
    // 升目を全て透明にする
    public void MakeAllBoardSquarTransparent()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                BoardSquar[i, j].GetComponent<Image>().color = defaultColor;
            }
        }
    }
    private IEnumerator LoadMyFormation()
    {
        Formation.GetComponent<FormationData>().InitMyFormationData();
        while (!Formation.GetComponent<FormationData>().InitializedMyformation)
        {
            // childのisComplete変数がtrueになるまで待機
            yield return new WaitForEndOfFrame();
        }
        PieceData[,] myFormationBoard = Formation.GetComponent<FormationData>().MyFormationBoard;
        // ここはいったんメタにループ回数を決める
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (myFormationBoard[i, j] != null)
                {
                    GameBoard[i + 3, j] = (GameObject)Instantiate(MyPiecePrefab, BoardSquarPosition[i + 3, j], Quaternion.identity, Canvas.transform);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitActionRange(myFormationBoard[i, j].UpperLeft, myFormationBoard[i, j].LowerLeft, myFormationBoard[i, j].UpperRight, myFormationBoard[i, j].LowerRight, myFormationBoard[i, j].Left, myFormationBoard[i, j].Right, myFormationBoard[i, j].Forward, myFormationBoard[i, j].Backward);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitPosition(j, i + 3);
                    GameBoard[i + 3, j].GetComponent<Piece>().Opponent = false;
                }
            }
        }
    }
    private IEnumerator LoadOpponentFormation()
    {
        Formation.GetComponent<FormationData>().InitOpponentFormationData();
        while (!Formation.GetComponent<FormationData>().InitializedOpponentformation)
        {
            // childのisComplete変数がtrueになるまで待機
            yield return new WaitForEndOfFrame();
        }
        PieceData[,] opponentFormationBoard = Formation.GetComponent<FormationData>().OpponentFormationBoard;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (opponentFormationBoard[i, j] != null)
                {
                    // 敵のデータを自分の盤面に反映するように変換
                    int x = 4 - j;
                    int y = 1 - i;
                    int upperLeft = opponentFormationBoard[i, j].LowerRight;
                    int lowerLeft = opponentFormationBoard[i, j].UpperRight;
                    int upperRight = opponentFormationBoard[i, j].LowerLeft;
                    int lowerRight = opponentFormationBoard[i, j].UpperLeft;
                    int left = opponentFormationBoard[i, j].Right;
                    int right = opponentFormationBoard[i, j].Left;
                    int forward = opponentFormationBoard[i, j].Backward;
                    int backward = opponentFormationBoard[i, j].Forward;
                    GameBoard[y, x] = (GameObject)Instantiate(OpponentPiecePrefab, BoardSquarPosition[y, x], Quaternion.identity, Canvas.transform);
                    GameBoard[y, x].GetComponent<Piece>().InitActionRange(upperLeft, lowerLeft, upperRight, lowerRight, left, right, forward, backward);
                    GameBoard[y, x].GetComponent<Piece>().InitPosition(x, y);
                    GameBoard[y, x].GetComponent<Piece>().Opponent = true;
                }
            }
        }
    }
}

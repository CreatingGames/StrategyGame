using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] GameObject Board;
    [SerializeField] GameObject Formation;
    [SerializeField] GameObject PiecePrefab;
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

        GameBoard = new GameObject[BoardSize, BoardSize];
        GetAllBoardSquar();
        GetAllBoardSquarPosition();
        //MakeBoardSquarOpaque(x, y);
        StartCoroutine(LoadFormation());
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
    public void MakeBoardSquarOpaque(int x, int y)
    {
        float red = BoardSquar[y, x].GetComponent<Image>().color.r;
        float green = BoardSquar[y, x].GetComponent<Image>().color.g;
        float blue = BoardSquar[y, x].GetComponent<Image>().color.b;
        float alfa = Opacity / 255;
        BoardSquar[y, x].GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
    // 升目を全て透明にする
    public void MakeAllBoardSquarTransparent()
    {
        for(int i = 0; i < BoardSize; i++)
        {
            for(int j = 0; j < BoardSize; j++)
            {
                float red = BoardSquar[i, j].GetComponent<Image>().color.r;
                float green = BoardSquar[i, j].GetComponent<Image>().color.g;
                float blue = BoardSquar[i, j].GetComponent<Image>().color.b;
                float alfa = Transparency / 255;
                BoardSquar[i, j].GetComponent<Image>().color = new Color(red, green, blue, alfa);
            }
        }
    }
    private IEnumerator LoadFormation()
    {
        Formation.GetComponent<FormationData>().InitFormationData();
        while (!Formation.GetComponent<FormationData>().isComplete)
        {
            // childのisComplete変数がtrueになるまで待機
            yield return new WaitForEndOfFrame();
        }
        PieceData[,] shortBoard = Formation.GetComponent<FormationData>().shortBoard;
        // ここはいったんメタにループ回数を決める
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Formation.GetComponent<FormationData>().shortBoard[i, j] != null)
                {
                    GameBoard[i + 3, j] = (GameObject)Instantiate(PiecePrefab, BoardSquarPosition[i + 3, j], Quaternion.identity, Canvas.transform);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitActionRange(shortBoard[i, j].UpperLeft, shortBoard[i, j].LowerLeft, shortBoard[i, j].UpperRight, shortBoard[i, j].LowerRight, shortBoard[i, j].Left, shortBoard[i, j].Right, shortBoard[i, j].Forward, shortBoard[i, j].Backward);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitPosition(j, i + 3);
                }
            }
        }
    }
}

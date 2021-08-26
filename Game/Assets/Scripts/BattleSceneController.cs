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
    [Header("StrategyPoint")]
    [SerializeField] int MySP;
    [SerializeField] int OpponentSP;
    [SerializeField] Text MySPText;
    [SerializeField] Text OpponentSPText;
    [Header("升目の透明度")]
    [SerializeField] float Opacity; // 不透明にするときの値
    [SerializeField] float Transparency;// 透明にするときの値
    [Header("盤のサイズ")]
    public int BoardSize;// 盤のサイズ
    [Header("テキスト")]
    [SerializeField] Text ModeText;
    [SerializeField] Text ActionNumberText;
    [SerializeField] Button ResetButton;
    [SerializeField] Button EnterButton;
    private GameObject[,] BoardSquare;// 盤のマス
    public GameObject[,] GameBoard; // 盤面の管理
    public GameObject[,] GameBoardBuffer; // 盤面の管理（ターン開始時の状況を保存）
    public List<GameObject[,]> ActionGameBoard; // 盤面の管理（１手毎の状況を保存）
    private Vector3[,] BoardSquarPosition;// 升目の座標
    private Vector3 piecePositionZ = new Vector3(0.0f, 0.0f, -0.46296296296f);// 生成されるオブジェクト位置をz軸-50にするためにメタ的にこうしてる。
    // 色の変更のための変数
    private Color defaultColor;
    private Color opponentOpaqueColor;
    private Color myOpaqueColor;
    // 駒を移動させるための変数
    private int movingPositionX = 0;
    private int movingPositionY = 0;
    private bool[,] onBoardActionRange = new bool[5, 5];
    public bool movingPieceSelected = false;
    public bool nowMoving = false;
    private static int actionNumber = 0;
    public int ActionMax = 3;
    public Functions Function { get; set; }// 移動・生成・進化のどのモードが選択されてるかを格納するための変数

    private void Start()
    {
        // 盤面のサイズを取得している
        if ((int)Mathf.Sqrt(Board.transform.childCount) == Mathf.Sqrt(Board.transform.childCount))
        {
            BoardSize = (int)Mathf.Sqrt(Board.transform.childCount);
        }
        InitBoardSquarColor();
        GameBoard = new GameObject[BoardSize, BoardSize];
        GameBoardBuffer = new GameObject[BoardSize, BoardSize];
        GetAllBoardSquare();
        GetAllBoardSquarePosition();
        StartCoroutine(LoadMyFormation());
        StartCoroutine(LoadOpponentFormation());
        MySP = StrategyPointSetting.InitialStrategyPoint;
        OpponentSP = StrategyPointSetting.InitialStrategyPoint;
        ActionGameBoard = new List<GameObject[,]>();
        for (int i = 0; i < ActionMax; i++)
        {
            ActionGameBoard.Add(new GameObject[BoardSize, BoardSize]);
        }
        ResetButton.interactable = false;
        EnterButton.interactable = false;
    }
    private void Update()
    {
        // 選択している機能（移動・生成・進化）をテキストに入れている
        TextUpdate();
    }

    private void TextUpdate()
    {
        ModeText.text = Function.ToString();
        MySPText.text = MySP.ToString();
        OpponentSPText.text = OpponentSP.ToString();
        if(actionNumber == ActionMax)
        {
            ActionNumberText.text = "全手登録済み";
        }
        else
        {
            ActionNumberText.text = (actionNumber + 1).ToString() + "手目";
        }
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
    private void GetAllBoardSquare()
    {
        BoardSquare = new GameObject[BoardSize, BoardSize];
        int index = 0;
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                BoardSquare[i, j] = Board.transform.GetChild(index).gameObject;
                index++;
            }
        }
    }
    private void GetAllBoardSquarePosition()
    {
        BoardSquarPosition = new Vector3[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                BoardSquarPosition[i, j] = BoardSquare[i, j].transform.position + piecePositionZ;

            }
        }
    }
    // 升目を不透明にする
    public void MakeBoardSquarOpaque(int x, int y, bool opponent)
    {
        if (opponent)
        {
            BoardSquare[y, x].GetComponent<Image>().color = opponentOpaqueColor;
        }
        else
        {
            BoardSquare[y, x].GetComponent<Image>().color = myOpaqueColor;
        }
    }
    // 升目を全て透明にする
    public void MakeAllBoardSquarTransparent()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                BoardSquare[i, j].GetComponent<Image>().color = defaultColor;
            }
        }
    }
    // Formationで初期化された自陣を呼び出して、GameBoardに登録し、Prefabから駒を生成してる
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
                    GameBoard[i + 3, j].GetComponent<Piece>().StrategyPoint = StrategyPointSetting.CalcuratePieaceStrategyPoint(GameBoard[i + 3, j].GetComponent<Piece>());
                    GameBoard[i + 3, j].GetComponent<Piece>().ToInspector();
                }
            }
        }
        CopyGameBoard(GameBoard, GameBoardBuffer);
    }
    // Formationで初期化された敵陣を呼び出して、GameBoardに登録し、Prefabから駒を生成してる
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
                    GameBoard[y, x].GetComponent<Piece>().StrategyPoint = StrategyPointSetting.CalcuratePieaceStrategyPoint(GameBoard[y, x].GetComponent<Piece>());
                    GameBoard[y, x].GetComponent<Piece>().ToInspector();
                }
            }
        }
        CopyGameBoard(GameBoard, GameBoardBuffer);
    }
    // 駒を選択したときに移動移動可能範囲を決定する。
    public void SetActionRange(int x, int y)
    {
        movingPositionX = x;
        movingPositionY = y;
        movingPieceSelected = true;
        InitonBoardActionRange();
        Piece piece = GameBoard[movingPositionY, movingPositionX].GetComponent<Piece>();
        if (piece.Forward != 0)
        {
            int minY;
            if (piece.PositionY - piece.Forward > 0)
            {
                minY = piece.PositionY - piece.Forward;
            }
            else
            {
                minY = 0;
            }
            for (int i = piece.PositionY - 1; i >= minY; i--)
            {
                if (GameBoard[i, x] != null)
                {
                    if (GameBoard[i, x].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[i, piece.PositionX] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[i, piece.PositionX] = true;
            }
        }
        if (piece.Backward != 0)
        {
            int maxY;
            if (piece.PositionY + piece.Backward < BoardSize)
            {
                maxY = piece.PositionY + piece.Backward;
            }
            else
            {
                maxY = BoardSize - 1;
            }
            for (int i = piece.PositionY + 1; i <= maxY; i++)
            {
                if (GameBoard[i, x] != null)
                {
                    if (GameBoard[i, x].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[i, piece.PositionX] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[i, piece.PositionX] = true;
            }
        }
        if (piece.Left != 0)
        {
            int minX;
            if (piece.PositionX - piece.Left > 0)
            {
                minX = piece.PositionX - piece.Left;
            }
            else
            {
                minX = 0;
            }
            for (int i = piece.PositionX - 1; i >= minX; i--)
            {
                if (GameBoard[y, i] != null)
                {
                    if (GameBoard[y, i].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[piece.PositionY, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[piece.PositionY, i] = true;
            }
        }
        if (piece.Right != 0)
        {
            int maxX;
            if (piece.PositionX + piece.Right < BoardSize)
            {
                maxX = piece.PositionX + piece.Right;
            }
            else
            {
                maxX = BoardSize - 1;
            }
            for (int i = piece.PositionX + 1; i <= maxX; i++)
            {
                if (GameBoard[y, i] != null)
                {
                    if (GameBoard[y, i].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[piece.PositionY, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[piece.PositionY, i] = true;
            }
        }
        if (piece.UpperLeft != 0)
        {
            int minX;
            if (piece.PositionX - piece.UpperLeft > 0)
            {
                minX = piece.PositionX - piece.UpperLeft;
            }
            else
            {
                minX = 0;
            }
            int minY;
            if (piece.PositionY - piece.UpperLeft > 0)
            {
                minY = piece.PositionY - piece.UpperLeft;
            }
            else
            {
                minY = 0;
            }
            for (int i = piece.PositionX - 1, j = piece.PositionY - 1; i >= minX && j >= minY; i--, j--)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[j, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[j, i] = true;
            }
        }
        if (piece.UpperRight != 0)
        {
            int maxX;
            if (piece.PositionX + piece.UpperRight < BoardSize)
            {
                maxX = piece.PositionX + piece.UpperRight;
            }
            else
            {
                maxX = BoardSize - 1;
            }
            int minY;
            if (piece.PositionY - piece.UpperRight > 0)
            {
                minY = piece.PositionY - piece.UpperRight;
            }
            else
            {
                minY = 0;
            }
            for (int i = piece.PositionX + 1, j = piece.PositionY - 1; i <= maxX && j >= minY; i++, j--)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[j, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[j, i] = true;
            }
        }
        if (piece.LowerLeft != 0)
        {
            int minX;
            if (piece.PositionX - piece.LowerLeft > 0)
            {
                minX = piece.PositionX - piece.LowerLeft;
            }
            else
            {
                minX = 0;
            }
            int maxY;
            if (piece.PositionY + piece.LowerLeft < BoardSize)
            {
                maxY = piece.PositionY + piece.LowerLeft;
            }
            else
            {
                maxY = BoardSize - 1;
            }
            for (int i = piece.PositionX - 1, j = piece.PositionY + 1; i >= minX && j <= maxY; i--, j++)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[j, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[j, i] = true;
            }
        }
        if (piece.LowerRight != 0)
        {
            int maxX;
            if (piece.PositionX + piece.LowerRight < BoardSize)
            {
                maxX = piece.PositionX + piece.LowerRight;
            }
            else
            {
                maxX = BoardSize - 1;
            }
            int maxY;
            if (piece.PositionY + piece.LowerRight < BoardSize)
            {
                maxY = piece.PositionY + piece.LowerRight;
            }
            else
            {
                maxY = BoardSize - 1;
            }
            for (int i = piece.PositionX + 1, j = piece.PositionY + 1; i <= maxX && j <= maxY; i++, j++)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        onBoardActionRange[j, i] = true;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                onBoardActionRange[j, i] = true;
            }
        }
    }
    // 駒の移動可能範囲をハイライトする
    public void HighlightActionRange(int x, int y)
    {
        Piece piece = GameBoard[y, x].GetComponent<Piece>();
        bool opponent = piece.Opponent;
        if (piece.Forward != 0)
        {
            int minY;
            if (piece.PositionY - piece.Forward > 0)
            {
                minY = piece.PositionY - piece.Forward;
            }
            else
            {
                minY = 0;
            }
            for (int i = piece.PositionY - 1; i >= minY; i--)
            {
                if (GameBoard[i, x] != null)
                {
                    if (GameBoard[i, x].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(x, i, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(x, i, opponent);
            }
        }
        if (piece.Backward != 0)
        {
            int maxY;
            if (piece.PositionY + piece.Backward < BoardSize)
            {
                maxY = piece.PositionY + piece.Backward;
            }
            else
            {
                maxY = BoardSize - 1;
            }
            for (int i = piece.PositionY + 1; i <= maxY; i++)
            {
                if (GameBoard[i, x] != null)
                {
                    if (GameBoard[i, x].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(x, i, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(x, i, opponent);
            }
        }
        if (piece.Left != 0)
        {
            int minX;
            if (piece.PositionX - piece.Left > 0)
            {
                minX = piece.PositionX - piece.Left;
            }
            else
            {
                minX = 0;
            }
            for (int i = piece.PositionX - 1; i >= minX; i--)
            {
                if (GameBoard[y, i] != null)
                {
                    if (GameBoard[y, i].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(i, y, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(i, y, opponent);
            }
        }
        if (piece.Right != 0)
        {
            int maxX;
            if (piece.PositionX + piece.Right < BoardSize)
            {
                maxX = piece.PositionX + piece.Right;
            }
            else
            {
                maxX = BoardSize - 1;
            }
            for (int i = piece.PositionX + 1; i <= maxX; i++)
            {
                if (GameBoard[y, i] != null)
                {
                    if (GameBoard[y, i].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(i, y, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(i, y, opponent);
            }
        }
        if (piece.UpperLeft != 0)
        {
            int minX;
            if (piece.PositionX - piece.UpperLeft > 0)
            {
                minX = piece.PositionX - piece.UpperLeft;
            }
            else
            {
                minX = 0;
            }
            int minY;
            if (piece.PositionY - piece.UpperLeft > 0)
            {
                minY = piece.PositionY - piece.UpperLeft;
            }
            else
            {
                minY = 0;
            }
            for (int i = piece.PositionX - 1, j = piece.PositionY - 1; i >= minX && j >= minY; i--, j--)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(i, j, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(i, j, opponent);
            }
        }
        if (piece.UpperRight != 0)
        {
            int maxX;
            if (piece.PositionX + piece.UpperRight < BoardSize)
            {
                maxX = piece.PositionX + piece.UpperRight;
            }
            else
            {
                maxX = BoardSize - 1;
            }
            int minY;
            if (piece.PositionY - piece.UpperRight > 0)
            {
                minY = piece.PositionY - piece.UpperRight;
            }
            else
            {
                minY = 0;
            }
            for (int i = piece.PositionX + 1, j = piece.PositionY - 1; i <= maxX && j >= minY; i++, j--)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(i, j, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(i, j, opponent);
            }
        }
        if (piece.LowerLeft != 0)
        {
            int minX;
            if (piece.PositionX - piece.LowerLeft > 0)
            {
                minX = piece.PositionX - piece.LowerLeft;
            }
            else
            {
                minX = 0;
            }
            int maxY;
            if (piece.PositionY + piece.LowerLeft < BoardSize)
            {
                maxY = piece.PositionY + piece.LowerLeft;
            }
            else
            {
                maxY = BoardSize - 1;
            }
            for (int i = piece.PositionX - 1, j = piece.PositionY + 1; i >= minX && j <= maxY; i--, j++)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(i, j, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(i, j, opponent);
            }
        }
        if (piece.LowerRight != 0)
        {
            int maxX;
            if (piece.PositionX + piece.LowerRight < BoardSize)
            {
                maxX = piece.PositionX + piece.LowerRight;
            }
            else
            {
                maxX = BoardSize - 1;
            }
            int maxY;
            if (piece.PositionY + piece.LowerRight < BoardSize)
            {
                maxY = piece.PositionY + piece.LowerRight;
            }
            else
            {
                maxY = BoardSize - 1;
            }
            for (int i = piece.PositionX + 1, j = piece.PositionY + 1; i <= maxX && j <= maxY; i++, j++)
            {
                if (GameBoard[j, i] != null)
                {
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent)
                    {
                        MakeBoardSquarOpaque(i, j, opponent);
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
                MakeBoardSquarOpaque(i, j, opponent);
            }
        }
    }
    // 駒を選択した状態を解除する
    public void RestMovingPieceSelected()
    {
        if (movingPieceSelected)
        {
            MakeAllBoardSquarTransparent();
            Piece piece = GameBoard[movingPositionY, movingPositionX].GetComponent<Piece>();
            piece.readyMove = false;
            movingPieceSelected = false;
            InitonBoardActionRange();
        }
    }
    // 行動可能範囲用の変数を初期化する
    private void InitonBoardActionRange()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                onBoardActionRange[i, j] = false;
            }
        }
    }
    // 升目をクリックしたときに動作する。
    public void OnBoardSquareClicked(int x, int y)
    {
        if (onBoardActionRange[y, x] && Function == Functions.Move)
        {
            if (GameBoard[y, x] != null)
            {
                if (nowMoving)
                {
                    BreakingPiece(x, y);
                }
                else
                {
                    GameBoard[y, x].SetActive(false);
                }
            }
            if (y < 2 && !GameBoard[movingPositionY, movingPositionX].GetComponent<Piece>().Invasion && nowMoving)
            {
                InvasionOpponentFormation();
            }
            GameBoard[y, x] = GameBoard[movingPositionY, movingPositionX];
            GameBoard[y, x].GetComponent<Piece>().InitPosition(x, y);
            GameBoard[y, x].GetComponent<Piece>().readyMove = false;
            GameBoard[y, x].transform.position = BoardSquarPosition[y, x];
            GameBoard[movingPositionY, movingPositionX] = null;
            movingPieceSelected = false;
            InitonBoardActionRange();
            MakeAllBoardSquarTransparent();
            CopyGameBoard(GameBoard, ActionGameBoard[actionNumber]);
            actionNumber++;
            ResetButton.interactable = true;
            if(actionNumber == ActionMax)
            {
                nowMoving = true;
                EnterButton.interactable = true;
            }
        }
    }

    private void InvasionOpponentFormation()
    {
        MySP += StrategyPointSetting.CalcurateInvasionPoint;
        GameBoard[movingPositionY, movingPositionX].GetComponent<Piece>().Invasion = true;
    }

    private void BreakingPiece(int x, int y)
    {
        MySP += StrategyPointSetting.CalcurateBreakingPiecePoints(GameBoard[y, x].GetComponent<Piece>());
        Destroy(GameBoard[y, x]);
    }

    public void ChangeOpponentFlag()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (GameBoard[i, j] != null)
                {
                    GameBoard[i, j].GetComponent<Piece>().Opponent = !GameBoard[i, j].GetComponent<Piece>().Opponent;
                }
            }
        }
    }
    public void ResetGameBoard()
    {
        if(actionNumber > 1)
        {
            actionNumber--;
            ResetPiecePosition(ActionGameBoard[actionNumber - 1]);
            CopyGameBoard(ActionGameBoard[actionNumber], GameBoard);
        }
        else if(actionNumber == 1)
        {
            actionNumber--;
            ResetPiecePosition(GameBoardBuffer);
            CopyGameBoard(GameBoardBuffer, GameBoard);
            ResetButton.interactable = false;
        }
    }
    private void ResetPiecePosition(GameObject[,] gameObjects)
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (gameObjects[i, j] != null)
                {
                    gameObjects[i, j].GetComponent<Piece>().InitPosition(j, i);
                    gameObjects[i, j].transform.position = BoardSquarPosition[i, j];
                    gameObjects[i, j].GetComponent<Piece>().ToInspector();
                    gameObjects[i, j].SetActive(true);
                }
            }
        }
    }
    // gameObjects1をgameObjects2にコピーする
    private void CopyGameBoard(GameObject[,] gameObjects1, GameObject[,] gameObjects2)
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                gameObjects2[i, j] = gameObjects1[i, j];
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] GameObject Board;
    [SerializeField] GameObject Formation;
    [SerializeField] GameObject MyPiecePrefab;
    [SerializeField] GameObject MyPiecePrefabKing;
    [SerializeField] GameObject OpponentPiecePrefab;
    [SerializeField] GameObject OpponentPiecePrefabKing;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject createPalette;
    [SerializeField] GameObject evolvePalette;
    [SerializeField] GameObject boardSquareClickController;
    [SerializeField] GameObject FinishBattle;
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
    [SerializeField] Text MoveText;
    [SerializeField] Button ResetButton;
    [SerializeField] Button EnterButton;
    [SerializeField] Button NextButton;
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
    private Color whiteColor;
    // 駒を移動させるための変数
    private int movingPositionX = 0;
    private int movingPositionY = 0;
    private bool[,] onBoardActionRange = new bool[5, 5]; // 移動可能先を判別するための配列
    public bool movingPieceSelected = false; // 駒がクリックされ、移動する駒が選択されている状態化を判別
    public bool StopMoving = false; // 駒がクリックされた時に移動できないようにする
    private static int actionNumber = 0; // 何手目かを記録するための変数
    public int ActionMax = 3; // 何手まで登録するかの変数
    private List<ActionData> myActionData; // 自身の手を登録するためのリスト
    private List<ActionData> opponentActionData; // 敵の手を登録するためのリスト
    private List<ActionData> totalActionData; // 全体の手を登録するためのリスト
    private int totalActionDataIndex = 0; // 全体の手を登録するためのリストのインデックスに使用する変数
    private bool myTurn = true; // 自身の手を登録するターンか相手の手を登録するターンかを判別するための変数
    public bool FirstMove = false; // 自身が先手が後手かture：先手　false：後手
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
        myActionData = new List<ActionData>();
        opponentActionData = new List<ActionData>();
        totalActionData = new List<ActionData>();
        for (int i = 0; i < ActionMax; i++)
        {
            ActionGameBoard.Add(new GameObject[BoardSize, BoardSize]);
            myActionData.Add(new ActionData() { });
            opponentActionData.Add(new ActionData() { });
            totalActionData.Add(new ActionData() { });
            totalActionData.Add(new ActionData() { });
        }
        InitButton();
    }
    // ボタンの状態を押せないようにしている
    private void InitButton()
    {
        ResetButton.interactable = false;
        EnterButton.interactable = false;
        NextButton.interactable = false;
    }

    private void Update()
    {
        // 選択している機能（移動・生成・進化）をテキストに入れている
        TextUpdate();
    }
    // UIに表示されるテキストの更新
    private void TextUpdate()
    {
        ModeText.text = Function.ToString();
        MySPText.text = MySP.ToString();
        OpponentSPText.text = OpponentSP.ToString();
        if (actionNumber == ActionMax)
        {
            ActionNumberText.text = "全手登録済み";
        }
        else
        {
            ActionNumberText.text = (actionNumber + 1).ToString() + "手目";
        }
        if (myTurn)
        {
            if (FirstMove)
            {
                MoveText.text = "先手";
            }
            else
            {
                MoveText.text = "後手";
            }
        }
        else
        {
            if (FirstMove)
            {
                MoveText.text = "後手";
            }
            else
            {
                MoveText.text = "先手";
            }
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
        whiteColor = new Color(1, 1, 1, Opacity / 255);
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
    // 駒の出現位置を格納するためのメソッド
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
    // 升目を透明にする
    public void MakeBoardSquareTransparent(int x, int y)
    {
        BoardSquare[y, x].GetComponent<Image>().color = defaultColor;
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
    public void MakeBoardSquareWhite(int x, int y)
    {
        BoardSquare[y, x].GetComponent<Image>().color = whiteColor;
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
                    GameObject piecePrefab;
                    if (myFormationBoard[i, j].King)
                    {
                        piecePrefab = MyPiecePrefabKing;
                    }
                    else
                    {
                        piecePrefab = MyPiecePrefab;
                    }
                    GameBoard[i + 3, j] = (GameObject)Instantiate(piecePrefab, BoardSquarPosition[i + 3, j], Quaternion.identity, Canvas.transform);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitActionRange(myFormationBoard[i, j].UpperLeft, myFormationBoard[i, j].LowerLeft, myFormationBoard[i, j].UpperRight, myFormationBoard[i, j].LowerRight, myFormationBoard[i, j].Left, myFormationBoard[i, j].Right, myFormationBoard[i, j].Forward, myFormationBoard[i, j].Backward);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitPosition(j, i + 3);
                    GameBoard[i + 3, j].GetComponent<Piece>().Opponent = false;
                    GameBoard[i + 3, j].GetComponent<Piece>().StrategyPoint = StrategyPointSetting.CalcuratePieceStrategyPoint(GameBoard[i + 3, j].GetComponent<Piece>());
                    GameBoard[i + 3, j].GetComponent<Piece>().ToInspector();
                    GameBoard[i + 3, j].GetComponent<Piece>().King = myFormationBoard[i, j].King;
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
                    GameObject piecePrefab;
                    if (opponentFormationBoard[i, j].King)
                    {
                        piecePrefab = OpponentPiecePrefabKing;
                    }
                    else
                    {
                        piecePrefab = OpponentPiecePrefab;
                    }
                    GameBoard[y, x] = (GameObject)Instantiate(piecePrefab, BoardSquarPosition[y, x], Quaternion.identity, Canvas.transform);
                    GameBoard[y, x].GetComponent<Piece>().InitActionRange(upperLeft, lowerLeft, upperRight, lowerRight, left, right, forward, backward);
                    GameBoard[y, x].GetComponent<Piece>().InitPosition(x, y);
                    GameBoard[y, x].GetComponent<Piece>().Opponent = true;
                    GameBoard[y, x].GetComponent<Piece>().StrategyPoint = StrategyPointSetting.CalcuratePieceStrategyPoint(GameBoard[y, x].GetComponent<Piece>());
                    GameBoard[y, x].GetComponent<Piece>().ToInspector();
                    GameBoard[y, x].GetComponent<Piece>().King = opponentFormationBoard[i, j].King;
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
    // 駒の移動可能範囲をハイライトする:Move機能時に使用
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
                    if (GameBoard[i, x].GetComponent<Piece>().Opponent != opponent)
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
                    if (GameBoard[i, x].GetComponent<Piece>().Opponent != opponent)
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
                    if (GameBoard[y, i].GetComponent<Piece>().Opponent != opponent)
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
                    if (GameBoard[y, i].GetComponent<Piece>().Opponent != opponent)
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
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent != opponent)
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
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent != opponent)
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
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent != opponent)
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
                    if (GameBoard[j, i].GetComponent<Piece>().Opponent != opponent)
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
                GameBoard[y, x].SetActive(false);
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
            if (myTurn)
            {
                myActionData[actionNumber].Function = Functions.Move;
                myActionData[actionNumber].MoveData = new MoveData(movingPositionX, movingPositionY, x, y);
                myActionData[actionNumber].Opponent = false;
            }
            else
            {
                opponentActionData[actionNumber].Function = Functions.Move;
                opponentActionData[actionNumber].MoveData = new MoveData(movingPositionX, movingPositionY, x, y);
                opponentActionData[actionNumber].Opponent = true;
            }
            ActionNumberPlus();
        }
        if (Function == Functions.Create)
        {
            if (GameBoard[y, x] == null || GameBoard[y, x].GetComponent<Piece>().Opponent)
            {
                // 生成してCanvasの子要素に設定
                createPalette.SetActive(true);
                createPalette.GetComponent<CreatePalette>().Init();
                boardSquareClickController.GetComponent<BoardSquareClickController>().PaletteCheck = true; // Paletteが１枚しか生成されないようにする
                createPalette.GetComponent<CreatePalette>().FixDialog = result => CreatePaletteButtonAction(result);
                createPalette.GetComponent<CreatePalette>().SetPosition(x, y);
                createPalette.GetComponent<CreatePalette>().SetStrategyPoint(MySP);
            }
        }
        if (Function == Functions.Evolve)
        {
            if (GameBoard[y, x] != null && !GameBoard[y, x].GetComponent<Piece>().Opponent && !GameBoard[y, x].GetComponent<Piece>().Evolved)
            {
                evolvePalette.SetActive(true);
                evolvePalette.GetComponent<EvolvePalette>().Init();
                evolvePalette.GetComponent<EvolvePalette>().SetPosition(x, y);
                evolvePalette.GetComponent<EvolvePalette>().SetActionRange(GameBoard[y, x].GetComponent<Piece>());
                boardSquareClickController.GetComponent<BoardSquareClickController>().PaletteCheck = true; // Paletteが１枚しか生成されないようにする
                evolvePalette.GetComponent<EvolvePalette>().FixDialog = result => EvolvePaletteButtonAction(result);
                evolvePalette.GetComponent<EvolvePalette>().SetStrategyPoint(MySP);

            }
        }
    }
    public void Skip()
    {
        if (myTurn)
        {
            myActionData[actionNumber].Function = Functions.Skip;
        }
        else
        {
            opponentActionData[actionNumber].Function = Functions.Skip;
        }
        ActionNumberPlus();
    }
    // actionNumber++にまつわる処理を関数化した
    private void ActionNumberPlus()
    {
        actionNumber++;
        ResetButton.interactable = true;
        if (actionNumber == ActionMax)
        {
            EnterButton.interactable = true;
            StopMoving = true;
        }
    }
    private void EvolvePaletteButtonAction(EvolvePalette.EvolvePaletteResult result)
    {
        boardSquareClickController.GetComponent<BoardSquareClickController>().PaletteCheck = false;
        MakeAllBoardSquarTransparent();
        Debug.Log(result);
        if (result == EvolvePalette.EvolvePaletteResult.OK)
        {
            EvolveData evolve = evolvePalette.GetComponent<EvolvePalette>().GetEvolveData();
            GameBoard[evolve.PositionY, evolve.PositionX].GetComponent<Piece>().SetEvolveData(evolve);
            CopyGameBoard(GameBoard, ActionGameBoard[actionNumber]);
            if (myTurn)
            {
                myActionData[actionNumber].Function = Functions.Evolve;
                myActionData[actionNumber].EvolveData = evolve;
                myActionData[actionNumber].Opponent = false;
            }
            else
            {
                opponentActionData[actionNumber].Function = Functions.Evolve;
                opponentActionData[actionNumber].EvolveData = evolve;
                opponentActionData[actionNumber].Opponent = true;
            }

            ActionNumberPlus();
        }
    }
    private void CreatePaletteButtonAction(CreatePalette.CreatePaletteResult result)
    {
        boardSquareClickController.GetComponent<BoardSquareClickController>().PaletteCheck = false;
        MakeAllBoardSquarTransparent();
        Debug.Log(result);
        if (result == CreatePalette.CreatePaletteResult.OK)
        {
            GameObject Prefab;
            if (myTurn)
            {
                Prefab = MyPiecePrefab;
            }
            else
            {
                Prefab = OpponentPiecePrefab;
            }
            CreateData createData = createPalette.GetComponent<CreatePalette>().GetCreateData();
            GameBoard[createData.PositionY, createData.PositionX] = Instantiate(Prefab, BoardSquarPosition[createData.PositionY, createData.PositionX], Quaternion.identity, Canvas.transform);
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().InitActionRange(createData.UpperLeft, createData.LowerLeft, createData.UpperRight, createData.LowerRight, createData.Left, createData.Right, createData.Forward, createData.Backward);
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().InitPosition(createData.PositionX, createData.PositionY);
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().Opponent = false;
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().StrategyPoint = StrategyPointSetting.CalcuratePieceStrategyPoint(GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>());
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().ToInspector();
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().StoppingAction = true;
            CopyGameBoard(GameBoard, ActionGameBoard[actionNumber]);
            if (myTurn)
            {
                myActionData[actionNumber].Function = Functions.Create;
                myActionData[actionNumber].CreateData = createData;
                myActionData[actionNumber].Opponent = false;
            }
            else
            {
                opponentActionData[actionNumber].Function = Functions.Create;
                opponentActionData[actionNumber].CreateData = createData;
                opponentActionData[actionNumber].Opponent = true;
            }
            ActionNumberPlus();
        }
    }

    // 駒が敵陣地に侵入した際の処理
    private void InvadeOpponentFormation(int y, int positionX, int positionY)
    {
        if (!GameBoard[positionY, positionX].GetComponent<Piece>().Opponent)
        {
            if (y < 2)
            {
                MySP += StrategyPointSetting.CalcurateInvasionPoint;
                GameBoard[positionY, positionX].GetComponent<Piece>().Invasion = true;
            }
        }
        else
        {
            if (y > 2)
            {
                OpponentSP += StrategyPointSetting.CalcurateInvasionPoint;
                GameBoard[positionY, positionX].GetComponent<Piece>().Invasion = true;
            }
        }
    }
    // 敵の駒を取った時の処理
    private void BreakPiece(int x, int y)
    {
        Piece piece = GameBoard[y, x].GetComponent<Piece>();
        if (piece.Opponent)
        {
            MySP += StrategyPointSetting.CalcurateBreakingPiecePoints(piece);
            if (piece.King)
            {
                FinishBattle.SetActive(true);
                FinishBattle.GetComponent<FinishBattle>().Winner();
            }
        }
        else
        {
            OpponentSP += StrategyPointSetting.CalcurateBreakingPiecePoints(piece);
            if (piece.King)
            {
                FinishBattle.SetActive(true);
                FinishBattle.GetComponent<FinishBattle>().Loser();
            }
        }

        Destroy(GameBoard[y, x]);
    }
    // 自身の手を登録するターンと相手の手を登録するターンを切り替えるためのメソッド
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
        myTurn = !myTurn;
    }
    // 登録した手を巻き戻すためのメソッド
    public void ResetGameBoard()
    {
        EnterButton.interactable = false;
        actionNumber--;
        ActionData actionData;
        if (myTurn)
        {
            actionData = myActionData[actionNumber];
        }
        else
        {
            actionData = opponentActionData[actionNumber];
        }
        if (actionNumber > 0)
        {

            ResetCreatePiece(ActionGameBoard[actionNumber - 1], ActionGameBoard[actionNumber]);
            if (actionData.Function == Functions.Evolve)
            {
                ResetEvolvePiece(ActionGameBoard[actionNumber], actionData.EvolveData);
            }
            ResetPiecePosition(ActionGameBoard[actionNumber - 1]);
            CopyGameBoard(ActionGameBoard[actionNumber - 1], GameBoard);
        }
        else if (actionNumber == 0)
        {

            ResetCreatePiece(GameBoardBuffer, ActionGameBoard[actionNumber]);
            if (actionData.Function == Functions.Evolve)
            {
                ResetEvolvePiece(ActionGameBoard[actionNumber], actionData.EvolveData);
            }
            ResetPiecePosition(GameBoardBuffer);
            CopyGameBoard(GameBoardBuffer, GameBoard);
            ResetButton.interactable = false;
        }
        StopMoving = false;
    }
    // 盤面にある手を入力された盤面の位置に戻す
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
    private void ResetCreatePiece(GameObject[,] before, GameObject[,] after)
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (before[i, j] == null && after[i, j] != null)
                {
                    if (after[i, j].GetComponent<Piece>().StoppingAction && !after[i, j].GetComponent<Piece>().Evolved)
                    {
                        Destroy(after[i, j]);
                    }
                }
            }
        }
    }
    private void ResetAllEvolvePieces()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (GameBoard[i, j] != null)
                {
                    if (GameBoard[i, j].GetComponent<Piece>().Evolved && GameBoard[i, j].GetComponent<Piece>().StoppingAction)
                    {
                        GameBoard[i, j].GetComponent<Piece>().ResetEvolveData();
                    }
                }
            }
        }
    }
    private void ResetEvolvePiece(GameObject[,] gameObject, EvolveData evolveData)
    {
        gameObject[evolveData.PositionY, evolveData.PositionX].GetComponent<Piece>().ResetEvolveData();
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
    // 確定ボタンが押されたときの処理
    public void OnEnterButtonClicked()
    {
        ResetButton.interactable = false;
        ResetAllEvolvePieces();
        ResetCreatePiece(GameBoardBuffer, GameBoard);
        ResetPiecePosition(GameBoardBuffer);
        CopyGameBoard(GameBoardBuffer, GameBoard);
        if (!myTurn)
        {
            MakeTotalActionData();
            NextButton.interactable = true;
            EnterButton.interactable = false;
            StopMoving = true;
            FirstMove = !FirstMove;
        }
        else
        {
            StopMoving = false;
        }
        totalActionDataIndex = 0;
        actionNumber = 0;
        ChangeOpponentFlag();
    }
    private void UnlockStoppingAction()
    {
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                if (GameBoard[i, j] != null && GameBoard[i, j].GetComponent<Piece>().StoppingAction)
                {
                    GameBoard[i, j].GetComponent<Piece>().StoppingAction = false;
                }

            }
        }
    }

    // 公開していく順番に自分と味方の手を登録していく
    private void MakeTotalActionData()
    {
        for (int i = 0, j = 0; i < ActionMax; i++)
        {
            if (FirstMove)
            {
                totalActionData[j] = myActionData[i];
                j++;
                totalActionData[j] = opponentActionData[i];
                j++;
            }
            else
            {
                totalActionData[j] = opponentActionData[i];
                j++;
                totalActionData[j] = myActionData[i];
                j++;
            }

        }
    }
    // 全体の手を順に公開していく
    public void ReflectAction()
    {
        switch (totalActionData[totalActionDataIndex].Function)
        {
            case Functions.Move:
                ReflectMoveData(totalActionData[totalActionDataIndex]);
                totalActionDataIndex++;
                break;
            case Functions.Create:
                ReflectCreateData(totalActionData[totalActionDataIndex]);
                totalActionDataIndex++;
                break;
            case Functions.Evolve:
                ReflectEvolveData(totalActionData[totalActionDataIndex]);
                totalActionDataIndex++;
                break;
            case Functions.Skip:
                totalActionDataIndex++;
                break;
        }
        if (totalActionDataIndex == ActionMax * 2)
        {
            ResetButton.interactable = false;
            NextButton.interactable = false;
            EnterButton.interactable = false;
            CopyGameBoard(GameBoard, GameBoardBuffer);
            StopMoving = false;
            UnlockStoppingAction();
        }
    }
    // 登録された手が移動だった時の処理
    private void ReflectMoveData(ActionData actionData)
    {
        MoveData moveData = actionData.MoveData;
        if (GameBoard[moveData.PositionY, moveData.PositionX] != null)
        {
            if (GameBoard[moveData.PositionY, moveData.PositionX].GetComponent<Piece>().Opponent == actionData.Opponent)
            {
                if (GameBoard[moveData.ToY, moveData.ToX] != null)
                {
                    BreakPiece(moveData.ToX, moveData.ToY);
                }
                if (!GameBoard[moveData.PositionY, moveData.PositionX].GetComponent<Piece>().Invasion)
                {
                    InvadeOpponentFormation(moveData.ToY, moveData.PositionX, moveData.PositionY);
                }
                GameBoard[moveData.ToY, moveData.ToX] = GameBoard[moveData.PositionY, moveData.PositionX];
                GameBoard[moveData.ToY, moveData.ToX].GetComponent<Piece>().InitPosition(moveData.ToX, moveData.ToY);
                GameBoard[moveData.ToY, moveData.ToX].transform.position = BoardSquarPosition[moveData.ToY, moveData.ToX];
                GameBoard[moveData.PositionY, moveData.PositionX] = null;
            }
        }
    }
    // 登録された手が生成だった時の処理
    private void ReflectCreateData(ActionData actionData)
    {
        CreateData createData = actionData.CreateData;
        if (GameBoard[createData.PositionY, createData.PositionX] == null)
        {
            GameObject Prefab;
            if (actionData.Opponent)
            {
                Prefab = OpponentPiecePrefab;
                OpponentSP -= StrategyPointSetting.CalcurateCreatingPoint(createData.UpperLeft, createData.UpperRight, createData.LowerLeft, createData.LowerRight, createData.Right, createData.Left, createData.Forward, createData.Backward);
            }
            else
            {
                Prefab = MyPiecePrefab;
                MySP -= StrategyPointSetting.CalcurateCreatingPoint(createData.UpperLeft, createData.UpperRight, createData.LowerLeft, createData.LowerRight, createData.Right, createData.Left, createData.Forward, createData.Backward);
            }
            GameBoard[createData.PositionY, createData.PositionX] = Instantiate(Prefab, BoardSquarPosition[createData.PositionY, createData.PositionX], Quaternion.identity, Canvas.transform);
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().InitActionRange(createData.UpperLeft, createData.LowerLeft, createData.UpperRight, createData.LowerRight, createData.Left, createData.Right, createData.Forward, createData.Backward);
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().InitPosition(createData.PositionX, createData.PositionY);
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().Opponent = actionData.Opponent;
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().StrategyPoint = StrategyPointSetting.CalcuratePieceStrategyPoint(GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>());
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().ToInspector();
            GameBoard[createData.PositionY, createData.PositionX].GetComponent<Piece>().StoppingAction = true;
        }
    }
    private void ReflectEvolveData(ActionData actionData)
    {
        EvolveData evolveData = actionData.EvolveData;
        if (GameBoard[evolveData.PositionY, evolveData.PositionX] != null)
        {
            if (GameBoard[evolveData.PositionY, evolveData.PositionX].GetComponent<Piece>().Opponent == actionData.Opponent)
            {
                GameBoard[evolveData.PositionY, evolveData.PositionX].GetComponent<Piece>().SetEvolveData(evolveData);
                if (actionData.Opponent)
                {
                    OpponentSP -= StrategyPointSetting.CalcurateEvolvingPoint(evolveData.EvolveUpperLeft, evolveData.EvolveUpperRight, evolveData.EvolveLowerLeft, evolveData.EvolveLowerRight, evolveData.EvolveRight, evolveData.EvolveLeft, evolveData.EvolveForward, evolveData.EvolveBackward);
                }
                else
                {
                    MySP -= StrategyPointSetting.CalcurateEvolvingPoint(evolveData.EvolveUpperLeft, evolveData.EvolveUpperRight, evolveData.EvolveLowerLeft, evolveData.EvolveLowerRight, evolveData.EvolveRight, evolveData.EvolveLeft, evolveData.EvolveForward, evolveData.EvolveBackward);
                }
            }
        }
    }
}

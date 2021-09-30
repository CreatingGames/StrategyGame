using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateFormationController : MonoBehaviour
{
    [SerializeField] string Faile;
    [SerializeField] CreateFormationPalette create;
    [System.Serializable]
    public struct FormationDatas
    {
        public int StrategyPoint;
        [System.Serializable]
        public struct Formation
        {
            public int x;
            public int y;
            public int UL;
            public int UR;
            public int LL;
            public int LR;
            public int L;
            public int R;
            public int F;
            public int B;
            public bool King;
        }
        public Formation[] Formations;
        public void print()
        {
            foreach (var item in Formations)
            {
                Debug.Log("[x:" + item.x +
                          "][y:" + item.y + "]");
            }
        }
    }
    private string _dataPath;
    private GameObject[,] BoardSquare;
    [SerializeField] GameObject Board;
    private Vector3[,] BoardSquarPosition;
    private Vector3 piecePositionZ = new Vector3(0.0f, 0.0f, -0.46296296296f);// 生成されるオブジェクト位置をz軸-50にするためにメタ的にこうしてる。
    private GameObject[,] GameBoard;
    [SerializeField] GameObject MyPiecePrefab;
    [SerializeField] GameObject MyPiecePrefabKing;
    [SerializeField] GameObject cavas;

    private void Start()
    {
        _dataPath = Path.Combine(Application.persistentDataPath, Faile);
        GameBoard = new GameObject[2, 5];
        GetAllBoardSquare();
        GetAllBoardSquarePosition();
        OnLoad();
    }
    private void GetAllBoardSquare()
    {
        BoardSquare = new GameObject[2, 5];
        int index = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BoardSquare[i, j] = Board.transform.GetChild(index).gameObject;
                index++;
            }
        }
    }
    private void GetAllBoardSquarePosition()
    {
        BoardSquarPosition = new Vector3[2, 5];
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                BoardSquarPosition[i, j] = BoardSquare[i, j].transform.position + piecePositionZ;
            }
        }
    }
    public void OnSave(int SP)
    {
        int num = 0;
        var obj = new FormationDatas
        {
            StrategyPoint = SP
        };
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (GameBoard[i, j] != null)
                {
                    num++;
                }
            }
        }
        obj.Formations = new FormationDatas.Formation[num];
        int index = 0;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (GameBoard[i, j] != null)
                {
                    FormationPiece formationPiece = GameBoard[i, j].GetComponent<FormationPiece>();
                    obj.Formations[index].x = formationPiece.PositionX;
                    obj.Formations[index].y = formationPiece.PositionY;
                    obj.Formations[index].UL = formationPiece.UpperLeft;
                    obj.Formations[index].UR = formationPiece.UpperRight;
                    obj.Formations[index].LL = formationPiece.LowerLeft;
                    obj.Formations[index].LR = formationPiece.LowerRight;
                    obj.Formations[index].L = formationPiece.Left;
                    obj.Formations[index].R = formationPiece.Right;
                    obj.Formations[index].F = formationPiece.Forward;
                    obj.Formations[index].B = formationPiece.Backward;
                    obj.Formations[index].King = formationPiece.King;
                    index++;
                }
            }
        }

        // JSON形式にシリアライズ
        var json = JsonUtility.ToJson(obj, false);

        // JSONデータをファイルに保存
        File.WriteAllText(_dataPath, json);
    }
    // JSON形式をロードしてデシリアライズ
    private void OnLoad()
    {
        // 念のためファイルの存在チェック
        if (!File.Exists(_dataPath))
        {
            create.SetSP(0);
            return;
        }

        // JSONデータとしてデータを読み込む
        var json = File.ReadAllText(_dataPath);

        // JSON形式からオブジェクトにデシリアライズ
        var obj = JsonUtility.FromJson<FormationDatas>(json);

        int pieces = obj.Formations.Length;

        for (int i = 0; i < pieces; i++)
        {
            GameObject piecePrefab;
            if (obj.Formations[i].King)
            {
                piecePrefab = MyPiecePrefabKing;
            }
            else
            {
                piecePrefab = MyPiecePrefab;
            }
            int x = obj.Formations[i].x;
            int y = obj.Formations[i].y;
            GameBoard[y, x] = (GameObject)Instantiate(piecePrefab, BoardSquarPosition[y, x], Quaternion.identity, cavas.transform);
            FormationPiece piece = GameBoard[y, x].GetComponent<FormationPiece>();
            piece.InitActionRange(obj.Formations[i].UL, obj.Formations[i].LL, obj.Formations[i].UR, obj.Formations[i].LR, obj.Formations[i].L, obj.Formations[i].R, obj.Formations[i].F, obj.Formations[i].B);
            piece.InitPosition(x, y);
            piece.Opponent = false;
            piece.StrategyPoint = StrategyPointSetting.CalcurateFormationPieceStrategyPoint(piece);
            piece.ToInspector();
            piece.King = obj.Formations[i].King;
            if (obj.Formations[i].King)
            {
                create.king = true;
            }
        }
        create.SetSP(obj.StrategyPoint);
    }
    public int  CreatePiece(int x, int y, int UR, int UL, int LR, int LL, int F, int B, int R, int L, bool king)
    {
        GameObject piecePrefab;
        if (king)
        {
            piecePrefab = MyPiecePrefabKing;
        }
        else
        {
            piecePrefab = MyPiecePrefab;
        }
        GameBoard[y, x] = (GameObject)Instantiate(piecePrefab, BoardSquarPosition[y, x], Quaternion.identity, cavas.transform);
        FormationPiece piece = GameBoard[y, x].GetComponent<FormationPiece>();
        piece.InitActionRange(UL, LL, UR, LR, L, R, F, B);
        piece.InitPosition(x, y);
        piece.Opponent = false;
        piece.StrategyPoint = StrategyPointSetting.CalcurateFormationPieceStrategyPoint(piece);
        piece.ToInspector();
        piece.King = king;
        return piece.StrategyPoint;
    }
    public void DeletePiece(int x,int y)
    {
        if(GameBoard[y, x].GetComponent<FormationPiece>().King)
        {
            create.king = false;
        }
        Destroy(GameBoard[y, x]);
        GameBoard[y, x] = null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : PieceData
{
    // インスペクターに表示する用
    [SerializeField] int t_UpperLeft = 0;
    [SerializeField] int t_LowerLeft = 0;
    [SerializeField] int t_UpperRight = 0;
    [SerializeField] int t_LowerRight = 0;
    [SerializeField] int t_Left = 0;
    [SerializeField] int t_Right = 0;
    [SerializeField] int t_Forward = 0;
    [SerializeField] int t_Backward = 0;
    [SerializeField] int t_X = 0;
    [SerializeField] int t_Y = 0;

    // 駒の状態
    public bool Evolved { get; set; } = false;
    public bool Opponent { get; set; } = false;

    BattleSceneController battleSceneController;
    public bool readyMove = false;
    private bool[,] OnBoardActionRange = new bool[5, 5];
    private void Start()
    {
        battleSceneController = GameObject.Find("BattleSceneController").GetComponent<BattleSceneController>();
    }
    private void Update()
    {
        if (readyMove)
        {
            battleSceneController.HighlightActionRange(PositionX, PositionY);
        }
    }
    // 行動範囲の初期化
    public void InitActionRange(int UpperLeft, int LowerLeft, int UpperRight, int LowerRight, int Left, int Right, int Forward, int Backward)
    {
        this.UpperLeft = UpperLeft;
        this.LowerLeft = LowerLeft;
        this.UpperRight = UpperRight;
        this.LowerRight = LowerRight;
        this.Left = Left;
        this.Right = Right;
        this.Forward = Forward;
        this.Backward = Backward;
        ToInspector();
        SumActionRange = GetSumActionRange();
    }
    public void InitPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
        ToInspector();
        SumActionRange = GetSumActionRange();
    }

    // 駒が保有する行動範囲の合計を返す。
    public int GetSumActionRange()
    {
        int sum = UpperLeft + LowerLeft + UpperRight + LowerRight + Left + Right + Forward + Backward;
        return sum;
    }
    // インスペクター用変数に代入していく
    private void ToInspector()
    {
        t_UpperLeft = UpperLeft;
        t_LowerLeft = LowerLeft;
        t_UpperRight = UpperRight;
        t_LowerRight = LowerRight;
        t_Left = Left;
        t_Right = Right;
        t_Forward = Forward;
        t_Backward = Backward;
        t_X = PositionX;
        t_Y = PositionY;
    }
    void OnMouseEnter()
    {
        battleSceneController.HighlightActionRange(PositionX, PositionY);
    }

    void OnMouseExit()
    {
        battleSceneController.MakeAllBoardSquarTransparent();
    }
    public void OnClicked()
    {
        if (!Opponent)
        {
            switch (battleSceneController.Function)
            {
                case Functions.Move:
                    if (!battleSceneController.movingPieceSelected)
                    {
                        Debug.Log("Move");
                        readyMove = true;
                        battleSceneController.SetActionRange(PositionX, PositionY);
                    }
                    else
                    {
                        battleSceneController.RestMovingPieceSelected();
                    }
                    break;
                case Functions.Create:
                    Debug.Log("Create");
                    break;
                case Functions.Evolve:
                    Debug.Log("Evolve");
                    break;
            }
        }
    }
}

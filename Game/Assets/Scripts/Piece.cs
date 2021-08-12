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
    public bool Opponent { get; set; } = true;

    BattleSceneController BattleSceneController;

    private void Start()
    {
        BattleSceneController = GameObject.Find("BattleSceneController").GetComponent<BattleSceneController>();
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
        if (Forward != 0)
        {
            int minY;
            if (PositionY - Forward > 0)
            {
                minY = PositionY - Forward;
            }
            else
            {
                minY = 0;
            }
            for (int i = PositionY - 1; i >= minY; i--)
            {
                BattleSceneController.MakeBoardSquarOpaque(PositionX, i, Opponent);
            }
        }
        if (Backward != 0)
        {
            int maxY;
            if (PositionY + Backward < BattleSceneController.BoardSize)
            {
                maxY = PositionY + Backward;
            }
            else
            {
                maxY = BattleSceneController.BoardSize - 1;
            }
            for (int i = PositionY + 1; i <= maxY; i++)
            {
                BattleSceneController.MakeBoardSquarOpaque(PositionX, i, Opponent);
            }
        }
        if (Left != 0)
        {
            int minX;
            if (PositionX - Left > 0)
            {
                minX = PositionX - Left;
            }
            else
            {
                minX = 0;
            }
            for (int i = PositionX - 1; i >= minX; i--)
            {
                BattleSceneController.MakeBoardSquarOpaque(i, PositionY, Opponent);
            }
        }
        if (Right != 0)
        {
            int maxX;
            if (PositionX + Right < BattleSceneController.BoardSize)
            {
                maxX = PositionX + Right;
            }
            else
            {
                maxX = BattleSceneController.BoardSize - 1;
            }
            for (int i = PositionX + 1; i <= maxX; i++)
            {
                BattleSceneController.MakeBoardSquarOpaque(i, PositionY, Opponent);
            }
        }
        if (UpperLeft != 0)
        {
            int minX;
            if (PositionX - Left > 0)
            {
                minX = PositionX - Left;
            }
            else
            {
                minX = 0;
            }
            int minY;
            if (PositionY - Forward > 0)
            {
                minY = PositionY - Forward;
            }
            else
            {
                minY = 0;
            }
            for (int i = PositionX - 1, j = PositionY - 1; i >= minX && j >= minY; i--, j--)
            {
                BattleSceneController.MakeBoardSquarOpaque(i, j, Opponent);
            }
        }
        if (UpperRight != 0)
        {
            int maxX;
            if (PositionX + Right < BattleSceneController.BoardSize)
            {
                maxX = PositionX + Right;
            }
            else
            {
                maxX = BattleSceneController.BoardSize - 1;
            }
            int minY;
            if (PositionY - Forward > 0)
            {
                minY = PositionY - Forward;
            }
            else
            {
                minY = 0;
            }
            for (int i = PositionX + 1, j = PositionY - 1; i <= maxX && j >= minY; i++, j--)
            {
                BattleSceneController.MakeBoardSquarOpaque(i, j, Opponent);
            }
        }
        if (LowerLeft != 0)
        {
            int minX;
            if (PositionX - Left > 0)
            {
                minX = PositionX - Left;
            }
            else
            {
                minX = 0;
            }
            int maxY;
            if (PositionY + Backward < BattleSceneController.BoardSize)
            {
                maxY = PositionY + Backward;
            }
            else
            {
                maxY = BattleSceneController.BoardSize - 1;
            }
            for (int i = PositionX - 1, j = PositionY + 1; i >= minX && j <= maxY; i--, j++)
            {
                BattleSceneController.MakeBoardSquarOpaque(i, j, Opponent);
            }
        }
        if (LowerRight != 0)
        {
            int maxX;
            if (PositionX + Right < BattleSceneController.BoardSize)
            {
                maxX = PositionX + Right;
            }
            else
            {
                maxX = BattleSceneController.BoardSize - 1;
            }
            int maxY;
            if (PositionY + Backward < BattleSceneController.BoardSize)
            {
                maxY = PositionY + Backward;
            }
            else
            {
                maxY = BattleSceneController.BoardSize - 1;
            }
            for (int i = PositionX + 1, j = PositionY + 1; i <= maxX && j <= maxY; i--, j++)
            {
                BattleSceneController.MakeBoardSquarOpaque(i, j, Opponent);
            }
        }
    }
    void OnMouseExit()
    {
        BattleSceneController.MakeAllBoardSquarTransparent();
    }
}

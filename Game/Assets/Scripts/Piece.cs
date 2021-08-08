using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Test用データ入力
    [SerializeField] int t_UpperLeft = 0;
    [SerializeField] int t_LowerLeft = 0;
    [SerializeField] int t_UpperRight = 0;
    [SerializeField] int t_LowerRight = 0;
    [SerializeField] int t_Left = 0;
    [SerializeField] int t_Right = 0;
    [SerializeField] int t_Forward = 0;
    [SerializeField] int t_Backward = 0;

    // 駒の行動範囲
    public int UpperLeft { get; set; } = 0;// 左斜め前
    public int LowerLeft { get; set; } = 0;// 左斜め後ろ
    public int UpperRight { get; set; } = 0;// 右斜め前
    public int LowerRight { get; set; } = 0;// 右斜め後ろ
    public int Left { get; set; } = 0;// 左
    public int Right { get; set; } = 0;// 右
    public int Forward { get; set; } = 0;// 前
    public int Backward { get; set; } = 0;// 後ろ

    // 駒の状態
    public bool evolved { get; set; } = false;
    private void Start()
    {
        InitActionRange(t_UpperLeft, t_LowerLeft, t_UpperRight, t_LowerRight, t_Left, t_Right, t_Forward, t_Backward);
    }

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
    }

    // 駒が保有する戦略ポイントを返す。
    public int GetStrategyPoint()
    {
        int sum = UpperLeft + LowerLeft + UpperRight + LowerRight + Left + Right + Forward + Backward;
        return sum;
    }
}

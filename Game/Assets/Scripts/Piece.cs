using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : PieceData
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



    // 駒の状態
    public bool evolved { get; set; } = false;
    private void Start()
    {
        // 今はこんな感じで使ってるけど、ホントは陣形をロードするときに呼び出すようにしたい
        InitActionRange(t_UpperLeft, t_LowerLeft, t_UpperRight, t_LowerRight, t_Left, t_Right, t_Forward, t_Backward);
        SumActionRange = GetSumActionRange();
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
    }
    public void InitPosition(int x, int y)
    {
        PositionX = x;
        PositionY = y;
    }

    // 駒が保有する行動範囲の合計を返す。
    public int GetSumActionRange()
    {
        int sum = UpperLeft + LowerLeft + UpperRight + LowerRight + Left + Right + Forward + Backward;
        return sum;
    }
}

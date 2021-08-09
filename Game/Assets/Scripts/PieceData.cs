using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData : MonoBehaviour
{
    // 駒の行動範囲
    public int UpperLeft { get; set; } = 0;// 左斜め前
    public int LowerLeft { get; set; } = 0;// 左斜め後ろ
    public int UpperRight { get; set; } = 0;// 右斜め前
    public int LowerRight { get; set; } = 0;// 右斜め後ろ
    public int Left { get; set; } = 0;// 左
    public int Right { get; set; } = 0;// 右
    public int Forward { get; set; } = 0;// 前
    public int Backward { get; set; } = 0;// 後ろ
    public int PositionX { get; set; } = 0;
    public int PositionY { get; set; } = 0;
}

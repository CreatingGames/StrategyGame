﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyPointSetting : MonoBehaviour
{
    public static int InitialStrategyPoint { get; } = 100;

    public static int CalcuratePieaceStrategyPoint(Piece piece)
    {
        if (piece is null)
        {
            throw new ArgumentNullException(nameof(piece));
        }

        // ここで、戦略ポイントを設定するアルゴリズムを作る 今は行動範囲の合計に行動の方向を足したもの
        int sum = piece.UpperLeft + piece.LowerLeft + piece.UpperRight + piece.LowerRight + piece.Left + piece.Right + piece.Forward + piece.Backward;
        int StrategyPoint = sum;
        if(piece.Forward != 0)
        {
            StrategyPoint++;
        }
        if (piece.Backward != 0)
        {
            StrategyPoint++;
        }
        if (piece.Left != 0)
        {
            StrategyPoint++;
        }
        if (piece.Right != 0)
        {
            StrategyPoint++;
        }
        if (piece.UpperLeft != 0)
        {
            StrategyPoint++;
        }
        if (piece.UpperRight != 0)
        {
            StrategyPoint++;
        }
        if (piece.LowerLeft != 0)
        {
            StrategyPoint++;
        }
        if (piece.LowerRight != 0)
        {
            StrategyPoint++;
        }
        return StrategyPoint;
    }

}

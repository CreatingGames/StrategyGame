using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyPointSetting : MonoBehaviour
{
    public static int InitialStrategyPoint { get; } = 100;

    public static int CalcuratePieceStrategyPoint(Piece piece)
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
    // 陣形生成時の駒生成の際の戦略ポイント
    public static int CalcurateFormationPieceStrategyPoint(FormationPiece piece)
    {
        if (piece is null)
        {
            throw new ArgumentNullException(nameof(piece));
        }

        // ここで、戦略ポイントを設定するアルゴリズムを作る 今は行動範囲の合計に行動の方向を足したもの
        int sum = piece.UpperLeft + piece.LowerLeft + piece.UpperRight + piece.LowerRight + piece.Left + piece.Right + piece.Forward + piece.Backward;
        int StrategyPoint = sum;
        if (piece.Forward != 0)
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
    public static int CalcurateBreakingPiecePoints(Piece piece)
    {
        if (piece is null)
        {
            throw new ArgumentNullException(nameof(piece));
        }
        // 今は相手の戦略ポイントをそのまま出してる
        return piece.StrategyPoint;
    }
    public static int CalcurateInvasionPoint { get; } = 3; // 駒が敵陣に侵入したときに得られるポイント

    public static int CalcurateCreatingPoint(int UL, int UR, int LL, int LR, int R, int L, int F, int B)
    {
        int sum = UL + UR + LL + LR + B + F + L + R;
        int StrategyPoint = sum;
        if (F != 0)
        {
            StrategyPoint++;
        }
        if (B != 0)
        {
            StrategyPoint++;
        }
        if (L != 0)
        {
            StrategyPoint++;
        }
        if (R != 0)
        {
            StrategyPoint++;
        }
        if (UL != 0)
        {
            StrategyPoint++;
        }
        if (UR != 0)
        {
            StrategyPoint++;
        }
        if (LL != 0)
        {
            StrategyPoint++;
        }
        if (LR != 0)
        {
            StrategyPoint++;
        }
        return StrategyPoint;
    }
    public static int CalcurateEvolvingPoint(int UL, int UR, int LL, int LR, int R, int L, int F, int B)
    {
        // 引数は全部進化分の値
        int sum = UL + UR + LL + LR + B + F + L + R;
        int StrategyPoint = sum;
        if (F != 0)
        {
            StrategyPoint++;
        }
        if (B != 0)
        {
            StrategyPoint++;
        }
        if (L != 0)
        {
            StrategyPoint++;
        }
        if (R != 0)
        {
            StrategyPoint++;
        }
        if (UL != 0)
        {
            StrategyPoint++;
        }
        if (UR != 0)
        {
            StrategyPoint++;
        }
        if (LL != 0)
        {
            StrategyPoint++;
        }
        if (LR != 0)
        {
            StrategyPoint++;
        }
        return StrategyPoint;
    }
}

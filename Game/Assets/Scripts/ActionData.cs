using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionData : MonoBehaviour
{
    public Functions Function;
    public int PositionX;// 駒の盤面上の座標
    public int PositionY;// 駒の盤面上の座標
    // Move機能で必要な奴
    public int ToX;
    public int ToY;
    // Create機能に必要な奴　駒の行動範囲
    public int UpperLeft;// 左斜め前
    public int LowerLeft;// 左斜め後ろ
    public int UpperRight;// 右斜め前
    public int LowerRight;// 右斜め後ろ
    public int Left;// 左
    public int Right;// 右
    public int Forward;// 前
    public int Backward;// 後ろ
    // Evolve機能に必要な奴　進化したときに駒に追加する行動範囲
    public int EvolveUpperLeft;// 左斜め前
    public int EvolveLowerLeft;// 左斜め後ろ
    public int EvolveUpperRight;// 右斜め前
    public int EvolveLowerRight;// 右斜め後ろ
    public int EvolveLeft;// 左
    public int EvolveRight;// 右
    public int EvolveForward;// 前
    public int EvolveBackward;// 後ろ
}

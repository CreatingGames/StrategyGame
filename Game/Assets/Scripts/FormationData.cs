using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FormationData : MonoBehaviour
{
    public struct Formation
    {
        public int UpperLeft;// 左斜め前
        public int LowerLeft;// 左斜め後ろ
        public int UpperRight;// 右斜め前
        public int LowerRight;// 右斜め後ろ
        public int Left;// 左
        public int Right;// 右
        public int Forward;// 前
        public int Backward;// 後ろ
    }
    public Formation[,] shortBoard = new Formation[2, 5];
    private void Start()
    {

    }
}

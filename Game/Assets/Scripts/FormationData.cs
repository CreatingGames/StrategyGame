using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FormationData : MonoBehaviour
{
    public struct Formation
    {
        public int UpperLeft;// ���΂ߑO
        public int LowerLeft;// ���΂ߌ��
        public int UpperRight;// �E�΂ߑO
        public int LowerRight;// �E�΂ߌ��
        public int Left;// ��
        public int Right;// �E
        public int Forward;// �O
        public int Backward;// ���
    }
    public Formation[,] shortBoard = new Formation[2, 5];
    private void Start()
    {

    }
}

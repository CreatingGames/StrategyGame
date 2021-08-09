using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : PieceData
{
    // Test�p�f�[�^����
    [SerializeField] int t_UpperLeft = 0;
    [SerializeField] int t_LowerLeft = 0;
    [SerializeField] int t_UpperRight = 0;
    [SerializeField] int t_LowerRight = 0;
    [SerializeField] int t_Left = 0;
    [SerializeField] int t_Right = 0;
    [SerializeField] int t_Forward = 0;
    [SerializeField] int t_Backward = 0;



    // ��̏��
    public bool evolved { get; set; } = false;
    private void Start()
    {
        // ���͂���Ȋ����Ŏg���Ă邯�ǁA�z���g�͐w�`�����[�h����Ƃ��ɌĂяo���悤�ɂ�����
        InitActionRange(t_UpperLeft, t_LowerLeft, t_UpperRight, t_LowerRight, t_Left, t_Right, t_Forward, t_Backward);
    }
    // �s���͈͂̏�����
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

    // ��ۗL����헪�|�C���g��Ԃ��B
    public int GetStrategyPoint()
    {
        int sum = UpperLeft + LowerLeft + UpperRight + LowerRight + Left + Right + Forward + Backward;
        return sum;
    }
}

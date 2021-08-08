using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // Testpf[^üÍ
    [SerializeField] int t_UpperLeft = 0;
    [SerializeField] int t_LowerLeft = 0;
    [SerializeField] int t_UpperRight = 0;
    [SerializeField] int t_LowerRight = 0;
    [SerializeField] int t_Left = 0;
    [SerializeField] int t_Right = 0;
    [SerializeField] int t_Forward = 0;
    [SerializeField] int t_Backward = 0;

    // îÌs®ÍÍ
    public int UpperLeft { get; set; } = 0;// ¶ÎßO
    public int LowerLeft { get; set; } = 0;// ¶Îßãë
    public int UpperRight { get; set; } = 0;// EÎßO
    public int LowerRight { get; set; } = 0;// EÎßãë
    public int Left { get; set; } = 0;// ¶
    public int Right { get; set; } = 0;// E
    public int Forward { get; set; } = 0;// O
    public int Backward { get; set; } = 0;// ãë

    // îÌóÔ
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

    // îªÛL·éíª|CgðÔ·B
    public int GetStrategyPoint()
    {
        int sum = UpperLeft + LowerLeft + UpperRight + LowerRight + Left + Right + Forward + Backward;
        return sum;
    }
}

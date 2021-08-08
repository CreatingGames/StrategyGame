using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    // ��̍s���͈�
    public int UpperLeft { get; set; } = 0;// ���΂ߑO
    public int LowerLeft { get; set; } = 0;// ���΂ߌ��
    public int UpperRight { get; set; } = 0;// �E�΂ߑO
    public int LowerRight { get; set; } = 0;// �E�΂ߌ��
    public int Left { get; set; } = 0;// ��
    public int Right { get; set; } = 0;// �E
    public int Forward { get; set; } = 0;// �O
    public int Backward { get; set; } = 0;// ���

    // ��̏��
    public bool evolved { get; set; } = false;

    // ��ۗL����헪�|�C���g��Ԃ��B
    public int GetStrategyPoint()
    {
        int sum = UpperLeft + LowerLeft + UpperRight + LowerRight + Left + Right + Forward + Backward;
        return sum;
    }
}

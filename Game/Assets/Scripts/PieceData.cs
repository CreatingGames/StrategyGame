using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceData : MonoBehaviour
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
    public int PositionX { get; set; } = 0;
    public int PositionY { get; set; } = 0;
}

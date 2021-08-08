using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] GameObject ParentBoard;
    // Test
    [SerializeField] int x;
    [SerializeField] int y;

    private GameObject[,] ChildBoard;// �Ղ̃}�X
    public GameObject[,] GameBoard; // �Ֆʂ̊Ǘ�
    private int BoardSize;
    private void Start()
    {
        if((int)Mathf.Sqrt(ParentBoard.transform.childCount) == Mathf.Sqrt(ParentBoard.transform.childCount))
        {
            BoardSize = (int)Mathf.Sqrt(ParentBoard.transform.childCount);
        }
        
        GameBoard = new GameObject[BoardSize, BoardSize];
        GetAllChildBoard();
        ChangeImageTransparency(x, y);
    }
    // �C���X�y�N�^�[�Ŏ擾����Board����q�v�f�̂��ꂼ���Image���擾����
    private void GetAllChildBoard()
    {
        ChildBoard = new GameObject[BoardSize, BoardSize];
        int index = 0;
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                ChildBoard[i, j] = ParentBoard.transform.GetChild(index).gameObject;
                index++;
            }
        }
    }
    public void ChangeImageTransparency(int x,int y)
    {
        float red = ChildBoard[y, x].GetComponent<Image>().color.r;
        float green = ChildBoard[y, x].GetComponent<Image>().color.g;
        float blue = ChildBoard[y, x].GetComponent<Image>().color.b;
        float alfa = 255;
        ChildBoard[y, x].GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
}

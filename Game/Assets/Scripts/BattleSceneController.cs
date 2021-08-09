using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] GameObject ParentBoard;
    [SerializeField] GameObject Formation;
    [SerializeField] GameObject PiecePrefab;
    [SerializeField] GameObject Canvas;
    [Header("Test�p�ϐ�")]
    [SerializeField] int x;
    [SerializeField] int y;

    private GameObject[,] ChildBoard;// �Ղ̃}�X
    public GameObject[,] GameBoard; // �Ֆʂ̊Ǘ�
    private int BoardSize;// �Ղ̃T�C�Y
    private Vector3[,] ChildBoardPosition;
    private Vector3 piecePositionZ = new Vector3(0.0f, 0.0f, -0.46296296296f);// ���������I�u�W�F�N�g�ʒu��z��-50�ɂ��邽�߂Ƀ��^�I�ɂ������Ă�B
    private void Start()
    {
        if ((int)Mathf.Sqrt(ParentBoard.transform.childCount) == Mathf.Sqrt(ParentBoard.transform.childCount))
        {
            BoardSize = (int)Mathf.Sqrt(ParentBoard.transform.childCount);
        }

        GameBoard = new GameObject[BoardSize, BoardSize];
        GetAllChildBoard();
        GetAllChildBoardPosition();
        ChangeImageTransparency(x, y);
        StartCoroutine(LoadFormation());
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
    private void GetAllChildBoardPosition()
    {
        ChildBoardPosition = new Vector3[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                ChildBoardPosition[i, j] = ChildBoard[i, j].transform.position + piecePositionZ;
                //ChildBoardPosition[i, j].z -= piecePositionZ;
                print(ChildBoardPosition[i, j].z);
            }
        }
    }
    public void ChangeImageTransparency(int x, int y)
    {
        float red = ChildBoard[y, x].GetComponent<Image>().color.r;
        float green = ChildBoard[y, x].GetComponent<Image>().color.g;
        float blue = ChildBoard[y, x].GetComponent<Image>().color.b;
        float alfa = 255;
        ChildBoard[y, x].GetComponent<Image>().color = new Color(red, green, blue, alfa);
    }
    private IEnumerator LoadFormation()
    {
        Formation.GetComponent<FormationData>().InitFormationData();
        while (!Formation.GetComponent<FormationData>().isClomplete)
        {
            // child��isComplete�ϐ���true�ɂȂ�܂őҋ@
            yield return new WaitForEndOfFrame();
        }
        PieceData[,] shortBoard = Formation.GetComponent<FormationData>().shortBoard;
        // �����͂������񃁃^�Ƀ��[�v�񐔂����߂�
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (Formation.GetComponent<FormationData>().shortBoard[i, j] != null)
                {
                    GameBoard[i + 3, j] = (GameObject)Instantiate(PiecePrefab, ChildBoardPosition[i + 3, j], Quaternion.identity, Canvas.transform);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitActionRange(shortBoard[i, j].UpperLeft, shortBoard[i, j].LowerLeft, shortBoard[i, j].UpperRight, shortBoard[i, j].LowerRight, shortBoard[i, j].Left, shortBoard[i, j].Right, shortBoard[i, j].Forward, shortBoard[i, j].Backward);
                    GameBoard[i + 3, j].GetComponent<Piece>().InitPosition(i + 3, j);
                }
            }
        }
    }
}

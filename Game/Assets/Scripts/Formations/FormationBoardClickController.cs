using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationBoardClickController: MonoBehaviour
{
    void Update()
    {
        // �N���b�N�����Ƃ��ɏd�Ȃ���3d�I�u�W�F�N�g���܂Ƃ߂Ď擾����
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                // Tag(BoardSquare)��p�ӂ��āABoardSquare��Tag�Ƃ��ēo�^����Ă���Ƃ��̂�
                if (hit.collider.gameObject.CompareTag("BoardSquare"))
                {
                    hit.collider.GetComponent<FormationBoardSquare>().OnClicked();
                }
                else if(hit.collider.gameObject.CompareTag("Piece"))
                {
                    hit.collider.GetComponent<FormationPiece>().OnClicked();
                }
            }
        }
    }
}

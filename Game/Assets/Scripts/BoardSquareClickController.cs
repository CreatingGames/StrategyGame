using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquareClickController : MonoBehaviour
{
    public bool CreatePaletteCheck = false; // Dialogtagを持つオブジェクトがあったら盤面のクリック判定を呼び出さないようにする
    void Update()
    {
        // クリックしたときに重なった3dオブジェクトをまとめて取得する
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                // Tag(BoardSquare)を用意して、BoardSquareがTagとして登録されているときのみ
                if (hit.collider.gameObject.CompareTag("BoardSquare") && !CreatePaletteCheck)
                {
                    hit.collider.GetComponent<BoardSquare>().OnClicked();
                }
            }
        }
    }
}
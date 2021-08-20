using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquareClickController : MonoBehaviour
{
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            foreach (RaycastHit hit in Physics.RaycastAll(ray))
            {
                if (hit.collider.gameObject.CompareTag("BoardSquare"))
                {
                    hit.collider.GetComponent<BoardSquare>().OnClicked();
                }
            }
        }
    }
}
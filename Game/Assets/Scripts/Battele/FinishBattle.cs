using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishBattle : MonoBehaviour
{
    [SerializeField] Text text;
    public void Winner()
    {
        text.text = "����";
    }
    public void Loser()
    {
        text.text = "�s�k";
    }
}

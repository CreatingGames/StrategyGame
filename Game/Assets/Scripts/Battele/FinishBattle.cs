using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishBattle : MonoBehaviour
{
    [SerializeField] Text text;
    public void Winner()
    {
        text.text = "èüóò";
    }
    public void Loser()
    {
        text.text = "îsñk";
    }
}

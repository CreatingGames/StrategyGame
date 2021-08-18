using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class AccountScene : MonoBehaviour
{
    private Text UserNameText;
    private Text ItemList;
    public static string InventoryItem;
    // Start is called before the first frame update
    void Start()
    {
        UserNameText = GameObject.Find("UsernameMsg").gameObject.GetComponent<Text>();
        ItemList = GameObject.Find("Item").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        UserNameText.text = "ユーザー名： " + AccountPrefs.GetAccountUsername();
        string msg = AccountManager.PieceList[0].Name;
        for (int i = 1; i < AccountManager.PieceList.Count; i++)
        {
            msg = msg + " " + AccountManager.PieceList[i].Name;
        }
        ItemList.text = msg;

    }
}

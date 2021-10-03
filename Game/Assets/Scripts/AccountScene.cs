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
    }

    // Update is called once per frame
    void Update()
    {
        
        UserNameText.text = "ユーザー名： ";

        
    }
}

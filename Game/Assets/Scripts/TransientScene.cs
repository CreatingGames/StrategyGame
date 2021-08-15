using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransientScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //アカウント画面へ遷移
    public void OnClickToAccountSceneButton()
    {
        SceneManager.LoadScene("AccountScene");
    }

    //メイン画面へ遷移
    public void OnClickToMainSceneButton()
    {
        SceneManager.LoadScene("MainScene");
    }

}

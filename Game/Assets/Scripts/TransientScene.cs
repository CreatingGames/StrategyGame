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
    //�A�J�E���g��ʂ֑J��
    public void OnClickToAccountSceneButton()
    {
        SceneManager.LoadScene("AccountScene");
    }

    //���C����ʂ֑J��
    public void OnClickToMainSceneButton()
    {
        SceneManager.LoadScene("MainScene");
    }

}

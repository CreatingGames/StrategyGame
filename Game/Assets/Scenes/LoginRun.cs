using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginRun : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
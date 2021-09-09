using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Effect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    //public Image image;
    public Animator Animator;
    public AudioClip Sound1;
    AudioSource audioSource;


    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
   public void OnPointerEnter(PointerEventData eventData)
   {
      //Debug.Log("true");
      Animator.SetBool("hude_effect", true);
      audioSource.PlayOneShot(Sound1);
    }

   public void OnPointerExit(PointerEventData eventData)
   {
       //Debug.Log("false");
        //animator.SetFloat(Animator.StringToHash("speed"), -1);
        Animator.SetBool("hude_effect", false);
    }

    // Use this for initialization
    void Start()
    {
        this.Animator = GetComponent<Animator>();
        Animator.SetBool("hude_effect", false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

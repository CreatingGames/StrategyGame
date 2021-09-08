using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Effect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    //public Image image;
    public Animator animator;
    public AudioClip sound1;
    AudioSource audioSource;


    // オブジェクトの範囲内にマウスポインタが入った際に呼び出されます。
    // this method called by mouse-pointer enter the object.
   public void OnPointerEnter(PointerEventData eventData)
   {
      Debug.Log("true");
      animator.SetBool("hude_effect", true);
      audioSource.PlayOneShot(sound1);
    }

   public void OnPointerExit(PointerEventData eventData)
   {
       Debug.Log("false");
        //animator.SetFloat(Animator.StringToHash("speed"), -1);
        animator.SetBool("hude_effect", false);
    }

    // Use this for initialization
    void Start()
    {
        
        this.animator = GetComponent<Animator>();
        animator.SetBool("hude_effect", false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

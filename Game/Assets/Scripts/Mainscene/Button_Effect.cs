using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Button_Effect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    //public Image image;
    public Animator animator;


    // �I�u�W�F�N�g�͈͓̔��Ƀ}�E�X�|�C���^���������ۂɌĂяo����܂��B
    // this method called by mouse-pointer enter the object.
   public void OnPointerEnter(PointerEventData eventData)
   {
      Debug.Log("true");
      animator.SetBool("hude_effect", true);
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

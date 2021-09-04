using System;
using UnityEngine;

public class CreatePalette : MonoBehaviour
{
    public enum CreatePaletteResult
    {
        OK,
        Cancel,
    }

    // �_�C�A���O�����삳�ꂽ�Ƃ��ɔ�������C�x���g
    public Action<CreatePaletteResult> FixDialog { get; set; }

    // OK�{�^���������ꂽ�Ƃ�
    public void OnOk()
    {
        this.FixDialog?.Invoke(CreatePaletteResult.OK);
        gameObject.SetActive(false);
    }

    // Cancel�{�^���������ꂽ�Ƃ�
    public void OnCancel()
    {
        // �C�x���g�ʒm�悪����Βʒm���ă_�C�A���O��j�����Ă��܂�
        this.FixDialog?.Invoke(CreatePaletteResult.Cancel);
        gameObject.SetActive(false);
    }
}

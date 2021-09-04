using System;
using UnityEngine;

public class CreatePalette : MonoBehaviour
{
    public enum CreatePaletteResult
    {
        OK,
        Cancel,
    }

    // ダイアログが操作されたときに発生するイベント
    public Action<CreatePaletteResult> FixDialog { get; set; }

    // OKボタンが押されたとき
    public void OnOk()
    {
        this.FixDialog?.Invoke(CreatePaletteResult.OK);
        gameObject.SetActive(false);
    }

    // Cancelボタンが押されたとき
    public void OnCancel()
    {
        // イベント通知先があれば通知してダイアログを破棄してしまう
        this.FixDialog?.Invoke(CreatePaletteResult.Cancel);
        gameObject.SetActive(false);
    }
}

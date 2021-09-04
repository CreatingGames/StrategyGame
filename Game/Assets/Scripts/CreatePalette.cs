using System;
using UnityEngine;

public class CreatePalette : MonoBehaviour
{
    public enum CreatePaletteResult
    {
        OK,
        Cancel,
    }
    public int UpperLeft;
    public int UpperRight;
    public int LowerLeft;
    public int LowerRight;
    public int Forward;
    public int Backward;
    public int Right;
    public int Left;
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

using System;
using UnityEngine;
using UnityEngine.UI;

public class CreatePalette : MonoBehaviour
{
    [SerializeField] Text ULText;
    [SerializeField] Text URText;
    [SerializeField] Text LLText;
    [SerializeField] Text LRText;
    [SerializeField] Text FText;
    [SerializeField] Text BText;
    [SerializeField] Text RText;
    [SerializeField] Text LText;
    [SerializeField] GameObject BattleSceneController;
    [SerializeField] Text BeforeStrategyPointText;
    [SerializeField] Text AfterStrategyPointText;
    [SerializeField] Text UseStrategyPointText;
    BattleSceneController battleSceneController;
    private int upperLeft;
    private int upperRight;
    private int lowerLeft;
    private int lowerRight;
    private int forward;
    private int backward;
    private int right;
    private int left;
    private int x;
    private int y;
    private int beforeStrategyPoint;
    private int useStrategyPoint;
    private int afterStarategyPoint;
    private void Start()
    {
        battleSceneController = BattleSceneController.GetComponent<BattleSceneController>();
        Init();
    }

    public void Init()
    {
        InitActionRange();
        UseStrategyPointText.text = "0";
        useStrategyPoint = 0;
    }

    private void Update()
    {
        battleSceneController.MakeBoardSquareWhite(x, y);
    }
    public enum CreatePaletteResult
    {
        OK,
        Cancel,
    }
    enum Direction
    {
        UpperLeft,
        UpperRight,
        LowerLeft,
        LowerRight,
        Forward,
        Backward,
        Right,
        Left
    }
    public void SetStrategyPoint(int SP)
    {
        beforeStrategyPoint = SP;
        afterStarategyPoint = SP;
        UpdateText();
    }

    // ダイアログが操作されたときに発生するイベント
    public Action<CreatePaletteResult> FixDialog { get; set; }
    private void ChangeBoardColorOpaque(int range, Direction direction)
    {
        BattleSceneController battleSceneController = BattleSceneController.GetComponent<BattleSceneController>();
        int boardSize = battleSceneController.BoardSize;
        int targetX = x;
        int targetY = y;
        switch (direction)
        {
            case Direction.UpperLeft:
                targetY = y - range;
                targetX = x - range;
                if (targetY >= 0 && targetX >= 0) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
            case Direction.UpperRight:
                targetY = y - range;
                targetX = x + range;
                if (targetY >= 0 && targetX < boardSize) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
            case Direction.LowerLeft:
                targetY = y + range;
                targetX = x - range;
                if (targetY < boardSize && targetX >= 0) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
            case Direction.LowerRight:
                targetY = y + range;
                targetX = x + range;
                if (targetY < boardSize && targetX < boardSize) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
            case Direction.Forward:
                targetY = y - range;
                if (targetY >= 0) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
            case Direction.Backward:
                targetY = y + range;
                if (targetY < boardSize) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
            case Direction.Right:
                targetX = x + range;
                if (targetX < boardSize) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
            case Direction.Left:
                targetX = x - range;
                if (targetX >= 0) battleSceneController.MakeBoardSquarOpaque(targetX, targetY, false);
                break;
        }
    }
    private void ChangeBoardSquareDefault(int range, Direction direction)
    {
        BattleSceneController battleSceneController = BattleSceneController.GetComponent<BattleSceneController>();
        int oldRnge = range + 1;
        int boardSize = battleSceneController.BoardSize;
        int targetX = x;
        int targetY = y;
        switch (direction)
        {
            case Direction.UpperLeft:
                targetY = y - oldRnge;
                targetX = x - oldRnge;
                if (targetY >= 0 && targetX >= 0) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
            case Direction.UpperRight:
                targetY = y - oldRnge;
                targetX = x + oldRnge;
                if (targetY >= 0 && targetX < boardSize) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
            case Direction.LowerLeft:
                targetY = y + oldRnge;
                targetX = x - oldRnge;
                if (targetY < boardSize && targetX >= 0) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
            case Direction.LowerRight:
                targetY = y + oldRnge;
                targetX = x + oldRnge;
                if (targetY < boardSize && targetX < boardSize) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
            case Direction.Forward:
                targetY = y - oldRnge;
                if (targetY >= 0) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
            case Direction.Backward:
                targetY = y + oldRnge;
                if (targetY < boardSize) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
            case Direction.Right:
                targetX = x + oldRnge;
                if (targetX < boardSize) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
            case Direction.Left:
                targetX = x - oldRnge;
                if (targetX >= 0) battleSceneController.MakeBoardSquareTransparent(targetX, targetY);
                break;
        }
    }
    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    private void InitActionRange()
    {
        upperLeft = 0;
        upperRight = 0;
        lowerLeft = 0;
        lowerRight = 0;
        forward = 0;
        backward = 0;
        right = 0;
        left = 0;
        UpdateText();
    }
    public void UpdateText()
    {
        ULText.text = upperLeft.ToString();
        URText.text = upperRight.ToString();
        LLText.text = lowerLeft.ToString();
        LRText.text = lowerRight.ToString();
        FText.text = forward.ToString();
        BText.text = backward.ToString();
        RText.text = right.ToString();
        LText.text = left.ToString();
        BeforeStrategyPointText.text = beforeStrategyPoint.ToString();
        AfterStrategyPointText.text = afterStarategyPoint.ToString();
        UseStrategyPointText.text = useStrategyPoint.ToString();
    }
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
    public CreateData GetCreateData()
    {
        CreateData createData = new CreateData(x, y, upperLeft, lowerLeft, upperRight, lowerRight, left, right, forward, backward);
        return createData;
    }
    public void OnULPlus()
    {
        upperLeft++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(upperLeft, Direction.UpperLeft);
    }
    public void OnULMinus()
    {
        if (upperLeft > 0)
        {

            upperLeft--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(upperLeft, Direction.UpperLeft);
        }
    }
    public void OnURPlus()
    {
        upperRight++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(upperRight, Direction.UpperRight);
    }
    public void OnURMinus()
    {
        if (upperRight > 0)
        {

            upperRight--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(upperRight, Direction.UpperRight);
        }
    }
    public void OnLLPlus()
    {
        lowerLeft++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(lowerLeft, Direction.LowerLeft);
    }
    public void OnLLMinus()
    {
        if (lowerLeft > 0)
        {
            lowerLeft--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(lowerLeft, Direction.LowerLeft);
        }
    }
    public void OnLRPlus()
    {
        lowerRight++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(lowerRight, Direction.LowerRight);
    }
    public void OnLRMinus()
    {
        if (lowerRight > 0)
        {
            lowerRight--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(lowerRight, Direction.LowerRight);
        }
    }
    public void OnFPlus()
    {
        forward++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(forward, Direction.Forward);
    }
    public void OnFMinus()
    {
        if (forward > 0)
        {
            forward--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(forward, Direction.Forward);
        }
    }
    public void OnBPlus()
    {
        backward++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(backward, Direction.Backward);
    }
    public void OnBMinus()
    {
        if (backward > 0)
        {
            backward--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(backward, Direction.Backward);
        }
    }
    public void OnRPlus()
    {
        right++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(right, Direction.Right);
    }
    public void OnRMinus()
    {
        if (right > 0)
        {
            right--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(right, Direction.Right);
        }
    }
    public void OnLPlus()
    {
        left++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(left, Direction.Left);
    }
    public void OnLMinus()
    {
        if (left > 0)
        {
            left--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(left, Direction.Left);
        }
    }
    public void CalcuratingUseStrategyPoint()
    {
        useStrategyPoint = StrategyPointSetting.CalcurateCreatingPoint(upperLeft, upperRight, lowerLeft, lowerRight, right, left, forward, backward);
        afterStarategyPoint = beforeStrategyPoint - useStrategyPoint;
    }
}

using System;
using UnityEngine;
using UnityEngine.UI;

public class EvolvePalette : MonoBehaviour
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

    private int evolveUpperLeft;
    private int evolveUpperRight;
    private int evolveLowerLeft;
    private int evolveLowerRight;
    private int evolveForward;
    private int evolveBackward;
    private int evolveRight;
    private int evolveLeft;
    private void Start()
    {
        battleSceneController = BattleSceneController.GetComponent<BattleSceneController>();
        Init();
    }

    public void Init()
    {
        InitEvolveActionRange();
        UseStrategyPointText.text = "0";
        useStrategyPoint = 0;
    }

    private void Update()
    {
        battleSceneController.MakeBoardSquareWhite(x, y);
    }
    public enum EvolvePaletteResult
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
    public void SetActionRange(Piece piece)
    {
        upperLeft = piece.UpperLeft;
        upperRight = piece.UpperRight;
        lowerLeft = piece.LowerLeft;
        lowerRight = piece.LowerRight;
        forward = piece.Forward;
        backward = piece.Backward;
        right = piece.Right;
        left = piece.Left;
        InitActionRangeBoardSquare();
    }
    private void InitActionRangeBoardSquare()
    {
        for(int i = 1; i < upperLeft; i++)
        {
            ChangeBoardColorOpaque(i, Direction.UpperLeft);
        }
        for (int i = 1; i < upperRight; i++)
        {
            ChangeBoardColorOpaque(i, Direction.UpperRight);
        }
        for (int i = 1; i < lowerLeft; i++)
        {
            ChangeBoardColorOpaque(i, Direction.LowerLeft);
        }
        for (int i = 1; i < lowerRight; i++)
        {
            ChangeBoardColorOpaque(i, Direction.LowerRight);
        }
        for (int i = 1; i < forward; i++)
        {
            ChangeBoardColorOpaque(i, Direction.Forward);
        }
        for (int i = 1; i < backward; i++)
        {
            ChangeBoardColorOpaque(i, Direction.Backward);
        }
        for (int i = 1; i < right; i++)
        {
            ChangeBoardColorOpaque(i, Direction.Right);
        }
        for (int i = 1; i < left; i++)
        {
            ChangeBoardColorOpaque(i, Direction.Left);
        }
    }
    // ダイアログが操作されたときに発生するイベント
    public Action<EvolvePaletteResult> FixDialog { get; set; }
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
    private void InitEvolveActionRange()
    {
        evolveUpperLeft = 0;
        evolveUpperRight = 0;
        evolveLowerLeft = 0;
        evolveLowerRight = 0;
        evolveForward = 0;
        evolveBackward = 0;
        evolveRight = 0;
        evolveLeft = 0;
        UpdateText();
    }
    public void UpdateText()
    {
        ULText.text = (upperLeft + evolveUpperLeft).ToString();
        URText.text = (upperRight + evolveUpperRight).ToString();
        LLText.text = (lowerLeft + evolveLowerLeft).ToString();
        LRText.text = (lowerRight + evolveLowerRight).ToString();
        FText.text = (forward + evolveForward).ToString();
        BText.text = (backward + evolveBackward).ToString();
        RText.text = (right + evolveRight).ToString();
        LText.text = (left + evolveLeft).ToString();
        BeforeStrategyPointText.text = beforeStrategyPoint.ToString();
        AfterStrategyPointText.text = afterStarategyPoint.ToString();
        UseStrategyPointText.text = useStrategyPoint.ToString();
    }
    // OKボタンが押されたとき
    public void OnOk()
    {
        this.FixDialog?.Invoke(EvolvePaletteResult.OK);
        gameObject.SetActive(false);
    }

    // Cancelボタンが押されたとき
    public void OnCancel()
    {
        // イベント通知先があれば通知してダイアログを破棄してしまう
        this.FixDialog?.Invoke(EvolvePaletteResult.Cancel);
        gameObject.SetActive(false);
    }
    public EvolveData GetEvolveData()
    {
        EvolveData evolveData = new EvolveData(x, y, evolveUpperLeft, evolveLowerLeft, evolveUpperRight, evolveLowerRight, evolveLeft, evolveRight, evolveForward, evolveBackward);
        return evolveData;
    }
    public void OnULPlus()
    {
        evolveUpperLeft++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(upperLeft + evolveUpperLeft, Direction.UpperLeft);
    }
    public void OnULMinus()
    {
        if (evolveUpperLeft > 0)
        {

            evolveUpperLeft--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(upperLeft + evolveUpperLeft, Direction.UpperLeft);
        }
    }
    public void OnURPlus()
    {
        evolveUpperRight++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(upperRight + evolveUpperRight, Direction.UpperRight);
    }
    public void OnURMinus()
    {
        if (evolveUpperRight > 0)
        {

            evolveUpperRight--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(upperRight + evolveUpperRight, Direction.UpperRight);
        }
    }
    public void OnLLPlus()
    {
        evolveLowerLeft++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(lowerLeft + evolveLowerLeft, Direction.LowerLeft);
    }
    public void OnLLMinus()
    {
        if (evolveLowerLeft > 0)
        {
            evolveLowerLeft--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(lowerLeft + evolveLowerLeft, Direction.LowerLeft);
        }
    }
    public void OnLRPlus()
    {
        evolveLowerRight++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(lowerRight + evolveLowerRight, Direction.LowerRight);
    }
    public void OnLRMinus()
    {
        if (evolveLowerRight > 0)
        {
            evolveLowerRight--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(lowerRight + evolveLowerRight, Direction.LowerRight);
        }
    }
    public void OnFPlus()
    {
        evolveForward++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(forward + evolveForward, Direction.Forward);
    }
    public void OnFMinus()
    {
        if (evolveForward > 0)
        {
            evolveForward--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(forward + evolveForward, Direction.Forward);
        }
    }
    public void OnBPlus()
    {
        evolveBackward++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(backward + evolveBackward, Direction.Backward);
    }
    public void OnBMinus()
    {
        if (evolveBackward > 0)
        {
            evolveBackward--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(backward + evolveBackward, Direction.Backward);
        }
    }
    public void OnRPlus()
    {
        evolveRight++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(right + evolveRight, Direction.Right);
    }
    public void OnRMinus()
    {
        if (evolveRight > 0)
        {
            evolveRight--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(right + evolveRight, Direction.Right);
        }
    }
    public void OnLPlus()
    {
        evolveLeft++;
        CalcuratingUseStrategyPoint();
        UpdateText();
        ChangeBoardColorOpaque(left + evolveLeft, Direction.Left);
    }
    public void OnLMinus()
    {
        if (evolveLeft > 0)
        {
            evolveLeft--;
            CalcuratingUseStrategyPoint();
            UpdateText();
            ChangeBoardSquareDefault(left + evolveLeft, Direction.Left);
        }
    }
    public void CalcuratingUseStrategyPoint()
    {
        useStrategyPoint = StrategyPointSetting.CalcurateCreatingPoint(upperLeft, upperRight, lowerLeft, lowerRight, right, left, forward, backward);
        afterStarategyPoint = beforeStrategyPoint - useStrategyPoint;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateFormationPalette : MonoBehaviour
{
    [SerializeField] Text ULText;
    [SerializeField] Text URText;
    [SerializeField] Text LLText;
    [SerializeField] Text LRText;
    [SerializeField] Text FText;
    [SerializeField] Text BText;
    [SerializeField] Text RText;
    [SerializeField] Text LText;
    [SerializeField] Text BeforeStrategyPointText;
    [SerializeField] Text AfterStrategyPointText;
    [SerializeField] Text UseStrategyPointText;
    [SerializeField] Text ModeText;
    [SerializeField] Text StrategyPointText;
    [SerializeField] Text PieceStrategyPointText;
    [SerializeField] Text CurrentStrategyPointText;
    [SerializeField] GameObject NormalMode;
    [SerializeField] GameObject CreateMode;
    [SerializeField] GameObject PieceMode;
    [SerializeField] GameObject CreateFormationController;
    CreateFormationController createFormationController;
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
    private int StrategyPoint;
    private int PieceStrategyPoint;
    private int CurrentStrategyPoint;
    private Modes mode;
    private void Start()
    {
        Init();
        createFormationController = CreateFormationController.GetComponent<CreateFormationController>();
    }

    public void Init()
    {
        InitActionRange();
        UseStrategyPointText.text = "0";
        useStrategyPoint = 0;
        beforeStrategyPoint = StrategyPointSetting.InitialStrategyPoint;
        StrategyPoint = StrategyPointSetting.InitialStrategyPoint;
        CurrentStrategyPoint = StrategyPointSetting.InitialStrategyPoint;
        mode = Modes.Normal;
    }

    private void Update()
    {
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
    enum Modes
    {
        Normal,
        Create,
        Piece
    }
    private void SetPosition(int x, int y)
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
        StrategyPointText.text = StrategyPoint.ToString();
        PieceStrategyPointText.text = PieceStrategyPoint.ToString();
        CurrentStrategyPointText.text = CurrentStrategyPoint.ToString();
        ModeText.text = mode.ToString();
    }
    public void OnBoardSquareClicked(int x, int y)
    {
        SetPosition(x, y);
        ModeToCreate();
        InitActionRange();
        useStrategyPoint = 0;
        afterStarategyPoint = StrategyPoint;
        beforeStrategyPoint = StrategyPoint;
        UpdateText();
    }
    public void OnPieceClicked(int x, int y, int UL, int UR, int LL, int LR, int F, int B, int R, int L)
    {
        SetPosition(x, y);
        ModeToPiece();
        upperLeft = UL;
        upperRight = UR;
        lowerLeft = LL;
        lowerRight = LR;
        backward = B;
        forward = F;
        left = L;
        right = R;
        PieceStrategyPoint = StrategyPointSetting.CalcurateCreatingPoint(upperLeft, upperRight, lowerLeft, lowerRight, right, left, forward, backward);
        UpdateText();
    }
    // OKボタンが押されたとき
    public void OnOk()
    {
        ModeToNormal();
        InitActionRange();
        UpdateText();
    }
    // Cancelボタンが押されたとき
    public void OnCancel()
    {
        ModeToNormal();
        InitActionRange();
        UpdateText();
    }
    public void OnSave()
    {

    }
    public void OnDelete()
    {
        createFormationController.DeletePiece(x, y);
        ModeToNormal();
        InitActionRange();
        UpdateText();
    }
    public void ModeToCreate()
    {
        mode = Modes.Create;
        CreateMode.SetActive(true);
        PieceMode.SetActive(false);
        NormalMode.SetActive(false);
    }
    public void ModeToPiece()
    {
        mode = Modes.Piece;
        CreateMode.SetActive(false);
        PieceMode.SetActive(true);
        NormalMode.SetActive(false);
    }
    public void ModeToNormal()
    {
        mode = Modes.Piece;
        CreateMode.SetActive(false);
        PieceMode.SetActive(false);
        NormalMode.SetActive(true);
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
    }
    public void OnULMinus()
    {
        if (upperLeft > 0)
        {

            upperLeft--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void OnURPlus()
    {
        upperRight++;
        CalcuratingUseStrategyPoint();
        UpdateText();
    }
    public void OnURMinus()
    {
        if (upperRight > 0)
        {

            upperRight--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void OnLLPlus()
    {
        lowerLeft++;
        CalcuratingUseStrategyPoint();
        UpdateText();
    }
    public void OnLLMinus()
    {
        if (lowerLeft > 0)
        {
            lowerLeft--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void OnLRPlus()
    {
        lowerRight++;
        CalcuratingUseStrategyPoint();
        UpdateText();
    }
    public void OnLRMinus()
    {
        if (lowerRight > 0)
        {
            lowerRight--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void OnFPlus()
    {
        forward++;
        CalcuratingUseStrategyPoint();
        UpdateText();
    }
    public void OnFMinus()
    {
        if (forward > 0)
        {
            forward--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void OnBPlus()
    {
        backward++;
        CalcuratingUseStrategyPoint();
        UpdateText();
    }
    public void OnBMinus()
    {
        if (backward > 0)
        {
            backward--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void OnRPlus()
    {
        right++;
        CalcuratingUseStrategyPoint();
        UpdateText();
    }
    public void OnRMinus()
    {
        if (right > 0)
        {
            right--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void OnLPlus()
    {
        left++;
        CalcuratingUseStrategyPoint();
        UpdateText();
    }
    public void OnLMinus()
    {
        if (left > 0)
        {
            left--;
            CalcuratingUseStrategyPoint();
            UpdateText();
        }
    }
    public void CalcuratingUseStrategyPoint()
    {
        useStrategyPoint = StrategyPointSetting.CalcurateCreatingPoint(upperLeft, upperRight, lowerLeft, lowerRight, right, left, forward, backward);
        afterStarategyPoint = beforeStrategyPoint - useStrategyPoint;
    }
}

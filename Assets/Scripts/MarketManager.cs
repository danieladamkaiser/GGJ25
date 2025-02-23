using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public partial class MarketManager : MonoBehaviour
{

    public TMP_Text valuationText;
    public TMP_Text debtText;
    public TMP_Text stageText;
    public TMP_Text interestText;
    public TMP_Text scoreText;
    public TMP_Text globalMultiplierText;
    public GameObject panel;
    public TMP_Text gameOverText;
    public int currentValuation;
    public int baseValuation;
    public int currentDebt;
    public float currentProgress;
    public float globalModifier = 1;
    public int currentLevel;
    public GameObject nextLevelButton;


    public Bar valueBar;
    public Bar stageBar;
    public Bar debtBar;

    public Level[] levels;
    //public Stage[] stages;


    public GameObject progressSliderGO;

    [Range(0, 100f)]
    public float incrementRangeMin;
    [Range(0, 100f)]
    public float incrementRangeMax;

    [Range(0, 10f)]
    public float currentDemand;
    [Range(0, 10)]
    public int interestRate;

    private Slider slider;
    private SceneSwapper sceneSwapper;
    private bool isCompanySold;
    private float sharesSold;
    private int currentStage;

    public bool IsGameOver => currentProgress > GetTotalDuration() || isCompanySold;

    // Start is called before the first frame update
    void Start()
    {
        sceneSwapper = FindObjectOfType<SceneSwapper>();
        if (sceneSwapper == null)
        {
            currentLevel = 1;
        }
        else
        {
            currentLevel = sceneSwapper.level - 1;
        }
        stageText.outlineWidth = 0.25f;
        stageText.outlineColor = Color.black;
        SetDebt(currentDebt);
        SetValuation(currentValuation);
        SetStage();
    }

    public float GetTotalDuration()
    {
        return levels[currentLevel].Stages.Sum(s => s.Duration);
    }

    private void OnValidate()
    {
    }

    public void SellCompany()
    {
        isCompanySold = true;
    }

    public int GetCreditworthiness()
    {
        return int.MaxValue;
    }

    private void AddInterest()
    {
        var newDebt = (int)(currentDebt * (1 + interestRate / 100f));
        SetDebt(newDebt);
    }

    public void AddDebt(int value)
    {
        SetDebt(currentDebt + value);
    }

    public void SetValuation(int value)
    {
        if (value == currentValuation) return;
        valuationText.text = $"{value} $";
        currentValuation = value;
        valueBar.SetBar(currentValuation / 20000f);
        
        valuationText.outlineColor = Color.black;
        valuationText.outlineWidth = 0.25f;
    }
    private void SetDebt(int value)
    {
        if (value == currentDebt) return;
        debtText.text = $"{value} $";
        currentDebt = value;
        debtBar.SetBar(currentDebt / 20000f);
        
        debtText.outlineColor = Color.black;
        debtText.outlineWidth = 0.25f;
    }

    public void NextIteration()
    {

        if (IsGameOver)
        {
            ShowOverScreen();
        }
        else
        {
            globalModifier *= (100 + levels[currentLevel].Stages[currentStage].GrowthRate) / 100f;
            AddInterest();
            currentProgress += GetIncrementValue();
            SetStage();
        }

        if (IsGameOver)
        {
            ShowOverScreen();
        }
    }

    private void ShowOverScreen()
    {
        scoreText.text = $"{currentValuation - currentDebt} $";
        panel.SetActive(true);
        bool won = currentValuation > currentDebt;
        nextLevelButton.SetActive(won);
        gameOverText.text = won ? "You won!" : "You lost!";
    }

    public void Restart()
    {
        sceneSwapper.StartGame();
    }

    public void GoToMainMenu()
    {
        sceneSwapper.GoToMainMenu();
    }

    public void NextLevel()
    {
        sceneSwapper.AddLevel();
        sceneSwapper.StartGame();
    }

    private void SetStage()
    {
        var currentStageProgress = 0f;
        int i = -1;
        do
        {
            i++;

            if (i == levels[currentLevel].Stages.Length)
            {
                break;
            }

            currentStageProgress = currentProgress - levels[currentLevel].Stages.Take(i).Sum(s => s.Duration);


        } while (currentStageProgress > levels[currentLevel].Stages[i].Duration);

        i = Mathf.Clamp(i, 0, levels[currentLevel].Stages.Length - 1);

        currentStage = i;
        stageText.text = levels[currentLevel].Stages[i].Type.ToString();
        stageText.color = levels[currentLevel].Stages[i].Color;
        interestRate = levels[currentLevel].Stages[i].InterestRates;
        interestText.text = interestRate.ToString() + " %";
        stageBar.SetBar(globalModifier / 8f);
    }

    private float GetIncrementValue()
    {
        return 1;
    }
    // Update is called once per frame
    void Update()
    {
        globalMultiplierText.text = "x" + globalModifier.ToString("F2");
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NextIteration();
        }
    }

    public void SellShares()
    {
        var valueSold = currentValuation * 0.1f;
        sharesSold += valueSold;
        var newDebt = currentDebt - (int)valueSold;
        SetDebt(newDebt);
    }

    public void Lobby()
    {
        globalModifier *= 1.2f;
        stageBar.SetBar(globalModifier / 8f);
        incrementRangeMax *= 1.4f;
    }
}

using System.Linq;
using TMPro;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
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

    public Bar valueBar;
    public Bar stageBar;
    public Bar debtBar;
    
    [SerializeField]
    public Stage[] stages;


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

    public bool IsGameOver => currentProgress > GetTotalDuration() || isCompanySold;

    // Start is called before the first frame update
    void Start()
    {
        sceneSwapper = FindObjectOfType<SceneSwapper>();
        stageText.outlineWidth = 0.25f;
        stageText.outlineColor = Color.black;
        slider = progressSliderGO.GetComponent<Slider>();
        slider.maxValue = GetTotalDuration();
        SetDebt(currentDebt);
        SetValuation(currentValuation);
        SetStage();
    }

    private float GetTotalDuration()
    {
        return stages.Sum(s => s.Duration);
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
        currentDebt = (int)(currentDebt * (1 + interestRate / 100f));
        SetDebt(currentDebt);
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
    }
    private void SetDebt(int value)
    {
        if (value == currentDebt) return;
        debtText.text = $"{value} $";
        currentDebt = value;
        debtBar.SetBar(currentDebt / 20000f);
    }

    public void NextIteration()
    {
        AddInterest();
        currentProgress += GetIncrementValue();
        slider.value = currentProgress;
        SetStage();

        if (IsGameOver)
        {
            scoreText.text = $"{currentValuation - currentDebt} $";
            panel.SetActive(true);
            bool won = currentValuation > currentDebt;
            gameOverText.text = won ? "You won!" : "You lost!";
        }
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

            if (i == stages.Length)
            {
                break;
            }

            currentStageProgress = currentProgress - stages.Take(i).Sum(s => s.Duration);


        } while (currentStageProgress > stages[i].Duration);

        i = Mathf.Clamp(i, 0, stages.Length - 1);

        stageText.text = stages[i].Type.ToString();
        stageText.color = stages[i].Color;
        interestRate = stages[i].InterestRates;
        interestText.text = interestRate.ToString() + " %";
        globalModifier *= (100 + stages[i].GrowthRate) / 100f;
        stageBar.SetBar(globalModifier / 8f);
    }

    private float GetIncrementValue()
    {
        return Random.Range(incrementRangeMin, incrementRangeMax);
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
        currentDebt -= (int)valueSold;
    }

    public void Lobby()
    {
        globalModifier *= 1.2f;
        stageBar.SetBar(globalModifier / 8f);
        incrementRangeMax *= 1.4f;
    }
}

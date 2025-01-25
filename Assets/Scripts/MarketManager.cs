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

    public GameObject panel;

    public int currentValuation;
    public int baseValuation = 1000;
    public int currentDebt;
    public float currentProgress;
    public float globalModifier = 1;

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
        valuationText.text = $"{value} $";
        currentValuation = value;
    }
    private void SetDebt(int value)
    {
        debtText.text = $"{value} $";
        currentDebt = value;
    }

    public void NextIteration()
    {
        currentProgress += GetIncrementValue();
        slider.value = currentProgress;
        SetStage();
        AddInterest();

        if (IsGameOver)
        {
            scoreText.text = $"{currentValuation - currentDebt} $";
            panel.SetActive(true);
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
    }

    private float GetIncrementValue()
    {
        return Random.Range(incrementRangeMin, incrementRangeMax);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            NextIteration();
        }
    }
}

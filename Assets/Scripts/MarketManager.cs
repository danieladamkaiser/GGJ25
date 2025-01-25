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

    public int currentValuation;
    public int currentDebt;
    public float currentProgress;

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

    // Start is called before the first frame update
    void Start()
    {
        stageText.outlineWidth = 0.25f;
        stageText.outlineColor = Color.black;
        slider = progressSliderGO.GetComponent<Slider>();
        slider.maxValue = stages.Sum(s => s.Duration);
        SetDebt(currentDebt);
        SetValuation(currentValuation);
    }

    private void OnValidate()
    {
    }

    public int GetCreditworthiness()
    {
        return currentValuation - currentDebt;
    }

    private void AddInterest()
    {
        currentDebt = (int)(currentDebt * (1 + interestRate / 100f));
        SetDebt(currentDebt);
    }

    public void AddValuation(int value)
    {
        SetValuation(currentValuation + value);
    }

    public void AddDebt(int value)
    {
        SetDebt(currentDebt + value);
    }

    private void SetValuation(int value)
    {
        valuationText.text = $"{value} $";
        currentValuation = value;
    }
    private void SetDebt(int value)
    {
        debtText.text = $"{value} $";
        currentDebt = value;
    }

    private void NextIteration()
    {
        currentProgress += GetIncrementValue();
        slider.value = currentProgress;
        SetStage();
        AddInterest();
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

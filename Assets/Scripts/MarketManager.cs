using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class MarketManager : MonoBehaviour
{
    public TMP_Text valuationText;
    public TMP_Text debtText;

    public int currentValuation;
    public int currentDebt;
    public float currentProgress;

    public GameObject progressSliderGO;
    public RawImage[] fillColors;

    [Range(0f, 100f)]
    public float stageDuration;
    [Range(0,100f)]
    public float incrementRangeMin;
    [Range(0, 100f)]
    public float incrementRangeMax;

    [Range(0,10f)]
    public float currentDemand;
    [Range(0,10)]
    public int interestRate;

    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = progressSliderGO.GetComponent<Slider>();
        getcom
    }

    private void OnValidate()
    {
        SetValuation(currentValuation);
        SetDebt(currentDebt);
        foreach (var image in fillColors)
        {
            image.color = Color.red;
        }
    }

    private void AddInterest()
    {
        currentDebt = (int)(currentDebt * (1 + interestRate / 100f));
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

    // Update is called once per frame
    void Update()
    {
        
    }
}

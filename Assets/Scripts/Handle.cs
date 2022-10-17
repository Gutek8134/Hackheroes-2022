using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Handle : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField population;
    private uint position;

    [SerializeField]
    private Slider slider;
    private TMP_Text members;

    void Awake()
    {
        position = transform.parent.parent.GetComponent<PartySizer>().position;
        population = GameObject.Find("PopField").GetComponent<TMP_InputField>();
        slider.wholeNumbers = true;
        members = GetComponentInChildren<TMP_Text>();
        population.text = "100000";
        members.text = population.text;
        slider.value = slider.maxValue =
            System.Convert.ToInt32(population.text) - PeopleInParties();
    }

    void Update()
    {
        int pip = PeopleInParties();
        int newMax = System.Convert.ToInt32(population.text) - pip;
        int newVal = (int)slider.value * (newMax / (int)slider.maxValue);
        if (slider.value == slider.maxValue)
            slider.value = newMax;
        slider.maxValue = newMax;
        members.text = slider.value.ToString();
    }

    int PeopleInParties()
    {
        if (position == 0)
            return 0;
        int sum = 0;
        for (int i = 0; i < position; ++i)
            sum += (int)PartyManager.sliders[i].value;
        return sum;
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Handle : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField population;

    [SerializeField]
    private Slider slider;
    private TMP_Text members;

    // Start is called before the first frame update
    void Start()
    {
        population = GameObject.Find("PopField").GetComponent<TMP_InputField>();
        slider.wholeNumbers = true;
        members = GetComponentInChildren<TMP_Text>();
        population.text = "100000";
        members.text = population.text;
        slider.value = slider.maxValue = System.Convert.ToInt32(population.text);
    }

    // Update is called once per frame
    void Update()
    {
        slider.maxValue = System.Convert.ToInt32(population.text);
        members.text = slider.value.ToString();
    }
}

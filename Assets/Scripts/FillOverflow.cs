using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FillOverflow : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private TMP_Text partyName;

    // Start is called before the first frame update
    void Start()
    {
        partyName = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        int totalPeople = System.Convert.ToInt32(
            GameObject.Find("PopField").GetComponent<TMP_InputField>().text
        );
        if (slider.value / (float)totalPeople <= 0.35)
        {
            partyName.rectTransform.localPosition = new Vector3(0f, 12.5f, 0f);
        }
        else
        {
            partyName.rectTransform.localPosition = Vector3.zero;
        }
    }
}

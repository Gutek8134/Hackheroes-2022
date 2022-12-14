using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PartySizer : MonoBehaviour
{
    public int position;
    private Transform handle,
        partyName;
    private Vector3 originalScale,
        originalHandleScale,
        originalPartyNameScale;
    int totalPeople;

    private TMP_InputField inputField;
    private float originalWidth;

    [SerializeField]
    private float width;

    [SerializeField]
    private float offset;

    // Start is called before the first frame update
    void Awake()
    {
        position = PartyManager.sliders.Count;
        handle = GetComponentInChildren<Handle>().transform;
        partyName = transform.GetChild(0).GetChild(0).GetChild(0);
        partyName.GetComponent<TMP_Text>().text = $"Partia {position + 1}";
        inputField = GameObject.Find("PopField").GetComponent<TMP_InputField>();
        originalScale = transform.localScale;
        originalHandleScale = handle.localScale;
        originalPartyNameScale = partyName.localScale;
        originalWidth = width;
    }

    // Update is called once per frame
    void Update()
    {
        if (position > 0)
            Resize();
    }

    void Resize()
    {
        totalPeople = System.Convert.ToInt32(inputField.text);
        int pip = PeopleInParties();
        float scale = (float)(totalPeople - pip) / (float)totalPeople;

        transform.localScale = new Vector3(
            originalScale.x * scale,
            transform.localScale.y,
            transform.localScale.z
        );
        handle.localScale = new Vector3(
            originalHandleScale.x / scale,
            handle.localScale.y,
            handle.localScale.z
        );
        partyName.localScale = new Vector3(
            originalPartyNameScale.x / scale,
            partyName.localScale.y,
            partyName.localScale.z
        );

        UnityEngine.UI.Slider previous = PartyManager.sliders[position - 1];
        width = previous.maxValue / totalPeople * originalWidth;
        transform.localPosition = new Vector3(
            previous.value / previous.maxValue * width
                + offset
                + previous.transform.localPosition.x,
            transform.localPosition.y,
            0f
        );
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

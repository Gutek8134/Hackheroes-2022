using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectionColumn : MonoBehaviour
{
    private static int lastPosition = 0;
    public static float scaleModifier = 1;
    public static float xScale = 1;
    private int position;
    private float ogVotesTextYPosition;

    void Awake()
    {
        position = lastPosition++;
        transform.GetChild(1).GetComponent<TMPro.TMP_Text>().text = $"Partia {position + 1}";
        GetComponentInChildren<UnityEngine.UI.Image>().color = PartyManager.sliderColors[position];
        ogVotesTextYPosition =
            transform.GetChild(2).GetComponent<TMPro.TMP_Text>().rectTransform.localPosition.y;
        Resize();
    }

    public void Resize()
    {
        int totalPeople = System.Convert.ToInt32(
            GameObject.Find("PopField").GetComponent<TMPro.TMP_InputField>().text
        );
        float yScale = (float)BonesButton.votes[position] / (float)totalPeople * scaleModifier;
        transform.localScale = new Vector3(xScale, yScale, 1);
        RectTransform rect = GetComponent<RectTransform>();
        rect.pivot = new Vector2(0.5f, 0);
        rect.localPosition = new Vector3(rect.localPosition.x, -79.7585f, rect.localPosition.z);
        TMPro.TMP_Text[] texts = GetComponentsInChildren<TMPro.TMP_Text>();
        foreach (var text in texts)
            text.gameObject.transform.localScale = new Vector3(1, 1 / yScale, 1);
        TMPro.TMP_Text votesText = transform.GetChild(2).GetComponent<TMPro.TMP_Text>();
        votesText.text = BonesButton.votes[position].ToString();
        votesText.rectTransform.localPosition = new Vector3(
            0,
            1 / yScale * (ogVotesTextYPosition + 70.9f),
            0
        );
    }

    private void OnDestroy()
    {
        lastPosition--;
    }
}

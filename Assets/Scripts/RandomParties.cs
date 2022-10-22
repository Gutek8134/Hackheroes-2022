using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomParties : MonoBehaviour
{
    public void RandomizeParties()
    {
        int totalPeople = System.Convert.ToInt32(
            GameObject.Find("PopField").GetComponent<TMPro.TMP_InputField>().text
        );
        List<Slider> newSliders = new List<Slider>();
        List<float> modifiers = new List<float>();
        while (newSliders.Count <= 5 && Random.value < 0.7f)
        {
            if (newSliders.Count >= PartyManager.sliders.Count)
            {
                PartyManager.sliders[PartyManager.sliders.Count - 1].value *= .9f;
                GameObject.Find("PartyManager").GetComponent<PartyManager>().CreateParty();
            }
            newSliders.Add(PartyManager.sliders[newSliders.Count]);
            modifiers.Add(Random.Range(0.1f, 1f));
        }
        float sum = 0f;
        foreach (float mod in modifiers)
            sum += mod;
        float scale = Mathf.Clamp(1 / sum, float.Epsilon, float.MaxValue);
        for (int i = 0; i < newSliders.Count; ++i)
        {
            if (i > 0)
                newSliders[i].maxValue = totalPeople - PeopleInParties(i);
            newSliders[i].value = totalPeople * modifiers[i];
        }
    }

    int PeopleInParties(int position)
    {
        if (position == 0)
            return 0;
        int sum = 0;
        for (int i = 0; i < position; ++i)
            sum += (int)PartyManager.sliders[i].value;
        return sum;
    }
}

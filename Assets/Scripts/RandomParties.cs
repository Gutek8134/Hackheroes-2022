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
        List<Slider> sliders = new List<Slider>();
        PartyManager partyManager = GameObject.Find("PartyManager").GetComponent<PartyManager>();
        if (sliders.Count < 5)
            do
            {
                if (sliders.Count >= PartyManager.sliders.Count)
                {
                    PartyManager.sliders[PartyManager.sliders.Count - 1].value *= .9f;
                    partyManager.CreateParty();
                }
                sliders.Add(PartyManager.sliders[sliders.Count]);
            } while (sliders.Count <= 5 && Random.value < 0.7f);

        for (int i = 0; i < sliders.Count - 1; ++i)
        {
            sliders[i].maxValue = totalPeople - PeopleInParties(i);
            sliders[i].value = sliders[i].maxValue * Random.value;
        }
        sliders[sliders.Count - 1].value = sliders[sliders.Count - 1].maxValue;
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

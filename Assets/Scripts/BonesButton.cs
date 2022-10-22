using UnityEngine;
using System.Collections.Generic;

public class BonesButton : MonoBehaviour
{
    public static List<int> votes = new List<int>();
    private List<GameObject> columns = new List<GameObject>();

    [SerializeField]
    private GameObject column;

    [SerializeField]
    private UnityEngine.UI.Slider freq;

    private void Awake()
    {
        freq.value = 100;
    }

    private void Update()
    {
        freq.gameObject.GetComponentInChildren<TMPro.TMP_Text>().text = freq.value.ToString();
    }

    public void Clicked()
    {
        int totalPeople = System.Convert.ToInt32(
            GameObject.Find("PopField").GetComponent<TMPro.TMP_InputField>().text
        );
        //Updates population
        int peopleInParties = 0;
        PartyManager.sliders.ForEach(x => peopleInParties += (int)x.value);

        //Resets values
        List<float> modifiers = new List<float>();
        votes = new List<int>();

        //Adds values to lists
        for (int i = 0; i < PartyManager.sliders.Count; ++i)
        {
            votes.Add((int)PartyManager.sliders[i].value);
            modifiers.Add(Random.value);
        }

        //computes scale based on sum of modifiers and frequency
        float sum = 0;
        for (int i = 0; i < modifiers.Count; ++i)
            sum += modifiers[i] * votes[i];
        float scale = freq.value * peopleInParties / 100 / sum;

        //Scales the modifiers
        for (int i = 0; i < modifiers.Count; ++i)
            modifiers[i] *= scale;
        //Scales the initial vote values
        for (int i = 0; i < votes.Count; ++i)
            votes[i] = Mathf.RoundToInt((float)votes[i] * modifiers[i]);
        //Then changes them to match their maximum values
        while (MoreVotesThanVoters())
        {
            for (int i = 0; i < votes.Count; ++i)
            {
                int voters = (int)PartyManager.sliders[i].value;
                if (votes[i] > voters)
                {
                    int diff = votes[i] - voters;
                    votes[i] = voters;
                    if (i == votes.Count - 1)
                        votes[0] += diff;
                    else
                        votes[i + 1] += diff;
                }
            }
        }

        //Gets maximum votes value
        int max = 0;
        votes.ForEach(
            x =>
            {
                if (x > max)
                    max = x;
            }
        );

        //Changes the scale so that the biggest column's size is always the same
        ElectionColumn.scaleModifier = (float)totalPeople / (float)max;

        //Generates positions for columns
        float[] positions = new float[votes.Count];
        ElectionColumn.xScale = votes.Count <= 5 ? 1 : 1 - 0.2f * (votes.Count - 5);
        float position,
            step = 120 * ElectionColumn.xScale;
        if (votes.Count % 2 == 0)
        {
            position = (-70 * votes.Count / 2) * ElectionColumn.xScale;
            for (int i = 0; i < votes.Count; ++i)
            {
                positions[i] = position;
                position += step;
            }
        }
        else
        {
            position = (-120 * (votes.Count - 1) / 2) * ElectionColumn.xScale;
            for (int i = 0; i < votes.Count; ++i)
            {
                positions[i] = position;
                position += step;
            }
        }

        //Instantiates columns
        for (int i = 0; i < votes.Count; ++i)
        {
            if (i >= columns.Count)
                columns.Add(
                    Instantiate(
                        original: column,
                        position: GameObject.Find("Wyniki").transform.position
                            + new Vector3(0, -7.7585f, 0),
                        rotation: transform.rotation,
                        parent: GameObject.Find("Wyniki").transform
                    )
                );
            else
                columns[i].GetComponent<ElectionColumn>().Resize();
            columns[i].transform.localPosition = new Vector3(
                positions[i],
                columns[i].transform.localPosition.y,
                columns[i].transform.localPosition.z
            );
        }
    }

    ///<summ>Determines whether any of the votes values is bigger than its value of slider</summ>
    private bool MoreVotesThanVoters()
    {
        for (int i = 0; i < votes.Count; ++i)
        {
            if (votes[i] > PartyManager.sliders[i].value)
                return true;
        }
        return false;
    }

    public void RandomFreq()
    {
        freq.value = Random.Range(1, 100);
    }
}

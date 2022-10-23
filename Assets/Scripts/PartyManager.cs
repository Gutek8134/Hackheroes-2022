using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    ///<summary>List holding all slider components for parties</summary>
    public static List<Slider> sliders = new List<Slider>();
    public static List<Color> sliderColors = new List<Color>();

    ///<summary>holds prefab for slider managing party voters size</summary>
    [SerializeField]
    private GameObject partyPrefab;

    private Color[] partyColors =
    {
        Color.green,
        Color.blue,
        Color.yellow,
        Color.gray,
        Color.magenta
    };

    // Start is called before the first frame update
    void Start()
    {
        //Since the list is static, every element must be added manually
        PartyManager.sliders.Add(GameObject.Find("Party").GetComponent<Slider>());
        PartyManager.sliderColors.Add(Color.red);
    }

    void Update()
    {
        //Grabs last slider for shortening access path
        Slider lastSlider = PartyManager.sliders[PartyManager.sliders.Count - 1];

        //If any slider reaches its maximum value, every latter is removed
        for (int i = 0; i < PartyManager.sliders.Count; ++i)
        {
            Slider current = PartyManager.sliders[i];
            if (current.value == current.maxValue)
            {
                //Can't use List.RemoveRange, because every object also needs to be destroyed
                for (int j = PartyManager.sliders.Count - 1; j > i; --j)
                {
                    Slider toDelete = PartyManager.sliders[j];
                    PartyManager.sliderColors.RemoveAt(j);
                    PartyManager.sliders.RemoveAt(j);
                    Destroy(toDelete.gameObject);
                }
            }
        }
    }

    public void CreateParty()
    {
        if (sliders.Count >= 5)
            return;
        Slider lastSlider = PartyManager.sliders[PartyManager.sliders.Count - 1];
        //Instantiate new party
        GameObject party = Instantiate(original: partyPrefab, parent: lastSlider.transform.parent);
        Slider partySlider = party.GetComponent<Slider>();
        partySlider.value = partySlider.maxValue;
        Color color = partyColors[sliders.Count];
        party.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = color;
        party.transform.GetChild(1).GetChild(0).GetComponent<Image>().color = color;
        //And add it to the list with its color
        PartyManager.sliders.Add(partySlider);
        PartyManager.sliderColors.Add(color);
    }
}

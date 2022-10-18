using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyManager : MonoBehaviour
{
    ///<summary>List holding all slider components for parties</summary>
    public static List<Slider> sliders = new List<Slider>();

    ///<summary>holds prefab for slider managing party voters size</summary>
    [SerializeField]
    private GameObject partyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Since the list is static, every element must be added manually
        PartyManager.sliders.Add(GameObject.Find("Party").GetComponent<Slider>());
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
                //Can't use List.RemoveRange, because every object also needs to be destroyed as a future-proof feature
                for (int j = PartyManager.sliders.Count - 1; j > i; --j)
                {
                    Slider toDelete = PartyManager.sliders[j];
                    PartyManager.sliders.Remove(toDelete);
                    Destroy(toDelete.gameObject);
                }
            }
        }
    }

    public void CreateParty()
    {
        Slider lastSlider = PartyManager.sliders[PartyManager.sliders.Count - 1];
        //Instantiate new party
        GameObject party = Instantiate(original: partyPrefab, parent: lastSlider.transform.parent);
        //And add it to the list
        Slider partySlider = party.GetComponent<Slider>();
        partySlider.value = partySlider.maxValue;
        PartyManager.sliders.Add(partySlider);
    }
}

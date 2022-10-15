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

    // Update is called once per frame
    void Update()
    {
        //Grabs last slider for shortening access path
        Slider lastSlider = PartyManager.sliders[PartyManager.sliders.Count - 1];
        //If its value is not the maximum value
        if (lastSlider.value < lastSlider.maxValue)
        {
            Debug.Log("Something something");
            //Instantiate new party
            GameObject party = Instantiate(
                original: partyPrefab,
                parent: lastSlider.gameObject.GetComponentInChildren<Handle>().gameObject.transform
            );
            party.transform.localScale = new Vector3(0.54f, 2.38f, 1f);
            PartyManager.sliders.Add(party.GetComponent<Slider>());
        }
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
}

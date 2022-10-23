using UnityEngine;
using TMPro;

public class RandomVoters : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;

    public void RandomizeVoters()
    {
        inputField.text = Random.Range(1000, 1000000).ToString();
    }
}

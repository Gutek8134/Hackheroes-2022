using UnityEngine;

public class BonesButton : MonoBehaviour
{
    [SerializeField]
    private Handle handle;

    public void Clicked()
    {
        Debug.Log(handle.population.text);
    }
}

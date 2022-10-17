using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartySizer : MonoBehaviour
{
    public uint position;

    // Start is called before the first frame update
    void Awake()
    {
        position = (uint)PartyManager.sliders.Count;
    }

    // Update is called once per frame
    void Update() { }
}

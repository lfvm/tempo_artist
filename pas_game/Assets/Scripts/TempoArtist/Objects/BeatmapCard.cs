using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapCard : MonoBehaviour
{
    public GameObject cardTitle { get; set; }
    public GameObject cardArtist { get; set; }

    private void Awake()
    {
        cardTitle = this.transform.GetChild(0).gameObject;
        cardArtist = this.transform.GetChild(1).gameObject;
    }
}

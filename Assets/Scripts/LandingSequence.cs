using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandingSequence : MonoBehaviour
{
    [SerializeField] int numberOfLandings;
    [SerializeField] public int[] landingSequence = new int[7];
    [SerializeField] public GameObject[] landingPads = new GameObject[5];


    void Awake()
    {
        for (int i = 0; i <= landingPads.Length-1; i++)
        {
            //access child obj of landing pad, the sign text
            GameObject signboardRef = landingPads[i].gameObject.transform.GetChild(0).GetChild(0).gameObject;
            //change the number displayed on sign text
            int displayNumber = i + 1;
            signboardRef.GetComponent<TextMesh>().text = displayNumber.ToString();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfLandings = landingSequence.Length;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    CollisionHandler collisionHandlerRef;
    public float totalScore;
    float finalfare;

    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject taxiRef;
    //[SerializeField] float decreaseRate;
    [SerializeField] float startingFare;

    bool timerOn = false;
    bool scoreAdded = false;

    // Start is called before the first frame update
    void Start()
    {
        collisionHandlerRef = taxiRef.GetComponent<CollisionHandler>();
        finalfare = startingFare;
        timerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisionHandlerRef.isColliding) 
        {
            if (!scoreAdded)
            {
                scoreAdded = true;
                timerOn = false;
                totalScore += finalfare;
                finalfare = startingFare;

                double d1 = Math.Round(totalScore, 2);

                scoreText.text = string.Format("Earnings: \n${0}", d1);

            }
            
        }
        else { timerOn = true; scoreAdded = false; }

        if (timerOn)
        {
            if (finalfare > 0)
            {
                scoreAdded = false;
                finalfare -= Time.deltaTime;
                updateTimer(finalfare);
            }
            else
            {
                Debug.Log("Time is UP!");
                finalfare = 0;
                timerOn = false;

            }
        }


    }

    void updateTimer(float currentTime)
    {
        double d1 = Math.Round(currentTime, 2);

        timerText.text = string.Format("${0}", d1);
    }
}

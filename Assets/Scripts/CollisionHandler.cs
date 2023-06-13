using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip[] padPrompts = new AudioClip[5];

    [SerializeField] GameObject parentObject;
    [SerializeField] float delayVal = 1f;

    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] LandingSequence sequenceScript;

    public int currentPadIndex = 0;
    AudioSource audioSource;
    AudioSource promptAudioSource;
    int currentSceneIndex;

    bool isTransitioning = false;
    public bool collisionDisabled;
    bool alreadyPlayed = false;
    public bool isColliding = false;


    void Awake()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = parentObject.GetComponent<AudioSource>();
        promptAudioSource = parentObject.transform.GetChild(1).GetComponent<AudioSource>();


    }

    void Start()
    {
        PlayPadPrompt(currentPadIndex);
    }

    void OnCollisionEnter(Collision other)
    {
        

        if (isTransitioning || collisionDisabled) { return; }
        else
        {
            switch (other.gameObject.tag)
            {
                case "Pad":
                    //getting the required pad number the player needs to land on
                    int requiredPad = sequenceScript.landingSequence[currentPadIndex];

                    if (CheckCorrectOrder(other))
                    {

                        //Debug.Log("landed corectly. move to pad" + requiredPad);
                        StartProgressSequence();                        
                    }
                    else {  }
                    //if no start crash sequence
                    break;
                case "hostile":
                    StartCrashSequence(delayVal);
                    break;
                
            }

        }


    }

    void OnCollisionExit(Collision other)
    {
        isColliding = false;
        alreadyPlayed = false;
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }


    public void LoadNextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);

    }

    void StartCrashSequence(float delay)
    {

        isTransitioning = true;
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        parentObject.GetComponent<MovementScript>().enabled = false;
        Invoke("ReloadLevel", delay);
    }

    void StartProgressSequence()
    {
        currentPadIndex++;
        isColliding = true;
        if (currentPadIndex >= sequenceScript.landingSequence.Length)
        {
            StartSuccessSequence(delayVal);
        }
        else 
        { 
            if (!alreadyPlayed) { PlayPadPrompt(currentPadIndex); alreadyPlayed = true; } 
        }
    }

    void StartSuccessSequence(float delay)
    {
        isTransitioning = true;
        audioSource.PlayOneShot(successSound);
        successParticles.Play();
        parentObject.GetComponent<MovementScript>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }


    bool CheckCorrectOrder(Collision other)
    {
        //getting the required pad number the player needs to land on
        int requiredPad = sequenceScript.landingSequence[currentPadIndex];
        //compare number on sign to number in landing sequence array at index of currentpadindex
        //getting the number on the sign
        GameObject currentPad = other.gameObject.transform.parent.gameObject;
        string numText = currentPad.transform.GetChild(0).GetChild(0).gameObject.GetComponent<TextMesh>().text;
        int padNum = int.Parse(numText);

        //comparing number on sign and number of target landing pad
        if (padNum == requiredPad)
        {
            return true;
        }
        else { return false; }
    }

    void PlayPadPrompt(int currentRequiredIndex)
    {

        int indexNum = sequenceScript.landingSequence[currentRequiredIndex];
        AudioClip targetPadPrompt = padPrompts[indexNum - 1];   
        promptAudioSource.PlayOneShot(targetPadPrompt);

    }

}

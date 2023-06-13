using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;
    [SerializeField] [Range(0,1)] float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        //Debug.Log("the start position is" + startPos);
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon){return;}
        else
        {
            //define sin value from -1 to 1 per frame
            float cycles = Time.time / period;
            const float tau = Mathf.PI * 2;
            float rawSinWave = Mathf.Sin(cycles * tau);

            //keeping the sin value between 0 and one
            movementFactor = (rawSinWave + 1f) / 2;

            //applying the movement to the game object
            Vector3 offset = movementVector * movementFactor;
            transform.position = startPos + offset;
        }
    }
}

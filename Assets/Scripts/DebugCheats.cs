using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCheats : MonoBehaviour
{
    [SerializeField] CollisionHandler collisionHandlerRef;

    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            collisionHandlerRef.collisionDisabled = !collisionHandlerRef.collisionDisabled; //toggle variable state

            
        }
        else if (Input.GetKey(KeyCode.L))
        {
            collisionHandlerRef.LoadNextLevel();
        }
    }

}

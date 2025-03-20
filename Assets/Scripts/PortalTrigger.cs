using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    [SerializeField] LevelLoad levelLoadScript;
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            levelLoadScript.LoadNextLevel();
        }
    }
}

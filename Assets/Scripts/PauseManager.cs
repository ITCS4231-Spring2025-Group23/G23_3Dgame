using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    [SerializeField] Button resume_button;
    [SerializeField] Button exit_button;
    public bool isPaused = false;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PauseGame() {
        Time.timeScale = 1.0f;
        isPaused = false;
    
        //Connect with InputManager to change action map back to player controls
    }

    public void UnPauseGame() {
        Time.timeScale = 0.0f;
        isPaused = true;

        //Connect with InputManager to change action map back to UI
    }

}

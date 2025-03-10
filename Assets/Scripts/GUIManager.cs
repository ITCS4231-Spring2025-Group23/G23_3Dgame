using UnityEngine;
using UnityEngine.UI;

//Controls which GUI elements are showing
public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    [SerializeField] GameObject pause_menu;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        pause_menu.SetActive(false);
    }

    void Update() {
        //Check for input
        if (Input.GetKeyDown(KeyCode.P)) {
            if (!PauseManager.isPaused) {
                PauseManager.instance.PauseGame();
            }
            else {
                PauseManager.instance.UnPauseGame();
            }
        }

        
    }
}

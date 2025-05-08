using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

//Controls which GUI elements are showing
public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;
    [SerializeField] GameObject pause_menu;
    // [SerializeField] Animator transition;

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

        if (SceneManager.GetActiveScene().name == "Ending") {
            Destroy(gameObject);
            Destroy(PlayerMovement.instance.gameObject);
        }
    }

    // public void LoadNextLevel() {
    //     StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    // }

    // IEnumerator LoadLevel(int levelIndex) {
    //     transition.SetTrigger("Start");

    //     yield return new WaitForSeconds(1);

    //     SceneManager.LoadScene(levelIndex);
    // }
}

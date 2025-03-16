using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;
    PlayerMovement playerMovementScript;
    [SerializeField] GameObject player;
    [SerializeField] Button resume_button;
    [SerializeField] Button exit_button;
    public static bool isPaused = false;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        playerMovementScript = player.GetComponent<PlayerMovement>();
    }

    //Might be able to get rid of changing "canMove"
    public void PauseGame() {
        isPaused = true;
        gameObject.SetActive(true);
        PlayerMovement.canMove = false;
        playerMovementScript.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnPauseGame() {
        isPaused = false;
        gameObject.SetActive(false);
        PlayerMovement.canMove = true;
        playerMovementScript.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Might change to return to main menu
    public void OnExit() {
        Debug.Log("Exit");
        Application.Quit();
    }

}

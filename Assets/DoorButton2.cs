using UnityEngine;
using TMPro;

public class DoorButton2 : MonoBehaviour
{
    public GameObject door;
    public Vector3 openPosition;
    public float speed = 2f;
    public float interactRange = 3f;
    public TextMeshProUGUI interactPrompt;

    private GameObject player;
    private bool open = false;
    private bool pressed = false;
    private bool inRange = false;

    private Vector3 originalButtonPos;
    private Vector3 pressedButtonPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        originalButtonPos = transform.position;
        pressedButtonPos = originalButtonPos + new Vector3(0, -0.1f, 0);

        if (door != null && openPosition == Vector3.zero)
            openPosition = door.transform.position + new Vector3(0, 5f, 0);

        if (interactPrompt != null)
            interactPrompt.enabled = false;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.transform.position);
        inRange = distance <= interactRange;

        if (interactPrompt != null)
            interactPrompt.enabled = inRange && !pressed;

        if (inRange && Input.GetKeyDown(KeyCode.E) && !pressed)
        {
            open = true;
            pressed = true;
        }

        if (open && door != null)
        {
            door.transform.position = Vector3.MoveTowards(
                door.transform.position,
                openPosition,
                speed * Time.deltaTime
            );
        }

        if (pressed)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                pressedButtonPos,
                speed * Time.deltaTime
            );
        }
    }
}

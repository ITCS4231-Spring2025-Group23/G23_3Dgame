using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

//Locks an object's rotation to a specified clampValue, rotation not tied to transform.rotation, rotation is accumulated
public class RotateObject : MonoBehaviour, IInteractable
{
    public Vector2 trackedRotation = Vector2.zero;
    [SerializeField] float clampValue = 135f;
    public string interact_text = "Rotate Object with 'J' and 'K'";
    private float originalRot;

    void Start() {
        if (transform.eulerAngles.y <= 180f) {
            originalRot = transform.eulerAngles.y;
        }
        else {
            originalRot = transform.eulerAngles.y - 360f;
        }
    }

    public void Interact() {
        if (Input.GetKey(KeyCode.J)) {
            //Precise Rotation Control
            if (Input.GetKey(KeyCode.RightShift)) {
                trackedRotation.y -= 0.05f;
            }
            else {
                trackedRotation.y -= 0.25f;
            }
        }
        else if (Input.GetKey(KeyCode.K)) {
            if (Input.GetKey(KeyCode.RightShift)) {
                trackedRotation.y += 0.05f;
            }
            else {
                trackedRotation.y += 0.25f;
            }
        }
        trackedRotation.y = Mathf.Clamp(trackedRotation.y, -clampValue, clampValue);
        transform.rotation = Quaternion.Euler(0f, originalRot + trackedRotation.y, 0f);
    }

    public string GetInteractionText() {
        return interact_text;
    }
}

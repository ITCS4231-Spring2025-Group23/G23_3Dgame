using UnityEngine;
using UnityEngine.Animations;

//Locks an object's rotation to a specified clampValue, rotation not tied to transform.rotation, rotation is accumulated
public class RotateObject : MonoBehaviour, IInteractable
{
    public Vector2 trackedRotation = Vector2.zero;
    [SerializeField] float clampValue = 135f;
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
        transform.rotation = Quaternion.Euler(0f, trackedRotation.y, 0f);
    }
}

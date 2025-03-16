using Unity.VisualScripting;
using UnityEngine;

public class PushBox : MonoBehaviour, IInteractable
{
    private GameObject interactable;
    private Rigidbody interactableRb;
    private bool itemAttached;

    void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.moveDirection.y > -0.3f) {
            interactableRb = hit.collider.attachedRigidbody;
            interactable = hit.gameObject;
            if (interactableRb != null && !itemAttached) {
                itemAttached = true;
                interactableRb.isKinematic = true;
                interactable.transform.SetParent(transform);
                interactable.transform.position += new Vector3(0,0,0.5f);
            }
        }
    }

    //Add method to drop box
    public void Interact() {
        if (interactableRb != null && itemAttached) {
            Debug.Log("DROPPED");
            itemAttached = false;
            interactable.transform.SetParent(null);
            interactableRb.isKinematic = false;
        }
    }
}

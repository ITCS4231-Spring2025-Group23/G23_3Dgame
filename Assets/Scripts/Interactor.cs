using System;
using Unity.VisualScripting;
using UnityEngine;

interface IInteractable {
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public GameObject interactionText;
    private float InteractRange = 3.0f;
    public LayerMask interactableLayer;

    // Update is called once per frame
    void Update()
    {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K)) {
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    interactObj.Interact();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            // Vector3 endPoint = InteractorSource.position + InteractorSource.forward * InteractRange;
            // Debug.DrawLine(InteractorSource.position, endPoint);
            if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer)) {
                if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                    interactObj.Interact();
                }
            }
        }
    }
}

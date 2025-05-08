using System;
using Unity.VisualScripting;
using UnityEngine;

//Each class inherits Interact(), is multiple functionality possible?
interface IInteractable {
    public void Interact();
    public string GetInteractionText();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public GameObject interactionText;
    private float InteractRange = 3.0f;
    public LayerMask interactableLayer;

    // Update is called once per frame
    void FixedUpdate() {
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
       
        if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer)) {
            CursorManager.instance.CursorSelect();
            
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
                CursorManager.instance.displayText(interactObj.GetInteractionText());
                //Rotate Object
                if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K)) {
                    interactObj.Interact();
                }

                //Pick Up Object
                if (Input.GetKeyDown(KeyCode.E)) {
                    interactObj.Interact();
                }
            }
        }
        else {
            CursorManager.instance.CursorDeselect();
            CursorManager.instance.displayText("");
        }
      
        // if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K)) {
        //     if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer)) {
        //         if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
        //             interactObj.Interact();
        //         }
        //     }
        // }

        // if (Input.GetKeyDown(KeyCode.E)) {
        //     // Vector3 endPoint = InteractorSource.position + InteractorSource.forward * InteractRange;
        //     // Debug.DrawLine(InteractorSource.position, endPoint);
        //     if (Physics.Raycast(r, out RaycastHit hitInfo, InteractRange, interactableLayer)) {
        //         if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)) {
        //             interactObj.Interact();
        //         }
        //     }
        // }
    }
}

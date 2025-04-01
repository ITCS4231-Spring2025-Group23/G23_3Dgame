using System;
using Unity.VisualScripting;
using UnityEngine;

public class PushBox1 : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject player;
    Rigidbody rb;
    public string interact_text = "Press E to Hold Object";
    private bool itemAttached = false;
    private float pickUpDistance = 2.0f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void Interact() {
        if (itemAttached) {
            itemAttached = false;
            rb.isKinematic = false;
            transform.SetParent(null);
        }
        else {
            Vector3 playerPos = player.transform.position;
            Vector3 playerDir = player.transform.forward;
            Quaternion playerRotation = player.transform.rotation;
            itemAttached = true;
            rb.isKinematic = true;
            transform.SetParent(player.transform);
            transform.rotation = playerRotation;
            transform.position = Vector3.Lerp(transform.position, playerPos + playerDir*pickUpDistance, 5f * Time.deltaTime);
            // transform.position = playerPos + playerDir*pickUpDistance;
        }
    }

    public string GetInteractionText() {
        if (itemAttached) {
            return "Press E to Drop Object";
        }
        return interact_text;
    }
}

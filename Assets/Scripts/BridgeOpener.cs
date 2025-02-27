using UnityEngine;

public class BridgeOpener : MonoBehaviour
{
    Animator bridgeAnimator;
    bool pressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bridgeAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mirror")) {
            bridgeAnimator.SetTrigger("Open");
            // pressed = true;
            Debug.Log("Opened");
        }
    }

    void OnTriggerExit(Collider other)
    {
        bridgeAnimator.SetTrigger("Closed");
        Debug.Log("Closed");
        // pressed = false;
    }
}

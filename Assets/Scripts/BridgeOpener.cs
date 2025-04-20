using UnityEngine;

public class BridgeOpener : MonoBehaviour
{
    Animator bridgeAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bridgeAnimator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mirror") || other.CompareTag("Refract")) {
            bridgeAnimator.SetTrigger("Open");
            Debug.Log("Open");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Mirror") || other.CompareTag("Refract")) {
            bridgeAnimator.SetTrigger("Closed");
            Debug.Log("Closed");
        }
    }
}

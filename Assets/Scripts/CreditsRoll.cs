using UnityEngine;

public class CreditsRoll : MonoBehaviour
{
    public float scrollSpped = 40f;
    private RectTransform rectTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.Translate(Vector3.up * Time.deltaTime * scrollSpped);
    }
}

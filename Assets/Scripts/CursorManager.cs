using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public static CursorManager instance;
    [SerializeField] Image circle_cursor;
    [SerializeField] TMP_Text interaction_text;
    private float transparent = 0.5f;
    private float opaque = 1f;
    
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void CursorSelect() {
        Color full_color = circle_cursor.color;
        full_color.a = opaque;
        circle_cursor.color = full_color;
    }

    public void CursorDeselect() {
        Color dim_color = circle_cursor.color;
        dim_color.a = transparent;
        circle_cursor.color = dim_color;
    }

    public void displayText(string interact_text) {
        interaction_text.text = interact_text;
    }
}

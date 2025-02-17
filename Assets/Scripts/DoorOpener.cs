using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.Rendering.Universal;

public class SwitchOpener : MonoBehaviour
{
    float opening_speed = 2;
    [SerializeField] GameObject door;

    // Update is called once per frame
    void Update()
    {
        if (LaserBeam.door_active && door.transform.position.y <= 12) {
            door.transform.Translate(Vector3.up * Time.deltaTime * opening_speed);
        }
        else if (!LaserBeam.door_active && door.transform.position.y >= 4) {
            door.transform.Translate(Vector3.down * Time.deltaTime * opening_speed);
        }

    }
}

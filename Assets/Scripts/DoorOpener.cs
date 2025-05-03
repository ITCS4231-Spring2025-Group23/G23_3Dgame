using System;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOpener : MonoBehaviour
{
    float opening_speed = 2;
    [SerializeField] GameObject door;
    [SerializeField] Material activeMaterial;
    [SerializeField] Material inactiveMaterial;
    MeshRenderer renderer;
    public int switchID;
    public static List<GameObject> activatedSwitches = new List<GameObject>();


    void Start() {
        renderer = GetComponent<MeshRenderer>();
        if (renderer != null) {
            inactiveMaterial = renderer.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activatedSwitches.Count == 2 && door.transform.position.y <= 12) {
            door.transform.Translate(Vector3.up * Time.deltaTime * opening_speed);
        }
        else if (activatedSwitches.Count < 2 && door.transform.position.y > 4.5) {
            Debug.Log(door.transform.position.y);
            door.transform.Translate(Vector3.down * Time.deltaTime * opening_speed);
        }
    }

    public void activateSwitch(GameObject switchOpener) {
        if (switchOpener == gameObject) {
            for (int i = 0; i < activatedSwitches.Count; i++) {
                if (activatedSwitches[i] == switchOpener) {
                    return;
                }
            }
            renderer.material = activeMaterial;
            activatedSwitches.Add(switchOpener);
        }
    }

    public static void deactivateSwitch() {
        for (int i = 0; i < activatedSwitches.Count; i++) {
            activatedSwitches.Remove(activatedSwitches[i]);
        }
    }
}

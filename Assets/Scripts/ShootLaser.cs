using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Material material;
    [SerializeField] int laserPointerID;
    LaserBeam beam;

    // Update is called once per frame
    void Update()
    {
        if (beam != null) {
            Destroy(beam.laserObj);
        }
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material, laserPointerID);
    }
}

using UnityEngine;

public class Laser : MonoBehaviour
{
    public Material material;
    LaserBeam beam;
    
    // Update is called once per frame
    void Update()
    {
        if (beam != null) {
            Destroy(beam.laserObj);
        }
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.forward, material);
    }
}

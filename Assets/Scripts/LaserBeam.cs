using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using System.Numerics;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

public class LaserBeam
{
    Vector3 pos, dir;
    public GameObject laserObj;
    int laserPointerID;
    LineRenderer laser;
    List<Vector3> laserIndices = new List<Vector3>();
    SwitchOpener door_script;
    Dictionary<string, float> refractiveMaterials = new Dictionary<string, float>() {
        {"Air", 1.0f},
        {"Glass", 1.5f}
    };
    public LayerMask laserLayer = 1 << LayerMask.NameToLayer("Interactable") | 1 << LayerMask.NameToLayer("Default");

    public LaserBeam(Vector3 pos, Vector3 dir, Material material, int laserPointerID) {
        this.laserPointerID = laserPointerID;
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = 0.1f;
        this.laser.endWidth = 0.1f;
        this.laser.material = material;
        this.laser.startColor = Color.yellow;   
        this.laser.endColor = Color.yellow;

        CastRay(pos, dir, laser);
    }

    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser) {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50, laserLayer)) {
            CheckHit(hit, dir, laser);
        }
        else {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser() {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in  laserIndices) {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser) {
        if (hitInfo.collider.CompareTag("Mirror")) {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
            CastRay(pos, dir, laser);
        }
        else if (hitInfo.collider.CompareTag("Refract")) {
            Vector3 pos = hitInfo.point;
            laserIndices.Add(pos);

            Vector3 newPos1 = new Vector3(Mathf.Abs(direction.x) / (direction.x + 0.0001f) * 0.0001f + pos.x, Mathf.Abs(direction.y)/ (direction.y + 0.0001f) * 0.0001f + pos.y, Mathf.Abs(direction.z)/ (direction.z + 0.0001f) * 0.0001f + pos.z);

            float n1 = refractiveMaterials["Air"];
            float n2 = refractiveMaterials["Glass"];

            Vector3 norm = hitInfo.normal;
            Vector3 incident = direction;

            Vector3 refractedVector = Refract(n1, n2, norm, incident);

            // CastRay(newPos1, refractedVector, laser);

            //Exit Refraction
            
            Ray ray1 = new Ray(newPos1, refractedVector);
            Vector3 newRayStartPos = ray1.GetPoint(1.5f);

            Ray ray2 = new Ray(newRayStartPos, -refractedVector);
            RaycastHit hit2;

            if (Physics.Raycast(ray2, out hit2, 1.6f, laserLayer)) {
                laserIndices.Add(hit2.point);
            }

            UpdateLaser();

            Vector3 refractedVector2 = Refract(n2, n1, -hit2.normal, refractedVector);
            CastRay(hit2.point, refractedVector2, laser);
        }
        else if (hitInfo.collider.CompareTag("DoorSwitch")) {
            laserIndices.Add(hitInfo.point);
            door_script = hitInfo.collider.gameObject.GetComponent<SwitchOpener>();
            if (laserPointerID == door_script.switchID) {
                door_script.activateSwitch(hitInfo.collider.gameObject);
            }
            UpdateLaser();
        }
        else {
            laserIndices.Add(hitInfo.point);
            SwitchOpener.deactivateSwitch();
            UpdateLaser();
        }
    }

    Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident) {
        incident.Normalize();

        Vector3 refractedVector = (n1/n2 * Vector3.Cross(norm, Vector3.Cross(-norm, incident)) - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (n1/n2 * n1/n2), Vector3.Cross(norm, incident)))).normalized;
        return refractedVector;
    }
}

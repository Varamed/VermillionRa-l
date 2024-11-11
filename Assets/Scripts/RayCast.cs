using UnityEngine;

public class RayCast : MonoBehaviour
{
    private void Update()
    {
        RayCastGenerator();
    }
    // Update is called once per frame
    public void RayCastGenerator()
    {
        Vector3 origin = transform.position;

        Vector3 direction = transform.forward;

        float maxDistance = 5f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            Debug.Log("Impacto con: " + hit.collider.name);
        }

        Debug.DrawRay(origin, direction * maxDistance, Color.red);
    }
}

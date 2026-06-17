using UnityEngine;

public class ProximityOutline : MonoBehaviour
{
    public Transform player;
    public float distanceToShow = 3f;

    private Outline outline;
    private Collider objectCollider;

    void Start()
    {
        outline = GetComponent<Outline>();
        objectCollider = GetComponent<Collider>();

        if (outline != null)
            outline.enabled = false;
    }

    void Update()
    {
        if (player == null || outline == null || objectCollider == null)
            return;

        Vector3 closestPoint = objectCollider.ClosestPoint(player.position);
        float distance = Vector3.Distance(player.position, closestPoint);

        outline.enabled = distance <= distanceToShow;
    }
}
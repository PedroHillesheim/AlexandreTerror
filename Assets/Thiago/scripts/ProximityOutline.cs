using UnityEngine;

public class ProximityOutline : MonoBehaviour
{
    public Transform player;
    public float distanceToShow = 3f;

    private Outline outline;

    void Start()
    {
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;
    }

    void Update()
    {
        if (player == null || outline == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        Debug.Log(gameObject.name + " Distância: " + distance);

        outline.enabled = distance <= distanceToShow;
    }
}
using UnityEngine;

public class LookOutline : MonoBehaviour
{
    public float distance = 3f;

    private Outline currentOutline;

    void Update()
    {
        // Remove outline do objeto anterior
        if (currentOutline != null)
        {
            currentOutline.enabled = false;
            currentOutline = null;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Outline outline = hit.collider.GetComponent<Outline>();

                if (outline != null)
                {
                    outline.enabled = true;
                    currentOutline = outline;
                }
            }
        }
    }
}
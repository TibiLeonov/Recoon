using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHighlighter : MonoBehaviour
{

    public Color highLightColour = Color.white;

    public Vector3 pickUpOffset = new Vector3(0, 1, 1);
    public float pickUpRadius = 2;
    public LayerMask pickUpLayer = 9;

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void LateUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(pickUpOffset.x * transform.right + pickUpOffset.y * transform.up + pickUpOffset.z * transform.forward + transform.position, pickUpRadius, pickUpLayer);
        foreach (Collider collider in hitColliders)
        {
            Highlightable highlightableScript = collider.GetComponentInParent<Highlightable>();
            if (highlightableScript != null)
            {
                if (highlightableScript.highlighted == false)
                {
                    highlightableScript.Highlight(highLightColour);
                    return;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(pickUpOffset.x * transform.right + pickUpOffset.y * transform.up + pickUpOffset.z * transform.forward + transform.position, pickUpRadius);
    }
}

using UnityEngine;
using System.Collections;

using SWS;

//[RequireComponent(typeof(Collider))]
public class NavGraphEdgeTrigger : MonoBehaviour
{
    // Reference to path
    public NavGraphEdgeMovement EdgeMovement;
    public bool ReverseMovement = false;
    public bool Enabled = true;

    public Material InactiveMaterial;
    public Material GazedAtMaterial;

    //private float rotationSpeed;

    void Start()
    {
        SetGazedAt(false);
    }

    void LateUpdate()
    {
        GvrViewer.Instance.UpdateState();
        if (GvrViewer.Instance.BackButtonPressed)
        {
            Application.Quit();
        }
    }

    public void SetGazedAt(bool gazedAt)
    {
        Transform cube = transform.FindChild("Cube");
        if (InactiveMaterial != null && GazedAtMaterial != null)
        {
            cube.GetComponent<Renderer>().material = gazedAt ? GazedAtMaterial : InactiveMaterial;
            return;
        }
        cube.GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
    }

    public void Hide()
    {
        SetGazedAt(false);
        gameObject.SetActive(false);
    }

    public void UnHide()
    {
        if(!Enabled)
            return;

        gameObject.SetActive(true);
        SetGazedAt(false);
    }

    public void Enable()
    {
        Enabled = true;

        // If the parent node is currently the active one, reveal the trigger
        if (NavGraphManager.instance.ActiveNode == transform.parent)
            UnHide();
    }

    public void Disable()
    {
        Hide();

        SetGazedAt(false);
        Enabled = false;            
    }

    // Trigger movement function here
    public void StartMovement(splineMove mover)
    {
        EdgeMovement.StartMovement(mover, ReverseMovement);
    }

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter()
    {
        if (!Enabled)
            return;

        SetGazedAt(true);
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
        if (!Enabled)
            return;

        SetGazedAt(false);
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnPointerExit.
    public void OnGazeTrigger()
    {
        if (!Enabled)
            return;

        StartMovement(NavGraphManager.instance.mover); 
    }
}

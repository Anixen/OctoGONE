using UnityEngine;
using System.Collections;

using SWS;

[RequireComponent(typeof(Collider))]
public class NavGraphEdgeTrigger : MonoBehaviour, IGvrGazeResponder
{
    // Reference to path
    public PathManager pathContainer;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

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
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
            return;
        }
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
    }

    // TODO Trigger movement function here
    public void StartMovement(splineMove movement)
    {
        movement.pathContainer = this.pathContainer;
        movement.reverse = false;
        movement.StartMove();
    }

    public void StartMovement_Reverse(splineMove movement)
    {
        movement.pathContainer = this.pathContainer;
        movement.reverse = true;
        movement.StartMove();
    }

    #region IGvrGazeResponder implementation

    /// Called when the user is looking on a GameObject with this script,
    /// as long as it is set to an appropriate layer (see GvrGaze).
    public void OnGazeEnter()
    {
        SetGazedAt(true);
    }

    /// Called when the user stops looking on the GameObject, after OnGazeEnter
    /// was already called.
    public void OnGazeExit()
    {
        SetGazedAt(false);
    }

    /// Called when the viewer's trigger is used, between OnGazeEnter and OnPointerExit.
    public void OnGazeTrigger()
    {
        //TODO Trigger movement
    }

    #endregion
}

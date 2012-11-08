using UnityEngine;
using System.Collections;

public class TPCamera : MonoBehaviour {

    public GameObject Target;
    public float MovementSpeed = 3.0f;
    public float RotationSpeed = 0.5f;

    public float DistanceFromTarget = 5.0f;
    public float HeightAboveTarget = 3.0f;

    //// Private Member Variables /////

    private Transform m_targetTransform;
    private Transform m_cameraTransform;

    private float m_desiredRotation;
    private float m_currentRotation;

    private float m_desiredHeight;
    private float m_currentHeight;

	// Use this for initialization
	void Start () {
        if (Target == null)
            Target = this.gameObject;

        m_targetTransform = Target.transform;
        m_cameraTransform = this.transform;
	}

    void LateUpdate()
    {
        if (!Target)
            return;

        m_desiredRotation = m_targetTransform.eulerAngles.y;
        m_desiredHeight = m_targetTransform.position.y + HeightAboveTarget;

        m_currentRotation = Mathf.LerpAngle(m_currentRotation, m_desiredRotation, RotationSpeed * Time.deltaTime);
        m_currentHeight = Mathf.Lerp(m_currentHeight, m_desiredHeight, MovementSpeed * Time.deltaTime);

        m_cameraTransform.position = m_targetTransform.position;
        m_cameraTransform.position -= Quaternion.Euler(0, m_currentRotation, 0) * Vector3.forward * DistanceFromTarget;
        m_cameraTransform.position = new Vector3(m_cameraTransform.position.x, m_currentHeight, m_cameraTransform.position.z);

        //m_cameraTransform.LookAt(m_targetTransform);
    }
}

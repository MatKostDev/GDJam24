using UnityEngine;

public class SpinningObject : MonoBehaviour
{
    [SerializeField]
    float spinForce = 20f;

    Rigidbody m_rigidBody;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        m_rigidBody.MoveRotation(m_rigidBody.rotation * Quaternion.Euler(-spinForce * Time.fixedDeltaTime, 0f, 0f));
    }
}

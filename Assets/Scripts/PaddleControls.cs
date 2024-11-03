using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    [Header("Paddle Movement Range")]
    [SerializeField]
    float paddleDynamicSideOffset = 0.6f;

    [SerializeField]
    float paddleDynamicHeightOffset = 0.25f;

    [Header("Forces")]
    [SerializeField]
    float paddleForwardForceMulti = 70f;

    [SerializeField]
    float paddleUpwardForceMulti = 50f;

    [SerializeField]
    float flipCanoeForceMulti = 400f;

    [Header("Transforms")]
    [SerializeField]
    Transform canoeTransform;

    [SerializeField]
    Transform paddleContactTransform;

    [Header("Paddle Collision")]
    [SerializeField]
    PaddleCollision paddleCollision;

    [Header("Paddle Visuals")]
    [SerializeField]
    Transform paddleVisualTransform;

    [Header("Effects")]
    [SerializeField]
    GameObject paddleCollisionParticle;

    float m_paddleStaticHeightOffset;

    Vector2 m_lastRelativeScreenPos;

    Vector3    m_visualTargetPos;
    Quaternion m_visualTargetRot;
    
    Vector3    m_visualStartOffsetPos;
    Quaternion m_visualStartOffsetRot;

    void Start()
    {
        m_visualTargetPos = paddleVisualTransform.localPosition;
        m_visualTargetRot = paddleVisualTransform.localRotation;

        m_visualStartOffsetPos = paddleVisualTransform.localPosition;
        m_visualStartOffsetRot = paddleVisualTransform.localRotation;

        m_paddleStaticHeightOffset = transform.localPosition.y;

        m_lastRelativeScreenPos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
    }

    void Update()
    {
        bool    flipCanoe  = Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space);
        bool    clickFrame = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
        bool    clickHeld  = Input.GetMouseButton(0)     || Input.GetMouseButton(1);
        Vector3 mousePos   = Input.mousePosition;

        Vector2 relativeScreenPos = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);
        relativeScreenPos.x = Mathf.Clamp(relativeScreenPos.x * 2f - 1f, -1f, 1f);
        relativeScreenPos.y = Mathf.Clamp((relativeScreenPos.y * 2f - 1f) * 2f, -1f, 1f);

        int sideMulti   = relativeScreenPos.x > 0f ? 1 : -1;
        int heightMulti = clickHeld ? -1 : 1;

        var canoePos = canoeTransform.position;

        Vector3 newPhysicsPos = transform.position;
        newPhysicsPos.x = paddleDynamicSideOffset * sideMulti;
        transform.position = newPhysicsPos;

        newPhysicsPos = canoePos;
        newPhysicsPos += canoeTransform.right   * (relativeScreenPos.x * 0.2f + (paddleDynamicSideOffset * sideMulti));
        newPhysicsPos += canoeTransform.up      * (paddleDynamicHeightOffset * heightMulti + m_paddleStaticHeightOffset);
        newPhysicsPos += canoeTransform.forward * relativeScreenPos.y * 1.2f;

        float paddleSpeed = m_lastRelativeScreenPos.y - relativeScreenPos.y;

        transform.position = newPhysicsPos;

        Vector3 newVisualTargetPos = m_visualStartOffsetPos;
        newVisualTargetPos.x *= sideMulti;
        newVisualTargetPos.x += relativeScreenPos.x * 0.25f;
        newVisualTargetPos.y += 0.25f * heightMulti;
        newVisualTargetPos.z += relativeScreenPos.y * 0.4f;

        paddleVisualTransform.localPosition = Vector3.Lerp(paddleVisualTransform.localPosition, newVisualTargetPos, 20f * Time.deltaTime);

        Vector3 newVisualTargetEulers = m_visualStartOffsetRot.eulerAngles;
        newVisualTargetEulers.x += sideMulti == -1 ? 90f : 0f;
        newVisualTargetEulers.y += -relativeScreenPos.y * 50f * sideMulti;

        paddleVisualTransform.localRotation = Quaternion.Slerp(paddleVisualTransform.localRotation, Quaternion.Euler(newVisualTargetEulers), 20f * Time.deltaTime);

        if (paddleCollision.IsCollision)
        {
            if (clickHeld)
            {
                Vector3 forwardForce = paddleForwardForceMulti * paddleSpeed * canoeTransform.forward;

                canoeTransform.GetComponent<Rigidbody>().AddForceAtPosition(forwardForce, paddleContactTransform.position);

                GameObject.Instantiate(paddleCollisionParticle, paddleContactTransform.position, Quaternion.identity);
            }
            if (clickFrame)
            {
                Vector3 upwardForce = canoeTransform.up * paddleUpwardForceMulti;

                Vector3 forceOrigin = canoePos + canoeTransform.forward * transform.localPosition.z;

                canoeTransform.GetComponent<Rigidbody>().AddForceAtPosition(upwardForce, forceOrigin);
            }
        }

        if (flipCanoe)
        {
            Vector3 flipForce = Vector3.up * flipCanoeForceMulti;

            Vector3 forceOrigin = canoePos + canoeTransform.right * transform.localPosition.x;

            canoeTransform.GetComponent<Rigidbody>().AddForceAtPosition(flipForce, forceOrigin);
        }

        m_lastRelativeScreenPos = relativeScreenPos;
    }
}

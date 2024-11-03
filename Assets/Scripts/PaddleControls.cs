using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    [SerializeField]
    float paddleDynamicSideOffset = 0.6f;

    [SerializeField]
    float paddleDynamicHeightOffset = 0.25f;

    [SerializeField]
    Transform canoeTransform;

    float m_paddleStaticHeightOffset;

    void Start()
    {
        m_paddleStaticHeightOffset = transform.localPosition.y;
    }

    void Update()
    {
        bool    clickHeld = Input.GetMouseButton(0) || Input.GetMouseButton(1);
        Vector3 mousePos  = Input.mousePosition;

        Vector2 relativeScreenPos = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);
        relativeScreenPos.x = relativeScreenPos.x * 2f - 1f;
        relativeScreenPos.y = relativeScreenPos.y * 2f - 1f;

        int sideMulti   = relativeScreenPos.x > 0f ? 1 : -1;
        int heightMulti = clickHeld ? -1 : 1;

        var canoePos = canoeTransform.position;

        Vector3 newPos = transform.position;
        newPos.x = paddleDynamicSideOffset * sideMulti;
        transform.position = newPos;

        newPos = canoePos;
        newPos += canoeTransform.right   * (relativeScreenPos.x * 0.2f + (paddleDynamicSideOffset * sideMulti));
        newPos += canoeTransform.up      * (paddleDynamicHeightOffset * heightMulti + m_paddleStaticHeightOffset);
        newPos += canoeTransform.forward * relativeScreenPos.y * 1.2f;

        float deltaZ = transform.position.z - newPos.z;

        transform.position = newPos;

        if (clickHeld)
        {
            canoeTransform.GetComponent<Rigidbody>().AddForceAtPosition(canoeTransform.forward * deltaZ * Time.deltaTime * 30000f, transform.position - new Vector3(0f, -0.5f, 0f));
        }
    }
}

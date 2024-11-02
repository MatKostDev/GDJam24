using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    [SerializeField]
    float paddleSideOffset = 1.5f;

    [SerializeField]
    float paddleHeightOffset = 0.35f;

    [SerializeField]
    Transform canoeTransform;

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
        newPos.x = paddleSideOffset * sideMulti;
        transform.position = newPos;

        newPos = canoePos;
        newPos += canoeTransform.right   * (relativeScreenPos.x * 0.2f + (paddleSideOffset * sideMulti));
        newPos += -canoeTransform.forward * paddleHeightOffset * heightMulti;
        newPos += canoeTransform.up      * relativeScreenPos.y * 1.2f;

        float deltaZ = transform.position.z - newPos.z;

        transform.position = newPos;

        if (clickHeld)
        {
            canoeTransform.GetComponent<Rigidbody>().AddForceAtPosition(transform.forward * deltaZ * Time.deltaTime * 30000f, transform.position - new Vector3(0f, -0.5f, 0f));
        }
    }
}

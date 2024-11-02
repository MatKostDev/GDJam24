using UnityEngine;

public class PaddleControls : MonoBehaviour
{
    [SerializeField]
    float paddleSideOffset = 5f;

    private void Update()
    {
        bool    clickHeld = Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1);
        Vector2 mousePos  = Input.mousePosition;

        Vector2 relativeScreenPos = new Vector2(mousePos.x / Screen.width, mousePos.y / Screen.height);

        int sideMulti = 1;
        if (relativeScreenPos.x < 0f)
        {
            sideMulti = -1;
        }

        Vector3 newLocalPos = transform.localPosition;
        newLocalPos.x = paddleSideOffset * sideMulti;
        transform.localPosition = newLocalPos;
    }
}

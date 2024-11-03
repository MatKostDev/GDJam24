using UnityEngine;

public class RestartPositionScript : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    Transform targetStartTransform;

    public void MovePlayerToSpawn()
    {
        playerTransform.SetPositionAndRotation(targetStartTransform.position, Quaternion.identity);
    }

}

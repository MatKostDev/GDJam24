using UnityEngine;

public class StickToTransform : MonoBehaviour
{
    [Header("Target")]
    [SerializeField]
    Transform targetTransform;



    // Update is called once per frame
    void Update()
    {
        this.transform.position = targetTransform.position;
        this.transform.SetPositionAndRotation(targetTransform.position, Quaternion.Euler(CalcNewRotation()));
        
    }

    private Vector3 CalcNewPosition()
    {
        Vector3 m_newPosition = new Vector3(1,1,1);

        return m_newPosition;

    }

    private Vector3 CalcNewRotation()
    {
        Vector3 m_newRotation = targetTransform.rotation.eulerAngles;

        m_newRotation.x = 0;
        m_newRotation.z = 0;

        return m_newRotation;
    }

}

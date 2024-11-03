using UnityEngine;

public class PaddleCollision : MonoBehaviour
{
    Collider m_collider;

    int m_numCollisions = 0;

    public bool IsCollision { get => m_numCollisions > 0; }

    void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider a_other)
    {
        m_numCollisions++;
    }
    void OnTriggerExit(Collider a_other)
    {
        m_numCollisions--;
    }
}

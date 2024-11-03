using UnityEngine;

public class DespawnAfterTime : MonoBehaviour
{
    [SerializeField]
    float lifetime = 20f;

    float m_despawnCountdown;

    void Start()
    {
        m_despawnCountdown = lifetime;
    }

    void Update()
    {
        m_despawnCountdown -= Time.deltaTime;
        if (m_despawnCountdown < 0f)
        {
            Destroy(gameObject);
        }
    }
}

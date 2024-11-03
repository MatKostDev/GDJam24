using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject logPrefab;

    [SerializeField]
    float spawnFrequency = 10f;

    [SerializeField]
    float firstSpawnDelay = 0f;

    float m_spawnCountdown;

    void Start()
    {
        m_spawnCountdown = firstSpawnDelay;
    }

    void Update()
    {
        m_spawnCountdown -= Time.deltaTime;
        if (m_spawnCountdown < 0f)
        {
            Instantiate(logPrefab, transform.position, transform.rotation);

            m_spawnCountdown = spawnFrequency;
        }
    }
}

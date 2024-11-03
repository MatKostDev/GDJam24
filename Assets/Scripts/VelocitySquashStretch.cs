using UnityEngine;

public class VelocitySquashStretch : MonoBehaviour
{

    [SerializeField]
    AnimationCurve stretchCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField]
    float curveRemapZero = 0.0f;

    [SerializeField]
    float curveRemapOne = 1.0f;

    [SerializeField]
    Rigidbody rigidbodyTarget;

    [SerializeField]
    Transform deformationTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetScale();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetScale()
    {
        deformationTarget.localScale.Set(1.0f, 1.0f, 1.0f);
    }


}

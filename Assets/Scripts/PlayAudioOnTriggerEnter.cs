using UnityEngine;
using UnityEngine.Events;

public class PlayAudioOnTriggerEnter : MonoBehaviour
{
    [SerializeField] FMODUnity.StudioEventEmitter waterEvent;
    [SerializeField] FMODUnity.StudioEventEmitter groundEvent;

    [SerializeField] UnityEvent onEnterWater;
    [SerializeField] UnityEvent onEnterGround;

    private void OnTriggerEnter(Collider other)
    {
        /**if (other.CompareTag("Water water"))
        {
            onEnterWater?.Invoke();
        }//*/
        Debug.Log("Physics mat: " + other.material.ToString());

        if (other.material.ToString() == "Water (Instance) (UnityEngine.PhysicsMaterial)")
        {
            onEnterWater?.Invoke();
        }
        if (other.CompareTag("Player"))
        {
            return;
        }
        //otherwise...
            onEnterGround?.Invoke();
        //...it's probably the ground
    }

}

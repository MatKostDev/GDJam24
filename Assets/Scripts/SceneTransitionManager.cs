using UnityEngine;
using EasyTransition;

public class SceneTransitionManager : MonoBehaviour
{
    public TransitionSettings transition;
    public float delay = 0.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

   public void LoadScene(string p_sceneName)
    {
        TransitionManager.Instance().Transition(p_sceneName, transition, delay);
    }
    public void Quit()
    {
        Application.Quit(1);
    }

}

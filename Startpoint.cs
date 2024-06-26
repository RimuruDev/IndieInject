using System.Collections.Generic;
using DIedMoth.Scenes;
using IndieInject;
using UnityEngine;

public class Startpoint : MonoBehaviour
{
    void Start()
    {
        Game.Injector.RegisterGameDependencies(this);
        
        Game.SwitchTo(new CommonScene());
    }

    [Inject]
    private void Construct(List<Terrain> d1, List<string> d2)
    {
        Debug.Log(d1);
        Debug.Log(d2);
    }
}
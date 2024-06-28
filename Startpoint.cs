using DIedMoth.Scenes;
using UnityEngine;

public class Startpoint : MonoBehaviour
{
    void Start()
    {
        Game.Injector.RegisterCoreDependencies(this);
        
        Game.SwitchTo(new CommonScene());
    }
}
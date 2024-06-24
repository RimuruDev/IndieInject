using destructive_code.Scenes;
using UnityEngine;

public class Startpoint : MonoBehaviour
{
    void Start()
    {
        Game.Injector.RegisterGameDependencies(this);
    }
}

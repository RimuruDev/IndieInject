using MothDIed.Scenes;
using UnityEngine;

public class GameStartPoint : MonoBehaviour
{
    void Start()
    {
        Game.Injector.RegisterCoreDependencies(this);
        
        Game.SwitchTo(new CommonScene());
    }
}
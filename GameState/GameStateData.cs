namespace MothDIed.LevelGeneration.GameStates
{
    public abstract class GameStateData
    {
        public virtual void OnSceneLoaded() {}
        public virtual void Enter() {}
        public virtual void Exit() {}
    }
}
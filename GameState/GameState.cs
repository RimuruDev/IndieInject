namespace destructive_code.LevelGeneration.GameStates
{
    public abstract class GameState
    {
        public virtual void OnSceneLoaded() {}
        public virtual void Enter() {}
        public virtual void Exit() {}
    }
}
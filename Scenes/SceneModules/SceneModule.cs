namespace MothDIed.Scenes.SceneModules
{
    public abstract class SceneModule
    {
        public virtual void StartModule(Scene scene) { }
        
        public virtual void StopModule(Scene scene) { }
        
        public virtual void UpdateModule(Scene scene) { }
        
        public virtual void Dispose(Scene scene) { }
    }
}
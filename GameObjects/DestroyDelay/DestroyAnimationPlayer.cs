using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MothDIed.GameObjects
{
    [DisallowMultipleComponent]
    public sealed class DestroyAnimationPlayer : DestroyDelay
    {
        [SerializeField] private AnimationClip destroyAnimation;
        [SerializeField] private Animator animator;
        
        private void Reset()
        {
            if (TryGetComponent(out Animator animator))
            {
                this.animator = animator;
            }
            else
            {
                this.animator = GetComponentInChildren<Animator>();    
            }
        }
    
        public override async UniTask Destroy()
        {
            var animationParsed = Animator.StringToHash(destroyAnimation.name);
            
            animator.Play(animationParsed);
            
            await UniTask.WaitForSeconds(destroyAnimation.length);
        }
    }
}
    using Cysharp.Threading.Tasks;
using UnityEngine;

namespace destructive_code.GameObjects
{
    [DisallowMultipleComponent]
    public sealed class DestroyAnimationPlayer : DestroyDelay
    {
        [SerializeField] private AnimationClip animation;
        [SerializeField] private Animator animator;
        
        private void Reset()
        {
            if (TryGetComponent(out Animator animatorOnGO))
            {
                this.animator = animatorOnGO;
            }
            else
            {
                animator = GetComponentInChildren<Animator>();    
            }
        }
    
        public override async UniTask Destroy()
        {
            var animationParsed = Animator.StringToHash(animation.name);
            
            animator.Play(animationParsed);
            
            await UniTask.WaitForSeconds(animation.length);
        }
    }
}
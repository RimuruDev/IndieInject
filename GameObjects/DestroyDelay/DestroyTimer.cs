using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MothDIed.GameObjects
{
    [DisallowMultipleComponent]
    public sealed class DestroyTimer : DestroyDelay
    {
        [SerializeField] private float time;
        
        public override async UniTask Destroy()
        {
            await UniTask.WaitForSeconds(time);
        }
    }
}
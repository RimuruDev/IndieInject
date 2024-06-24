using Cysharp.Threading.Tasks;
using UnityEngine;

namespace destructive_code.GameObjects
{
    public abstract class DestroyDelay : MonoBehaviour
    {
        public abstract UniTask Destroy();
    }
}
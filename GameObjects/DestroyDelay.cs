using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DIedMoth.GameObjects
{
    public abstract class DestroyDelay : MonoBehaviour
    {
        public abstract UniTask Destroy();
    }
}
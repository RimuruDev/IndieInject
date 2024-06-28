using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MothDIed.GameObjects
{
    public abstract class DestroyDelay : MonoBehaviour
    {
        public abstract UniTask Destroy();
    }
}
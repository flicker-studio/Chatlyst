using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime
{
    public interface IElement
    {
        string Id { get; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        float Scale { get; set; }
        bool Visible { get; set; }

        UniTask InitializeAsync();
        UniTask ChangePositionAsync(Vector3 targetPosition, float endTime);
        UniTask ChangeRotationAsync(Vector3 targetRotation, float endTime);
        UniTask ChangeScaleAsync(float targetScale, float endTime);
    }
}
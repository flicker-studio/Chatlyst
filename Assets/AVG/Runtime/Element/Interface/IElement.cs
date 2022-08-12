using System.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime.Element
{
    public interface IElement
    {
        string Id { get; }
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
        float Scale { get; set; }
        bool Visible { get; set; }

        Task InitializeAsync();
        Task ChangePositionAsync(Vector3 targetPosition, float endTime);
        Task ChangeRotationAsync(Vector3 targetRotation, float endTime);
        Task ChangeScaleAsync(float targetScale, float endTime);
    }
}
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NexusVisual.Runtime
{
    public class ViewElement : IViewElement
    {
        public string Id { get; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
        public bool Visible { get; set; }
        public GameObject Plane { get; set; }


        public async UniTask InitializeAsync()
        {
            //TODO:init the view element
            await UniTask.CompletedTask;
        }

        public async UniTask ChangePositionAsync(Vector3 targetPosition, float endTime)
        {
            //TODO:update algorithm
            while (Position.z < 100)
            {
                Position += new Vector3(0, 0, 1f);
                Plane.transform.position = Position;
                await UniTask.Yield();
            }
        }

        public UniTask ChangeRotationAsync(Vector3 targetRotation, float endTime)
        {
            throw new System.NotImplementedException();
        }

        public UniTask ChangeScaleAsync(float targetScale, float endTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
using System.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime.Element.View
{
    public class ViewElement : IViewElement
    {
        public string Id { get; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public float Scale { get; set; }
        public bool Visible { get; set; }
        public GameObject Plane { get; set; }


        public async Task InitializeAsync()
        {
            //TODO:init the view element
            await Task.CompletedTask;
        }

        public async Task ChangePositionAsync(Vector3 targetPosition, float endTime)
        {
            //TODO:update algorithm
            while (Position.z < 100)
            {
                Position += new Vector3(0, 0, 1f);
                Plane.transform.position = Position;
                await Task.Yield();
            }
        }

        public Task ChangeRotationAsync(Vector3 targetRotation, float endTime)
        {
            throw new System.NotImplementedException();
        }

        public Task ChangeScaleAsync(float targetScale, float endTime)
        {
            throw new System.NotImplementedException();
        }
    }
}
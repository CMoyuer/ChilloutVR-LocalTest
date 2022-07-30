using UnityEngine;

namespace ABI.CCK.Components
{
    public class CVRHapticAreaChest : MonoBehaviour
    {
        public Vector3 chestAreaSize = new Vector3(0.05f, 0.05f, 0.05f);

        [HideInInspector]
        public Vector3[] HapticPoints40 = new []
        {
            new Vector3(-0.75f, 0.8f, 1f),
            new Vector3(-0.25f, 0.8f, 1f),
            new Vector3(0.25f, 0.8f, 1f),
            new Vector3(0.75f, 0.8f, 1f),
            
            new Vector3(-0.75f, 0.4f, 1f),
            new Vector3(-0.25f, 0.4f, 1f),
            new Vector3(0.25f, 0.4f, 1f),
            new Vector3(0.75f, 0.4f, 1f),
            
            new Vector3(-0.75f, 0f, 1f),
            new Vector3(-0.25f, 0f, 1f),
            new Vector3(0.25f, 0f, 1f),
            new Vector3(0.75f, 0f, 1f),
            
            new Vector3(-0.75f, -0.4f, 1f),
            new Vector3(-0.25f, -0.4f, 1f),
            new Vector3(0.25f, -0.4f, 1f),
            new Vector3(0.75f, -0.4f, 1f),
            
            new Vector3(-0.75f, -0.8f, 1f),
            new Vector3(-0.25f, -0.8f, 1f),
            new Vector3(0.25f, -0.8f, 1f),
            new Vector3(0.75f, -0.8f, 1f),
            
            new Vector3(-0.75f, 0.8f, -1f),
            new Vector3(-0.25f, 0.8f, -1f),
            new Vector3(0.25f, 0.8f, -1f),
            new Vector3(0.75f, 0.8f, -1f),
            
            new Vector3(-0.75f, 0.4f, -1f),
            new Vector3(-0.25f, 0.4f, -1f),
            new Vector3(0.25f, 0.4f, -1f),
            new Vector3(0.75f, 0.4f, -1f),
            
            new Vector3(-0.75f, 0f, -1f),
            new Vector3(-0.25f, 0f, -1f),
            new Vector3(0.25f, 0f, -1f),
            new Vector3(0.75f, 0f, -1f),
            
            new Vector3(-0.75f, -0.4f, -1f),
            new Vector3(-0.25f, -0.4f, -1f),
            new Vector3(0.25f, -0.4f, -1f),
            new Vector3(0.75f, -0.4f, -1f),
            
            new Vector3(-0.75f, -0.8f, -1f),
            new Vector3(-0.25f, -0.8f, -1f),
            new Vector3(0.25f, -0.8f, -1f),
            new Vector3(0.75f, -0.8f, -1f),
        };

        [HideInInspector]
        public int selectedPoint = -1;

        private void OnDrawGizmosSelected()
        {
            if (isActiveAndEnabled)
            {
                Gizmos.color = Color.yellow;
                Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
                Gizmos.matrix = rotationMatrix;
                Gizmos.DrawWireCube(Vector3.zero, chestAreaSize);
                
                foreach (var point in HapticPoints40)
                {
                    var localPoint = point;
                    localPoint.Scale(chestAreaSize * 0.5f);
                    
                    Gizmos.DrawWireCube(localPoint, new Vector3(0.01f, 0.01f, 0.01f));
                }
            }
        }
    }
}
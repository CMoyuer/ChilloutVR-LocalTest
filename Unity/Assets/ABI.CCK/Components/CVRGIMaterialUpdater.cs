using UnityEngine;

namespace ABI.CCK.Components
{
    [RequireComponent(typeof(Renderer))]
    public class CVRGIMaterialUpdater : MonoBehaviour
    {
        [SerializeField] bool updateEveryFrame;
        private Renderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Update()
        {
            if (_renderer == null || !updateEveryFrame) return;
            _renderer.UpdateGIMaterials();
        }
    
    }
}
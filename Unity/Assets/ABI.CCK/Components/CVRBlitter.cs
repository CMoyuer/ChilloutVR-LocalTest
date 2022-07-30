using UnityEngine;

public class CVRBlitter : MonoBehaviour
{
    [SerializeField] RenderTexture originTexture = null;
    [SerializeField] RenderTexture destinationTexture = null;
    [SerializeField] Material blitMaterial = null;
    public bool clearEveryFrame;
}

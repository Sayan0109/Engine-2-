using UnityEngine;

public class RotateSkybox : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;

    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation",
            Time.time * rotationSpeed);
    }
}
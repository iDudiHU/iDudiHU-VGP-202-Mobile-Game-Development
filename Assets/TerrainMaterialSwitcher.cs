using System.Collections;
using UnityEngine;

public class TerrainMaterialSwitcher : MonoBehaviour
{
    [ColorUsage(true)] public Color downColor;
    [ColorUsage(true)] public Color upColor;
    [ColorUsage(true)] public Color leftColor;
    [ColorUsage(true)] public Color rightColor;

    [SerializeField] private Renderer terrainRenderer;
    [SerializeField] private float animationDuration = 1.0f;

    private Material terrainMaterial;

    private void Awake()
    {
        if (terrainRenderer != null)
        {
            terrainMaterial = terrainRenderer.sharedMaterial;
        }
    }

    public void SwitchColor(ObstacleGravityController.GravityDirection direction)
    {
        if (terrainMaterial == null) return;

        StartCoroutine(AnimateSwitchColor(direction));
    }

    private IEnumerator AnimateSwitchColor(ObstacleGravityController.GravityDirection direction)
    {
        float elapsedTime = 0f;
        float startValue = 6f;
        float endValue = 100f;

        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float currentValue = Mathf.Lerp(startValue, endValue, t);

            terrainMaterial.SetFloat("Vector1_F5FD9210", currentValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Animate back to the start value
        elapsedTime = 0f;
        SwitchColorBasedOnDirection(direction);
        while (elapsedTime < animationDuration)
        {
            float t = elapsedTime / animationDuration;
            float currentValue = Mathf.Lerp(endValue, startValue, t);

            terrainMaterial.SetFloat("Vector1_F5FD9210", currentValue);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void SwitchColorBasedOnDirection(ObstacleGravityController.GravityDirection direction)
    {
        switch (direction)
        {
            case ObstacleGravityController.GravityDirection.Down:
                terrainMaterial.SetColor("_GridColor", downColor);
                Debug.Log("Down");
                break;
            case ObstacleGravityController.GravityDirection.Up:
                terrainMaterial.SetColor("_GridColor", upColor);
                Debug.Log("Up");
                break;
            case ObstacleGravityController.GravityDirection.Left:
                terrainMaterial.SetColor("_GridColor", leftColor);
                Debug.Log("Left");
                break;
            case ObstacleGravityController.GravityDirection.Right:
                terrainMaterial.SetColor("_GridColor", rightColor);
                Debug.Log("Right");
                break;
        }
    }
}

using System.Collections;
using UnityEngine;

public class TerrainMaterialSwitcher : MonoBehaviour
{
    [ColorUsage(true)] public Color downColor;
    [ColorUsage(true)] public Color upColor;
    [ColorUsage(true)] public Color leftColor;
    [ColorUsage(true)] public Color rightColor;

    [SerializeField] private Renderer terrainRenderer;
    [SerializeField] private float animationDuration = 0.5f;

    private Material terrainMaterial;

    private void Awake()
    {
        downColor = HexToColor("#00FF27");
        upColor = HexToColor("#2034AD");
        leftColor = HexToColor("#FF7900");
        rightColor = HexToColor("#FF0002");
        if (!terrainRenderer)
        {
            terrainRenderer = GetComponent<Renderer>();
        }
        terrainMaterial = terrainRenderer.sharedMaterial;
        SwitchColorBasedOnDirection(ObstacleGravityController.GravityDirection.Down);
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

    public Color HexToColor(string hexColor)
    {
        // Remove the '#' character from the beginning of the hex color string
        if (hexColor.StartsWith("#"))
        {
            hexColor = hexColor.Substring(1);
        }

        // Parse the red, green, and blue values from the hex color string
        float r = int.Parse(hexColor.Substring(0, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float g = int.Parse(hexColor.Substring(2, 2), System.Globalization.NumberStyles.HexNumber) / 255f;
        float b = int.Parse(hexColor.Substring(4, 2), System.Globalization.NumberStyles.HexNumber) / 255f;

        // Create a new Color object with the parsed values
        Color color = new Color(r, g, b);

        return color;
    }
}

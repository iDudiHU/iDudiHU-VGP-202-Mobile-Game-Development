using UnityEngine;

public class ObstacleGravityController : MonoBehaviour
{
    public enum GravityDirection { Down, Up, Left, Right, Forward, Backward }
    [SerializeField] private GravityDirection currentGravityDirection = GravityDirection.Down;

    public static ObstacleGravityController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

	private void Start()
	{
        GameObject terrain = GameObject.Find("Terrain");
        terrain.AddComponent<TerrainMaterialSwitcher>();
        terrain.AddComponent<BoxCollider>();
	}

	public void SetGravityDirection(GravityDirection newDirection)
    {
        currentGravityDirection = newDirection;
        ApplyGravityDirectionToObstacles();
        ApplyGravityShiftToTerrain();
    }

    private void ApplyGravityShiftToTerrain()
	{
		FindObjectOfType<TerrainMaterialSwitcher>()?.SwitchColor(currentGravityDirection);
    }

    private void ApplyGravityDirectionToObstacles()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (Obstacle obstacle in obstacles)
        {
            obstacle.ApplyGravityDirection(GetGravityVector());
        }
    }

    public Vector3 GetGravityVector()
    {
        float g = 9.8f;
        Vector3 gravity = Physics.gravity;

        switch (currentGravityDirection)
        {
            case GravityDirection.Down:
                gravity = new Vector3(0, -Mathf.Abs(g), 0);
                break;
            case GravityDirection.Up:
                gravity = new Vector3(0, Mathf.Abs(g), 0);
                break;
            case GravityDirection.Left:
                gravity = new Vector3(-Mathf.Abs(g), 0, 0);
                break;
            case GravityDirection.Right:
                gravity = new Vector3(Mathf.Abs(g), 0, 0);
                break;
            case GravityDirection.Forward:
                gravity = new Vector3(0, 0, Mathf.Abs(g));
                break;
            case GravityDirection.Backward:
                gravity = new Vector3(0, 0, -Mathf.Abs(g));
                break;
        }

        return gravity;
    }
}

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

    public void SetGravityDirection(GravityDirection newDirection)
    {
        currentGravityDirection = newDirection;
        ApplyGravityDirectionToObstacles();
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
        Vector3 gravity = Physics.gravity;

        switch (currentGravityDirection)
        {
            case GravityDirection.Down:
                gravity = new Vector3(0, -Mathf.Abs(gravity.y), 0);
                break;
            case GravityDirection.Up:
                gravity = new Vector3(0, Mathf.Abs(gravity.y), 0);
                break;
            case GravityDirection.Left:
                gravity = new Vector3(-Mathf.Abs(gravity.x), 0, 0);
                break;
            case GravityDirection.Right:
                gravity = new Vector3(Mathf.Abs(gravity.x), 0, 0);
                break;
            case GravityDirection.Forward:
                gravity = new Vector3(0, 0, Mathf.Abs(gravity.z));
                break;
            case GravityDirection.Backward:
                gravity = new Vector3(0, 0, -Mathf.Abs(gravity.z));
                break;
        }

        return gravity;
    }
}

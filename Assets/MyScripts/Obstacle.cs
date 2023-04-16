using HyperCasual.Runner;
using UnityEngine;

public class Obstacle : Spawnable
{
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (ObstacleGravityController.Instance != null)
        {
            ApplyGravityDirection(ObstacleGravityController.Instance.GetGravityVector());
        }
    }

    public void ApplyGravityDirection(Vector3 gravity)
    {
        if (rb == null)
        {
            return;
        }
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        Physics.gravity = gravity;
        rb.useGravity = true;
    }
}

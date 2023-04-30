using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Gyroscope = UnityEngine.InputSystem.Gyroscope;

public class GyroscopeInput : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float tiltSensitivity = 0.5f;
    [SerializeField] private float deadzone = 0.15f;

    private GravitySensor gravitySensor;
    public TextMeshProUGUI gyroDebugText;

    private void Awake()
    {
        if (!playerController)
		{
            playerController = FindObjectOfType<PlayerController>();
		}
    }

	private void Start()
	{
        gyroDebugText = GameObject.Find("GyroDebug").GetComponent<TextMeshProUGUI>();
        InputSystem.EnableDevice(Accelerometer.current);
    }

	private void Update()
    {
        Vector3 acceleration = Accelerometer.current.acceleration.ReadValue();
        // Get the acceleration value on the X-axis
        float tilt = acceleration.x;
        gyroDebugText.text = $"{tilt}";
        if (Mathf.Abs(tilt) < deadzone)
        {
            tilt = 0.0f;
        }

        // Apply sensitivity (optional)
        float tiltWithSensitivity = tilt * tiltSensitivity;

        // Use the tilt value to set the target position of the player
        playerController.SetDeltaPosition(tiltWithSensitivity);
    }
}

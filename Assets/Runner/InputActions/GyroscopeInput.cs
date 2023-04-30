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
    private float tiltSensitivity = 0.1f;
    private float deadzone = 0.3f;

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

        float tilt = acceleration.x;
        gyroDebugText.text = $"{tilt}";

        if (Mathf.Abs(tilt) < deadzone)
        {
            tilt = 0.0f;
        }
        else
        {
            float normalizedTilt = (Mathf.Abs(tilt) - deadzone) / (1 - deadzone);

            float sensitivityCurve = Mathf.SmoothStep(0.5f, 1.5f, normalizedTilt);

            float tiltWithSensitivity = tilt * sensitivityCurve * tiltSensitivity;

            playerController.SetDeltaPosition(tiltWithSensitivity);
        }
    }

}

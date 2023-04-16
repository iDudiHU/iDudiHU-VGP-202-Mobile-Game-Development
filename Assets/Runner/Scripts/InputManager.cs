using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace HyperCasual.Runner
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance => s_Instance;
        static InputManager s_Instance;

        public enum SwipeDirection { Left, Right, Up, Down, Tap };

        [SerializeField]
        float m_InputSensitivity = 1.5f;

        bool m_HasInput;
        Vector3 m_InputPosition;
        Vector3 m_PreviousInputPosition;
        Vector3 m_StartPosition;
        float m_SwipeThreshold = 50f;

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            s_Instance = this;
        }

        void OnEnable()
        {
            EnhancedTouchSupport.Enable();
        }

        void OnDisable()
        {
            EnhancedTouchSupport.Disable();
        }

        void Update()
        {
            if (PlayerController.Instance == null)
            {
                return;
            }

#if UNITY_EDITOR
            m_InputPosition = Mouse.current.position.ReadValue();

            if (Mouse.current.leftButton.isPressed)
            {
                if (!m_HasInput)
                {
                    m_StartPosition = m_PreviousInputPosition = m_InputPosition;
                }
                m_HasInput = true;
            }
            else
            {
                if (m_HasInput)
                {
                    DetectSwipe();
                }
                m_HasInput = false;
            }
#else
            if (Touch.activeTouches.Count > 0)
            {
                m_InputPosition = Touch.activeTouches[0].screenPosition;

                if (!m_HasInput)
                {
                    m_StartPosition = m_PreviousInputPosition = m_InputPosition;
                }
                
                m_HasInput = true;
            }
            else
            {
                if (m_HasInput)
                {
                    DetectSwipe();
                }
                m_HasInput = false;
            }
#endif

            m_PreviousInputPosition = m_InputPosition;
        }

        void DetectSwipe()
        {
            Vector3 delta = m_InputPosition - m_StartPosition;

            if (Mathf.Abs(delta.x) > m_SwipeThreshold || Mathf.Abs(delta.y) > m_SwipeThreshold)
            {
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x > 0)
                        OnSwipe(SwipeDirection.Right);
                    else
                        OnSwipe(SwipeDirection.Left);
                }
                else
                {
                    if (delta.y > 0)
                        OnSwipe(SwipeDirection.Up);
                    else
                        OnSwipe(SwipeDirection.Down);
                }
            }
            else
            {
                OnSwipe(SwipeDirection.Tap);
            }
        }

        void OnSwipe(SwipeDirection direction)
        {
            Debug.Log($"Swipe {direction}");
            PlayerController.Instance.OnSwipe(direction);
        }
    }
}

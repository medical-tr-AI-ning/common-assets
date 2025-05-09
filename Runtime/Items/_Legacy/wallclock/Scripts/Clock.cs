using System;
using UnityEngine;

/// <summary>
/// Based on https://oxmond.com/create-a-real-time-analog-wall-clock-using-c-unity-2018-intermediate-tutorial
/// </summary>
namespace Tweens
{
    [RequireComponent(typeof(AudioSource))]
    public class Clock : MonoBehaviour
    {
        private enum rotationAxis
        {
            X,
            Y,
            Z
        }

        private static readonly Vector3[] vectorAxes = new Vector3[] {
            Vector3.right,
            Vector3.up,
            Vector3.forward
        };

        #region Attributes
        // Hand Gameobjects
        [SerializeField]
        private GameObject secondHand;
        [SerializeField]
        private GameObject minuteHand;
        [SerializeField]
        private GameObject hourHand;

        [SerializeField]
        private AudioSource audioSource;

        // Options
        [SerializeField]
        private rotationAxis _localRotationAxis = rotationAxis.Z;
        [SerializeField]
        private bool linearSecondHandMovement = false;
        [SerializeField]
        private bool _enableSound = true;

        // Trigger
        private string _oldSeconds;

        // Tweens
        private LocalRotationTween _secondHandTween;
        private LocalRotationTween _minuteHandTween;
        private LocalRotationTween _hourHandTween;

        // Initial Transform Rotations
        private Quaternion _initRotationSecondHand = new();
        private Quaternion _initRotationMinuteHand = new();
        private Quaternion _initRotationHourHand = new();
        #endregion

        #region unity_functions
        private void OnValidate()
        {
            SetupTweens();
        }

        private void Start()
        {
            // Save initial rotation values
            if(secondHand) _initRotationSecondHand = secondHand.transform.rotation;
            if(minuteHand) _initRotationMinuteHand = minuteHand.transform.rotation;
            if(hourHand) _initRotationHourHand = hourHand.transform.rotation;

            audioSource = GetComponent<AudioSource>();
            SetupTweens();
        }

        private void Update()
        {
            string seconds = DateTime.UtcNow.ToString("ss");

            if (seconds != _oldSeconds)
            {                
                UpdateTimer();
                if (_enableSound)
                {
                    audioSource.Play();
                }
            }
            _oldSeconds = seconds;
        }
        #endregion

        #region helper_functions
        private void SetupTweens()
        {
            // SecondHand
            _secondHandTween = new LocalRotationTween
            {
                easeType = linearSecondHandMovement ? EaseType.Linear : EaseType.QuintOut,
                duration = 1
            };

            // MinuteHand
            _minuteHandTween = new LocalRotationTween
            {
                easeType = EaseType.ElasticOut,
                duration = 1
            };

            // HourHand
            _hourHandTween = new LocalRotationTween
            {
                easeType = EaseType.QuintOut,
                duration = 1
            };
        }

        private void UpdateTimer()
        {
            int secondsInt = int.Parse(DateTime.UtcNow.ToString("ss"));
            int minutesInt = int.Parse(DateTime.UtcNow.ToString("mm"));
            int hoursInt = int.Parse(DateTime.UtcNow.ToLocalTime().ToString("hh"));

            if (secondHand)
            {
                _secondHandTween.to = _initRotationSecondHand * Quaternion.Euler(GetAxis(_localRotationAxis) * (secondsInt * 6));
                secondHand.AddTween(_secondHandTween);
            }
            if (minuteHand)
            {
                _minuteHandTween.to = _initRotationMinuteHand * Quaternion.Euler(GetAxis(_localRotationAxis) * (minutesInt * 6));
                minuteHand.AddTween(_minuteHandTween);
            }
            if (hourHand)
            {
                float hourDistance = (float)(minutesInt) / 60f;
                _hourHandTween.to = _initRotationHourHand * Quaternion.Euler(GetAxis(_localRotationAxis) * ((hoursInt + hourDistance) * 360 / 12));
                hourHand.AddTween(_hourHandTween);
            }
        }

        private Vector3 GetAxis(rotationAxis p_Axis)
        {
            return vectorAxes[(int)p_Axis];
        }
        #endregion
    }
}
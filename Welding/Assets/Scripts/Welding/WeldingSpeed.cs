using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WeldingSpeed : MonoBehaviour
{
    public UIScript uIScript;

    #region Values for Speed
    [Header("Values for Speed")]
    Vector3 lastMousePosition;
    Vector3 currentMousePosition;
    public float mouseSpeed;
    float timeSinceLastCheck;
    public float smoothSpeed = 0.1f; // adjust this value to control the smoothness of the speed calculation
    int sampleCount = 0;
    public float averageSpeed = 0;
    int sampleSize = 5; // how many samples you want to average
    #endregion

    #region Values for Score
    [Header("Values for Score")]
    public float MinSpeed;
    public float MaxSpeed;
    public float BigScore;
    public float SmallScore;
    public float ResultScore=0;
    #endregion

    #region Values for Timer
    [Header("Values for Timer")]
    public float TimerResult;
    public float TimeAdd;
    public TextMeshProUGUI TimeText;
    #endregion

    public bool WeldingSpeedBool;
    public bool WeldingScoreBool;

    private void Start()
    {
        TimeText.text = "";
        BigScore = 100 / (TimerResult*2);
        SmallScore = BigScore / 2;
    }

    void Update()
    {
        if (WeldingSpeedBool)
        {
            if (Input.GetMouseButton(0))
            {
                currentMousePosition = Input.mousePosition;
                if (lastMousePosition != Vector3.zero)
                {
                    timeSinceLastCheck += Time.deltaTime;
                    if (timeSinceLastCheck >= 0.5f)
                    {
                        CalculateSpeed();
                        if (WeldingScoreBool)
                        {
                            ResultScore += ((MinSpeed < mouseSpeed && mouseSpeed < MaxSpeed) ? BigScore : SmallScore);

                        }

                    }
                }
                else
                {
                    lastMousePosition = currentMousePosition;
                }
            }
            else
            {
                mouseSpeed = 0;
            }
        }
        
    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
        TimerResult--;
        TimeText.text = TimerResult.ToString();
        if (TimeAdd<TimerResult)
        {
            StartCoroutine(Timer());
        }
        else
        {
            WeldingSpeedBool = false;
            uIScript.FinalFunc();
        }
    }

    private void CalculateSpeed()
    {
        float distance = Vector3.Distance(currentMousePosition, lastMousePosition);
        float calculatedSpeed = distance / 0.5f;
        sampleCount++;
        averageSpeed = ((averageSpeed * (sampleCount - 1)) + calculatedSpeed) / sampleCount;
        mouseSpeed = averageSpeed;
        if (sampleCount >= sampleSize)
        {
            sampleCount = 0;
            averageSpeed = 0;
        }
        // You can use the mouseSpeed variable to control other elements in your game or to show it to the player
        timeSinceLastCheck = 0f;
        lastMousePosition = currentMousePosition;
    }
}

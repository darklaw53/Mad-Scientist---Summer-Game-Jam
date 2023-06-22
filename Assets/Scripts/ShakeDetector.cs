using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDetector : Singleton<ShakeDetector>
{
    float mouseSpeed;
    public float maxSpeed = 0;
    public float barDegradeAmount = 1;

    float barValue = 0;

    public Slider slider;

    public float vibrateSpeed = 5;
    public float vibrateIntensity = .1f;
    public GameObject potionVial;

    Vector3 vialStartPos;

    private void Start()
    {
        vialStartPos = potionVial.transform.localPosition;
    }

    void Update()
    {
        mouseSpeed = Mathf.Sqrt((Input.GetAxis("Mouse X") * Input.GetAxis("Mouse X")) + (Input.GetAxis("Mouse Y") * Input.GetAxis("Mouse Y")));
        /*
        //detector (3 is quite fast, 1 is quite stable)
        if (maxSpeed < mouseSpeed)
        {
            maxSpeed = mouseSpeed;
        }
        */

        barValue += mouseSpeed;
        barValue -= barDegradeAmount;

        if (barValue < 0)
        {
            barValue = 0;
        }
        else if (barValue > 100)
        {
            barValue = 100;
        }

        slider.value = barValue;

        if (HoldAndShake.Instance.shaking)
        {
            potionVial.transform.localPosition = vibrateIntensity * new Vector3(
                vialStartPos.x + Mathf.PerlinNoise(mouseSpeed * Time.time, 1),
                vialStartPos.y + Mathf.PerlinNoise(mouseSpeed * Time.time, 2),
                vialStartPos.z + Mathf.PerlinNoise(mouseSpeed * Time.time, 3));
        }
    }
}

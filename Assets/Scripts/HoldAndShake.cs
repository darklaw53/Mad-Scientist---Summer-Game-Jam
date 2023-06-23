using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAndShake : Singleton<HoldAndShake>
{
    public GameObject shakeBar, shakeButton;
    public bool shaking;

    private void OnMouseDown()
    {
        shaking = true;
        ShakeDetector.Instance.barValue = 0;
        ShakeDetector.Instance.slider.value = 0;
        shakeButton.SetActive(false);
        shakeBar.SetActive(true);
        MixMaster.Instance.ResetTimer(1);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && shaking)
        {
            StopShaking();
        }
    }

    public void StopShaking()
    {
        shaking = false;
        shakeButton.SetActive(true);
        shakeBar.SetActive(false);
    }
}

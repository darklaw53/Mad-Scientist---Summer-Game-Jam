using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixMaster : Singleton<MixMaster>
{
    public float timeRemaining = 1;
    bool isInTheGoodZone, isInTheBadZone;
    bool finished = false;

    public float badTimeTolerance = 1;
    float maxTolerance;

    public Image warning;

    private void Start()
    {
        maxTolerance = badTimeTolerance;
    }

    void Update()
    {
        if (timeRemaining > 0 && isInTheGoodZone)
        {
            timeRemaining -= Time.deltaTime;
        }
        else if (timeRemaining <= 0 && !finished)
        {
            finished = true;
            FinishMix();
        }

        if (badTimeTolerance > 0 && isInTheBadZone)
        {
            badTimeTolerance -= Time.deltaTime;
        }
        else if (badTimeTolerance <= 0 && !finished)
        {
            finished = true;
            Explode();
        }
        else if (badTimeTolerance < maxTolerance && !isInTheBadZone)
        {
            badTimeTolerance += Time.deltaTime;
        }

        warning.rectTransform.localScale = new Vector3(2 - badTimeTolerance, 2 - badTimeTolerance, 2 - badTimeTolerance);
    }

    public void ResetTimer(float time)
    {
        finished = false;
        timeRemaining = time;
    }

    #region Getters and Setters
    public void SetIsInGoodZone()
    {
        isInTheGoodZone = true;
    }

    public void SetOutOfGoodZone()
    {
        isInTheGoodZone = false;
    }

    public void SetIsInBadZone()
    {
        isInTheBadZone = true;
    }

    public void SetOutOfBadZone()
    {
        isInTheBadZone = false;
    }
    #endregion

    void Explode()
    {
        Debug.Log("BOOM");
    }

    void FinishMix()
    {
        Debug.Log("DONE");
    }
}


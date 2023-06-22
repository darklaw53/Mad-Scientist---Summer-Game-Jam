using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixMaster : Singleton<MixMaster>
{
    public float timeRemaining = 1;
    float maxTime = 1;
    bool isInTheGoodZone, isInTheBadZone;
    bool finished = false;

    public float badTimeTolerance = 1;
    float maxTolerance;

    public Image warning;
    public Image progressCircle;

    public InventorySlot mixSlot1, mixSlot2;
    public GameObject mixButton;
    public SpriteRenderer tubeFluid, flaskFluid, beakerFluid;

    Color defaultColor;

    public GameObject explosion;

    private void Start()
    {
        defaultColor = tubeFluid.color;
        maxTolerance = badTimeTolerance;
    }

    void Update()
    {
        if (mixSlot1.ing != null)
        {
            tubeFluid.color = mixSlot1.ing.color;
        }
        else
        {
            tubeFluid.color = defaultColor;
        }

        if (mixSlot2.ing != null)
        {
            beakerFluid.color = mixSlot2.ing.color;
        }
        else
        {
            beakerFluid.color = defaultColor;
        }

        flaskFluid.color = new Vector4((beakerFluid.color.r + tubeFluid.color.r) / 2, (beakerFluid.color.g + tubeFluid.color.g) / 2, (beakerFluid.color.b + tubeFluid.color.b) / 2, (beakerFluid.color.a + tubeFluid.color.a) / 2);

        if (mixSlot1.ing != null && mixSlot2.ing != null && !HoldAndShake.Instance.shaking)
        {
            mixButton.SetActive(true);
        }
        else
        {
            mixButton.SetActive(false);
        }

        if (timeRemaining > 0 && isInTheGoodZone)
        {
            timeRemaining -= Time.deltaTime;
            var x = 1 -(timeRemaining / maxTime);
            progressCircle.fillAmount = x;
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
        maxTime = time;
        progressCircle.fillAmount = 0;
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
        Instantiate(explosion, IngredientDragManager.Instance.worldCanvas.transform);
        mixSlot1.image.sprite = null;
        mixSlot2.image.sprite = null;
        mixSlot1.image.color = new Vector4(mixSlot1.image.color.r, mixSlot1.image.color.g, mixSlot1.image.color.b, 0);
        mixSlot2.image.color = new Vector4(mixSlot2.image.color.r, mixSlot2.image.color.g, mixSlot2.image.color.b, 0);
        mixSlot1.ing = null;
        mixSlot2.ing = null;

        HoldAndShake.Instance.StopShaking();
    }

    void FinishMix()
    {
        FormulaManager.Instance.GenerateFormula(mixSlot1.ing, mixSlot2.ing);
        mixSlot1.image.sprite = null;
        mixSlot2.image.sprite = null;
        mixSlot1.image.color = new Vector4(mixSlot1.image.color.r, mixSlot1.image.color.g, mixSlot1.image.color.b, 0);
        mixSlot2.image.color = new Vector4(mixSlot2.image.color.r, mixSlot2.image.color.g, mixSlot2.image.color.b, 0);
        mixSlot1.ing = null;
        mixSlot2.ing = null;

        HoldAndShake.Instance.StopShaking();
    }
}


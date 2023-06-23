using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltObjectScript : MonoBehaviour
{
    public bool melting;
    public MeltObject meltObj;
    public InventorySlot slot;
    public Formula formulaInProcess;

    public GameObject particleEffect,particleEffect2;
    public ParticleSystem acidEffect,vapourEffect;

    public SpriteRenderer meltyPart;

    public SpriteRenderer thisBody;

    private void OnMouseDown()
    {
        if (!melting)
        {
            IngredientDragManager.Instance.DragItem(meltObj, slot);

            Destroy(gameObject);
        }
    }

    public void RecieveFormula(Formula formul)
    {
        var x = new Color((formul.ingredient1.color.r + formul.ingredient2.color.r) / 2, (formul.ingredient1.color.g + formul.ingredient2.color.g) / 2, (formul.ingredient1.color.b + formul.ingredient2.color.b) / 2, (formul.ingredient1.color.a + formul.ingredient2.color.a) / 2);

        formulaInProcess = formul;
        particleEffect.SetActive(true);
        var main = acidEffect.main;
        main.startColor = x;

        meltyPart.color = x;

        if (formul.potency > meltObj.meltResistance)
        {
            melting = true;
            particleEffect2.SetActive(true);
            var main2 = acidEffect.main;
            main2.startColor = x;
        }
        StartCoroutine(ChangeColor(melting, x));
        StartCoroutine(ProcessingAcid(melting));
    }

    IEnumerator ChangeColor(bool startedMelting, Color colorFraction)
    {
        while (startedMelting)
        {
            thisBody.color = new Color((colorFraction.r / 2 + thisBody.color.r) / 2, (colorFraction.g / 2 + thisBody.color.g) / 2, (colorFraction.b / 2 + thisBody.color.b) / 2, thisBody.color.a);
            yield return new WaitForSeconds(.5f);
        }
    }

    IEnumerator ProcessingAcid(bool startedMelting)
    {
        yield return new WaitForSeconds(5);
        if (!startedMelting & !melting)
        {
            particleEffect.SetActive(false);
        }
        else if (startedMelting)
        {
            SucessfulMelt();
        }
    }

    void SucessfulMelt()
    {
        IngredientDragManager.Instance.formulaRackOBJ.SetActive(false);
        IngredientDragManager.Instance.ingredientRackOBJ.SetActive(false);
        IngredientDragManager.Instance.objectRackOBJ.SetActive(true);
        slot.RecieveItem(meltObj);

        IngredientDragManager.Instance.objectRack.rack[slot.numberIndex + 1].lockIcon.SetActive(false);
        ScoreManager.Instance.score += meltObj.reward;

        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        if (!melting)
        {
            IngredientDragManager.Instance.hoverinOverMeltObject = this;
        }
    }

    private void OnMouseExit()
    {
        if (!melting)
        {
            IngredientDragManager.Instance.hoverinOverMeltObject = null;
        }
    }
}

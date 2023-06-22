using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltObjectScript : MonoBehaviour
{
    public bool melting;
    public MeltObject meltObj;
    public InventorySlot slot;
    public Formula formulaInProcess;

    public GameObject particleEffect;
    public ParticleSystem acidEffect;

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
        formulaInProcess = formul;
        particleEffect.SetActive(true);
        var main = acidEffect.main;
        main.startColor = new Color((formul.ingredient1.color.r + formul.ingredient2.color.r) / 2, (formul.ingredient1.color.g + formul.ingredient2.color.g) / 2, (formul.ingredient1.color.b + formul.ingredient2.color.b) / 2, (formul.ingredient1.color.a + formul.ingredient2.color.a) / 2);

        if (formul.potency > meltObj.meltResistance)
        {
            melting = true;
        }
        StartCoroutine(ProcessingAcid(melting));
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

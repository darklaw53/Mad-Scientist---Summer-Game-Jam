using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Ingredient ing;
    public Formula formul;
    public MeltObject meltObj;
    public Image image;
    public GameObject lockIcon;

    public bool isObjectSlot;
    public int numberIndex;

    public bool isMixSlot = false;

    private void Start()
    {
        if (isObjectSlot && numberIndex != 0)
        {
            lockIcon.SetActive(true);
        }

        if (meltObj != null)
        {
            image.sprite = meltObj.sprite;
            image.color = meltObj.bonusColor;
        }
    }

    public void RecieveItem(Ingredient ingredient)
    {
        ing = ingredient;
        image.sprite = ing.sprite;
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, 100);
    }

    public void RecieveItem(Formula formula)
    {
        formul = formula;
        image.sprite = formul.sprite;
        image.color = new Color((formul.ingredient1.color.r + formul.ingredient2.color.r) / 2, (formul.ingredient1.color.g + formul.ingredient2.color.g) / 2, (formul.ingredient1.color.b + formul.ingredient2.color.b) / 2, (formul.ingredient1.color.a + formul.ingredient2.color.a) / 2);
    }

    public void RecieveItem(MeltObject mletObjj)
    {
        meltObj = mletObjj;
        image.sprite = meltObj.sprite;
        image.color = meltObj.bonusColor;
    }

    private void OnMouseDown()
    {
        if (!PosHandler.Instance.isMoving)
        {
            if (ing != null)
            {
                IngredientDragManager.Instance.DragItem(ing, this, isMixSlot);
            
                image.sprite = null;
                image.color = new Vector4(image.color.r, image.color.g, image.color.b, 0);
            }
            else if (formul != null)
            {
                IngredientDragManager.Instance.DragItem(formul, this);

                image.sprite = null;
                image.color = new Vector4(image.color.r, image.color.g, image.color.b, 0);
            }
            else if (meltObj != null)
            {
                IngredientDragManager.Instance.DragItem(meltObj, this);

                image.sprite = null;
                image.color = new Vector4(image.color.r, image.color.g, image.color.b, 0);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (isMixSlot)
        {
            IngredientDragManager.Instance.hoverinOverSlot = true;
            IngredientDragManager.Instance.mixingSlot = this;
        }
    }

    private void OnMouseExit()
    {
        if (isMixSlot)
        {
            IngredientDragManager.Instance.hoverinOverSlot = false;
            IngredientDragManager.Instance.mixingSlot = null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Ingredient ing;
    public Image image;

    public bool isMixSlot;

    public void RecieveItem(Ingredient ingredient)
    {
        ing = ingredient;
        image.sprite = ing.sprite;
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, 100);
    }

    private void OnMouseDown()
    {
        if (ing != null)
        {
            IngredientDragManager.Instance.DragItem(ing, this, isMixSlot);
            
            image.sprite = null;
            image.color = new Vector4(image.color.r, image.color.g, image.color.b, 0);
        }
    }

    private void OnMouseEnter()
    {
        if (isMixSlot)
        {
            IngredientDragManager.Instance.hoverinOverSlot = true;
            IngredientDragManager.Instance.mixingSlot = this;
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAA!!!!");
        }
    }

    private void OnMouseExit()
    {
        if (isMixSlot)
        {
            IngredientDragManager.Instance.hoverinOverSlot = false;
            IngredientDragManager.Instance.mixingSlot = null;
            Debug.Log("OH THE PAIN!!!!");
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Ingredient ing;
    public Image image;

    public void RecieveItem(Ingredient ingredient)
    {
        ing = ingredient;
        image.sprite = ing.sprite;
        image.color = new Vector4(image.color.r, image.color.g, image.color.b, 100);
    }
}

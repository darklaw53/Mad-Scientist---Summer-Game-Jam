using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public Ingredient ing;
    public Image image;
    public TextMeshProUGUI price;
    public TextMeshProUGUI ItemName;
    ItemRack rack;

    public GameObject formulaRackOBJ;
    public GameObject ingredientRackOBJ;
    public GameObject objectRackOBJ;

    private void Awake()
    {
        image.sprite = ing.sprite;
        price.text = "" + ing.ingredientPrice;
        ItemName.text = ing.name;

        rack = IngredientDragManager.Instance.ingredientRack;
    }

    public void BuyItem()
    {
        if (ing.ingredientPrice <= ScoreManager.Instance.score && !rack.full)
        {
            formulaRackOBJ.SetActive(false);
            ingredientRackOBJ.SetActive(true);
            objectRackOBJ.SetActive(false);

            ScoreManager.Instance.score -= ing.ingredientPrice;
            rack.TakeItem(ing);
        }
    }
}

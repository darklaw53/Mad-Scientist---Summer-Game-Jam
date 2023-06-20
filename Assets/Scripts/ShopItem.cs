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

    private void Awake()
    {
        image.sprite = ing.sprite;
        price.text = "$" + ing.ingredientPrice + ".00";
        ItemName.text = ing.name;

        rack = GameObject.FindWithTag("ingredientRack").GetComponent<ItemRack>();
    }

    public void BuyItem()
    {
        if (ing.ingredientPrice < ScoreManager.Instance.score && !rack.full)
        {
            ScoreManager.Instance.score -= ing.ingredientPrice;
            rack.TakeItem(ing);
        }
    }
}

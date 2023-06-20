using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredients/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string ingredientName;
    public int ingredientPrice;
    public Sprite sprite;
    public Color color;
}
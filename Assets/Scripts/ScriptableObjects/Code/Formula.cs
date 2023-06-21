using UnityEngine;

[CreateAssetMenu(fileName = "New Formula", menuName = "Ingredients/Formula")]
public class Formula : ScriptableObject
{
    public string formulaName;
    public Ingredient ingredient1, ingredient2;
    public Sprite sprite;
}
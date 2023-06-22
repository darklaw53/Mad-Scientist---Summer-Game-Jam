using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormulaManager : Singleton<FormulaManager>
{
    public List<Formula> listOfFormulas;
    public ItemRack formulaRack;
    public GameObject formulaRackOBJ;
    public GameObject ingredientRackOBJ;
    public GameObject objectRackOBJ;

    public void GenerateFormula(Ingredient ing1, Ingredient ing2)
    {
        foreach (Formula formula in listOfFormulas)
        {
            if ((formula.ingredient1 == ing1 && formula.ingredient2 == ing2) || (formula.ingredient1 == ing2 && formula.ingredient2 == ing1))
            {
                ingredientRackOBJ.SetActive(false);
                objectRackOBJ.SetActive(false);
                formulaRackOBJ.SetActive(true);
                formulaRack.TakeItem(formula);
            }
        }
    }
}

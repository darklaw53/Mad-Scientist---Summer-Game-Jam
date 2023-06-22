using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltZone : MonoBehaviour
{
    private void OnMouseEnter()
    {
        IngredientDragManager.Instance.hoverinOverMeltZone = true;
    }

    private void OnMouseExit()
    {
        IngredientDragManager.Instance.hoverinOverMeltZone = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRack : MonoBehaviour
{
    Dictionary<int, InventorySlot> rack = new Dictionary<int, InventorySlot>();
    public bool full;

    private void Awake()
    {
        for (int i =0; i < GetComponentsInChildren<InventorySlot>().Length; i++)
        {
            rack.Add(i, gameObject.transform.GetChild(i).GetComponent<InventorySlot>());
        }
    }

    public void TakeItem(Ingredient newIng)
    {
        for (int i = 0; i < GetComponentsInChildren<InventorySlot>().Length; i++)
        {
            if (rack[i].ing == null)
            {
                rack[i].RecieveItem(newIng);

                if (i == GetComponentsInChildren<InventorySlot>().Length -1)
                {
                    full = true;
                }
                break;
            }
        }
    }

    public void TakeItem(Formula newFormula)
    {
        for (int i = 0; i < GetComponentsInChildren<InventorySlot>().Length; i++)
        {
            if (rack[i].formul == null)
            {
                rack[i].RecieveItem(newFormula);

                if (i == GetComponentsInChildren<InventorySlot>().Length -1)
                {
                    full = true;
                }
                break;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRack : MonoBehaviour
{
    Dictionary<int, InventorySlot> rack = new Dictionary<int, InventorySlot>();
    public bool full;

    private void Start()
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
}

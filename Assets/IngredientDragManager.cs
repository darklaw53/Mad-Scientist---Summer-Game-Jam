using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDragManager : Singleton<IngredientDragManager>
{
    public Ingredient _ingredientInQuestion;
    public InventorySlot _inventorySlotInQuestion, mixingSlot;
    public GameObject avatarTemplate, worldCanvas;
    public ItemRack ingredientRack;
    public bool avatarIncarnated;
    public bool hoverinOverSlot;
    GameObject avatar;

    private void Update()
    {
        if (avatarIncarnated)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            avatar.transform.position = mousePosition;

            if (Input.GetMouseButtonUp(0))
            {
                Destroy(avatar);
                avatarIncarnated = false;

                if (hoverinOverSlot & mixingSlot.ing != null)
                {
                    mixingSlot.RecieveItem(_ingredientInQuestion);
                    _inventorySlotInQuestion.ing = null;
                }
                else if (_inventorySlotInQuestion != null)
                {
                    _inventorySlotInQuestion.RecieveItem(_ingredientInQuestion);
                }
                else
                {
                    ingredientRack.TakeItem(_ingredientInQuestion);
                }

                _ingredientInQuestion = null;
                _inventorySlotInQuestion = null;
            }
        }
    }

    public void DragItem(Ingredient ing, InventorySlot slot, bool isMixSlot)
    {
        _ingredientInQuestion = ing;
        if (!isMixSlot) _inventorySlotInQuestion = slot;

        avatar = Instantiate(avatarTemplate, worldCanvas.transform);
        avatar.GetComponent<Image>().sprite = _ingredientInQuestion.sprite;
        avatarIncarnated = true;
    }
}

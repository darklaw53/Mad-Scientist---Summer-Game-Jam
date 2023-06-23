using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDragManager : Singleton<IngredientDragManager>
{
    public Ingredient _ingredientInQuestion;
    public Formula _formulaInQuestion;
    public MeltObject _meltObjectInQuestion;

    public InventorySlot _inventorySlotInQuestion, mixingSlot, oldMixSlot;
    public GameObject avatarTemplate, worldCanvas;
    public ItemRack ingredientRack;
    public ItemRack formulaRack;
    public ItemRack objectRack;
    public bool avatarIncarnated;
    public bool hoverinOverSlot;
    public bool hoverinOverMeltZone;
    public MeltObjectScript hoverinOverMeltObject;
    GameObject avatar;

    public GameObject formulaRackOBJ;
    public GameObject ingredientRackOBJ;
    public GameObject objectRackOBJ;

    public GameObject soundEffect;

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

                if (hoverinOverSlot && mixingSlot != null && mixingSlot.ing == null && _ingredientInQuestion != null)
                {
                    Instantiate(soundEffect);
                    mixingSlot.RecieveItem(_ingredientInQuestion);
                    _ingredientInQuestion = null;
                    _inventorySlotInQuestion.ing = null;
                }
                else if (hoverinOverMeltZone && _meltObjectInQuestion != null)
                {
                    var x = Instantiate(_meltObjectInQuestion.corpusDArt, mousePosition, transform.rotation);
                    x.GetComponent<MeltObjectScript>().slot = _inventorySlotInQuestion;
                    _meltObjectInQuestion = null;
                    _inventorySlotInQuestion.meltObj = null;
                }
                else if (hoverinOverMeltObject != null && _formulaInQuestion)
                {
                    hoverinOverMeltObject.RecieveFormula(_formulaInQuestion);
                    _formulaInQuestion = null;
                    _inventorySlotInQuestion.formul = null;
                }
                else if (_inventorySlotInQuestion != null)
                {
                    if (_ingredientInQuestion)
                    {
                        _inventorySlotInQuestion.RecieveItem(_ingredientInQuestion);
                    }
                    else if (_formulaInQuestion)
                    {
                        _inventorySlotInQuestion.RecieveItem(_formulaInQuestion);
                    }
                    else if (_meltObjectInQuestion)
                    {
                        _inventorySlotInQuestion.RecieveItem(_meltObjectInQuestion);
                    }
                }
                else
                {
                    if (_ingredientInQuestion)
                    {
                        formulaRackOBJ.SetActive(false);
                        ingredientRackOBJ.SetActive(true);
                        objectRackOBJ.SetActive(false);
                        ingredientRack.TakeItem(_ingredientInQuestion);
                    }
                    else if (_meltObjectInQuestion)
                    {
                        formulaRackOBJ.SetActive(false);
                        ingredientRackOBJ.SetActive(false);
                        objectRackOBJ.SetActive(true);
                        objectRack.TakeItem(_meltObjectInQuestion);
                    }
                    oldMixSlot.ing = null;
                    oldMixSlot = null;
                }

                _ingredientInQuestion = null;
                _inventorySlotInQuestion = null;
            }
        }
    }

    public void DragItem(Ingredient ing, InventorySlot slot, bool isMixSlot)
    {
        _ingredientInQuestion = ing;

        if (!isMixSlot)
        {
            _inventorySlotInQuestion = slot;
        }
        else
        {
            oldMixSlot = slot;
        }

        avatar = Instantiate(avatarTemplate, worldCanvas.transform);
        avatar.GetComponent<Image>().sprite = _ingredientInQuestion.sprite;
        avatarIncarnated = true;
    }

    public void DragItem(Formula formul, InventorySlot slot)
    {
        _formulaInQuestion = formul;
        _inventorySlotInQuestion = slot;

        avatar = Instantiate(avatarTemplate, worldCanvas.transform);
        avatar.GetComponent<Image>().sprite = _formulaInQuestion.sprite;
        avatar.GetComponent<Image>().color = new Color((formul.ingredient1.color.r + formul.ingredient2.color.r) / 2, (formul.ingredient1.color.g + formul.ingredient2.color.g) / 2, (formul.ingredient1.color.b + formul.ingredient2.color.b) / 2, (formul.ingredient1.color.a + formul.ingredient2.color.a) / 2);
        avatarIncarnated = true;
    }

    public void DragItem(MeltObject meltObj, InventorySlot slot)
    {
        _meltObjectInQuestion = meltObj;
        _inventorySlotInQuestion = slot;

        avatar = Instantiate(avatarTemplate, worldCanvas.transform);
        avatar.GetComponent<Image>().sprite = _meltObjectInQuestion.sprite;
        avatarIncarnated = true;
    }
}

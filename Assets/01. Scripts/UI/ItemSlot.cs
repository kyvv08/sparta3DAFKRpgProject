using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Consumable,
    Equipable
}

public enum StatType
{
    HP,MP,EXP
}

public enum EquipType
{
    Weapon,Armor
}

[CreateAssetMenu(fileName = "Item",menuName = "ItemData")]
public class ItemData : ScriptableObject
{
    [Header("아이템에 들어갈 정보 Info")]
    public string itemName;
    //public string displayName;
    public ItemType type;
    public Sprite icon;
    public StatType statType;
    public EquipType equipType;
    public uint statValue;
    public uint price;
}

public class ItemSlot : MonoBehaviour
{
    public ItemData item;
    
    public InventoryUIController inventory;
    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText;  
    public TextMeshProUGUI equippedText;  
    private Outline outline;             

    public int index;                    
    public bool equipped;                
    public int quantity;                 

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }
    
    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        quatityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if(equippedText != null)
        {
            equippedText.enabled = equipped;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        equippedText.text = string.Empty;
        quatityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }
}

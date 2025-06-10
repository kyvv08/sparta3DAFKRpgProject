using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    public ItemSlot[] slots;

    public GameObject inventoryWindow;
    public Transform slotPanel;

    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;
    public GameObject useButton;
    public GameObject equipButton;
    public GameObject sellButton;

    private int curEquipIndex;

    private uint gold;
    
    void Start()
    {
        inventoryWindow.SetActive(false);
        slots = new ItemSlot[slotPanel.childCount];

        for(int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].inventory = this;
            slots[i].Clear();
        }

        ClearSelectedItemWindow();
        
        PlayerManager.Instance.player.UpdatePlayerUI += UpdatePlayerUI;
        
        UpdatePlayerUI();
        UpdateGameUI();
    }

	void ClearSelectedItemWindow()
    {
        selectedItem = null;

        selectedItemName.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        useButton.SetActive(false);
        equipButton.SetActive(false);
        sellButton.SetActive(false);
    }
        
    public void Toggle()
    {
        if (IsOpen())
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }

		
    public void AddItem(ItemData data, uint addGold = 0)
    {
        ItemSlot slot = GetItemStack(data);
        gold += addGold;
        if (slot != null)
        {
            slot.quantity++;
            UpdateSlotUI();
            return;
        }
        
        ItemSlot emptySlot = GetEmptySlot();
        
        if(emptySlot != null)
        {
            emptySlot.item = data;
            emptySlot.quantity = 1;
            UpdateSlotUI();
        }
        
    }
    
    public void UpdateSlotUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
    }
    
    ItemSlot GetItemStack(ItemData data)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == data)
            {
                return slots[i];
            }
        }
        return null;
    }
    
    ItemSlot GetEmptySlot()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }

    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.item.displayName;

        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

   
        selectedItemStatName.text += selectedItem.item.statType;
        selectedItemStatValue.text += selectedItem.item.statValue;

        useButton.SetActive(true);
        useButton.GetComponent<Button>().interactable = selectedItem.item.type == ItemType.Consumable;
        equipButton.SetActive(true);
        equipButton.GetComponent<Button>().interactable = selectedItem.item.type == ItemType.Equipable;
        sellButton.SetActive(true);
    }

    public void OnUseButton()
    {
        if(selectedItem.item.type == ItemType.Consumable)
        {
            switch (selectedItem.item.statType)
            {
                case StatType.HP:
                {
                    PlayerManager.Instance.player.UseHPItem(selectedItem.item.statValue);
                    break;
                }
                case StatType.MP:
                {
                    PlayerManager.Instance.player.UseMPItem(selectedItem.item.statValue);
                    break;
                }
                case StatType.EXP:
                {
                    PlayerManager.Instance.player.UseExpItem(selectedItem.item.statValue);
                    break;
                }
            }
            RemoveSelctedItem();
        }
    }

    public void OnSellButton()
    {
        SellItem(selectedItem.item);
        RemoveSelctedItem();
    }

    void RemoveSelctedItem()
    {
        selectedItem.quantity--;

        if(selectedItem.quantity <= 0)
        {
            if (slots[selectedItemIndex].equipped)
            {
                //UnEquip(selectedItemIndex);
            }

            selectedItem.item = null;
            ClearSelectedItemWindow();
        }

        UpdateSlotUI();
    }

    public bool HasItem(ItemData item, int quantity)
    {
        return false;
    }

    private void SellItem(ItemData item)
    {
        gold += item.price;
        UpdateGameUI();
    }
    
    private void UpdateGameUI()
    {
        UIManager.Instance.gameInfoUI.SetCurMoney(gold);
    }

    public void UpdatePlayerUI()
    {
        float hp = (float)PlayerManager.Instance.player.playerRealStat.CurHP / 
            PlayerManager.Instance.player.PlayerStat.BaseStat.MaxHP;
        float mp = (float)PlayerManager.Instance.player.playerRealStat.CurMP / 
            PlayerManager.Instance.player.PlayerStat.BaseStat.MaxMP;
        float exp = (float)PlayerManager.Instance.player.playerRealStat.CurExp / 
            PlayerManager.Instance.player.PlayerStat.BaseStat.ExpToNextLevel;
        
        UIManager.Instance.playerUI.SetHp(hp);
        UIManager.Instance.playerUI.SetMp(mp);
        UIManager.Instance.playerUI.SetExp(exp);
        
        UIManager.Instance.playerUI.SetLevel(PlayerManager.Instance.player.playerRealStat.Level);
    }
}

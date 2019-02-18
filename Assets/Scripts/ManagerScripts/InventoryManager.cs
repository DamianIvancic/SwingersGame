﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform UISlotsParent;

    private Dictionary<string, List<Item>> inventory;
    private InventorySlot[] slots;

    public delegate void OnItemChanged(); //38 times faster than UnityEvent
    public OnItemChanged OnItemChangedCallback;


    public void Initialize() //used by GameManager.Awake() instead of InventoryManager.Start() since the object is inactive at the beginning
    {
        inventory = new Dictionary<string, List<Item>>();
        slots = UISlotsParent.GetComponentsInChildren<InventorySlot>();

        OnItemChangedCallback += UpdateUI;
        SceneManager.sceneLoaded += OnSceneLoadedListener;
    }


    void OnSceneLoadedListener(Scene scene, LoadSceneMode mode)
    {
        Clear();       
    }

    void UpdateUI() //gets called whenever OnItemChangedCallback is invoked
    {
        int index = 0;

        foreach(string ItemName in inventory.Keys)
        {
            slots[index].UpdateItem(inventory[ItemName][0], inventory[ItemName].Count);
            index++;
        }

        if(slots.Length > inventory.Count)
        {
            do
            {           
                slots[index].ClearSlot();
                index++;

            } while (index < slots.Length);
        }       
    }


    public bool Add(Item item) // called by ItemPickup.DoOnInteract() which is triggered when colliding with player
    {
        if ((inventory.ContainsKey(item.ItemName) && inventory[item.ItemName].Count == 99) || (!inventory.ContainsKey(item.ItemName) && inventory.Count == slots.Length))
        {
            Debug.Log("Add()" + item.name + " false");
            return false;
        }

        if (!inventory.ContainsKey(item.ItemName) && inventory.Count < slots.Length)
            inventory.Add(item.ItemName, new List<Item>());


        inventory[item.ItemName].Add(item);
       
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();

        Debug.Log("Add()" + item.name + " true");
        return true;
    }


    public void Remove(Item item) //called by the GUI buttons on the inventory slots
    {
        Debug.Log("InventoryManager.Remove(" + item.ItemName + ")");
        inventory[item.ItemName].RemoveAt(inventory[item.ItemName].Count - 1);

        if (inventory[item.ItemName].Count == 0)
            inventory.Remove(item.ItemName);
    
        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }

    public void Clear()
    {
        inventory.Clear();

        foreach(InventorySlot slot in slots)
        {
            slot.ClearSlot();
        }
    }

    public int GetCount(Item item)
    {
        return inventory[item.ItemName].Count;
    }

    public int GetCount(string itemName)
    {
        return inventory[itemName].Count;
    }

    public bool Contains(string key)
    {
        return inventory.ContainsKey(key);
    }
}

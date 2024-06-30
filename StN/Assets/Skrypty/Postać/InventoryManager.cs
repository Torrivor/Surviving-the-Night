using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Weapons> weaponSlots = new List<Weapons>(6);  //sloty na bron np 6 mozna zmienic
    public int[] weaponLevels = new int[6];
    public List<PassiveItem> passiveItemsSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];

    public void AddWeapon(int slotIndex, Weapons weapon)
    {
        weaponSlots[slotIndex] = weapon;
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemsSlots[slotIndex] = passiveItem;
    }

    public void LevelUpWeapon(int slotIndex)
    {

    }

    public void LevelUpPassiveItem(int slotIndex)
    {

    }
}

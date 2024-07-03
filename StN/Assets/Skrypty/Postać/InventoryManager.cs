using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public List<Weapons> weaponSlots = new List<Weapons>(6);  //sloty na bron np 6 mozna zmienic
    public int[] weaponLevels = new int[6];
    public List<Image> weaponUISlots = new List<Image>(6);
    public List<PassiveItem> passiveItemsSlots = new List<PassiveItem>(6);
    public int[] passiveItemLevels = new int[6];
    public List<Image> passiveItemUISlots = new List<Image>(6);

    [System.Serializable]
    public class WeaponUpgrade
    {
        public GameObject initialWeapon;
        public WeaponsScriptableObject weaponData;
    }

    [System.Serializable]
    public class PassiveItemUpgrade
    {
        public GameObject initialPassiveItem;
        public PassiveItemScriptableObject passiveItemData;
    }

    [System.Serializable]
    public class UpgradeUI
    {
        public Text upgradeNameDisplay;
        public Text upgradeDescriptionDisplay;
        public Image upgradeIcon;
        public Button upgradeButton;
    }

    public List<WeaponUpgrade> weaponUpgradeOptions = new List<WeaponUpgrade>();     //Lista z ulepszeniami broni
    public List<PassiveItemUpgrade> passiveItemUpgradeOptions = new List<PassiveItemUpgrade>();
    public List<UpgradeUI> upgradeUIOptions = new List<UpgradeUI>();

    PlayerStats player;

    void Start()
    {
        player = GetComponent<PlayerStats>();
    }

    public void AddWeapon(int slotIndex, Weapons weapon)
    {
        weaponSlots[slotIndex] = weapon;
        weaponLevels[slotIndex] = weapon.weaponData.Level;
        weaponUISlots[slotIndex].enabled = true;    //wlancza obrazek gdy jest w uzyciu slot
        weaponUISlots[slotIndex].sprite = weapon.weaponData.Icon;

        if(GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void AddPassiveItem(int slotIndex, PassiveItem passiveItem)
    {
        passiveItemsSlots[slotIndex] = passiveItem;
        passiveItemLevels[slotIndex] = passiveItem.passiveItemData.Level;
        passiveItemUISlots[slotIndex].enabled = true;    //wlancza obrazek gdy jest w uzyciu slot
        passiveItemUISlots[slotIndex].sprite = passiveItem.passiveItemData.Icon;

        if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
        {
            GameManager.instance.EndLevelUp();
        }
    }

    public void LevelUpWeapon(int slotIndex)
    {
        if(weaponSlots.Count > slotIndex)
        {
            Weapons weapon = weaponSlots[slotIndex];
            if(!weapon.weaponData.NextLevelPrefab) //sprawdza czy jest nastepny lvl dla broni
            {
                Debug.LogError("brak nastepnego lvl dla" + weapon.name);
                return;
            }
            GameObject upgradedWeapon = Instantiate(weapon.weaponData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedWeapon.transform.SetParent(transform); //ustawia bron jako dziecko dla player
            AddWeapon(slotIndex, upgradedWeapon.GetComponent<Weapons>());
            Destroy(weapon.gameObject);
            weaponLevels[slotIndex] = upgradedWeapon.GetComponent<Weapons>().weaponData.Level;  //zeby upewnic sie ze mamy wlasciwy lvl

            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    public void LevelUpPassiveItem(int slotIndex)
    {
        if (passiveItemsSlots.Count > slotIndex)
        {
            PassiveItem passiveItem = passiveItemsSlots[slotIndex];
            if (!passiveItem.passiveItemData.NextLevelPrefab) //sprawdza czy jest nastepny lvl dla pass
            {
                Debug.LogError("brak nastepnego lvl dla" + passiveItem.name);
                return;
            }
            GameObject upgradedPassiveItem = Instantiate(passiveItem.passiveItemData.NextLevelPrefab, transform.position, Quaternion.identity);
            upgradedPassiveItem.transform.SetParent(transform); //ustawia bron jako dziecko dla player
            AddPassiveItem(slotIndex, upgradedPassiveItem.GetComponent<PassiveItem>());
            Destroy(passiveItem.gameObject);
            passiveItemLevels[slotIndex] = upgradedPassiveItem.GetComponent<PassiveItem>().passiveItemData.Level;  //zeby upewnic sie ze mamy wlasciwy lvl pasywki

            if (GameManager.instance != null && GameManager.instance.choosingUpgrade)
            {
                GameManager.instance.EndLevelUp();
            }
        }
    }

    void ApplyUpgradeOptions()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            int upgradeType = Random.Range(1, 3);  //wybor miedzy pasywka a bron
            if(upgradeType == 1)
            {
                WeaponUpgrade chosenWeaponUpgrade = weaponUpgradeOptions[Random.Range(0, weaponUpgradeOptions.Count)];

                if(chosenWeaponUpgrade != null)
                {
                    bool newWeapon = false;
                    for (int i = 0; i < weaponSlots.Count; i++)
                    {
                        if (weaponSlots[i] != null && weaponSlots[i].weaponData == chosenWeaponUpgrade.weaponData)
                        {
                            newWeapon = false ;
                            if(!newWeapon)
                            {
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpWeapon(i));  //dodaje funkcje przycisku
                                //ustaw nazwe i opis nastepnego levela
                                upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<Weapons>().weaponData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.NextLevelPrefab.GetComponent<Weapons>().weaponData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newWeapon = true ;
                        }
                    }
                    if(newWeapon) //daje nowa bron
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnWeapon(chosenWeaponUpgrade.initialWeapon));
                        //ustaw poczatkowe imie i opis
                        upgradeOption.upgradeDescriptionDisplay.text = chosenWeaponUpgrade.weaponData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenWeaponUpgrade.weaponData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenWeaponUpgrade.weaponData.Icon;
                }
            }
            else if(upgradeType == 2)
            {
                PassiveItemUpgrade chosenPassiveItemUpgrade = passiveItemUpgradeOptions[Random.Range(0, passiveItemUpgradeOptions.Count)];

                if(chosenPassiveItemUpgrade != null)
                {
                    bool newPassiveItem = false;
                    for(int i = 0;i < passiveItemsSlots.Count;i++)
                    {
                        if (passiveItemsSlots[i] != null && passiveItemsSlots[i].passiveItemData == chosenPassiveItemUpgrade.passiveItemData)
                        {
                            newPassiveItem = false ;

                            if(!newPassiveItem)
                            {
                                upgradeOption.upgradeButton.onClick.AddListener(() => LevelUpPassiveItem(i));
                                //ustaw nazwe i opis nastepnego levela
                                upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Description;
                                upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.NextLevelPrefab.GetComponent<PassiveItem>().passiveItemData.Name;
                            }
                            break;
                        }
                        else
                        {
                            newPassiveItem = true ;
                        }
                    }
                    if(newPassiveItem)
                    {
                        upgradeOption.upgradeButton.onClick.AddListener(() => player.SpawnPassiveItem(chosenPassiveItemUpgrade.initialPassiveItem));
                        //ustaw poczatkowe imie i opis
                        upgradeOption.upgradeDescriptionDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Description;
                        upgradeOption.upgradeNameDisplay.text = chosenPassiveItemUpgrade.passiveItemData.Name;
                    }

                    upgradeOption.upgradeIcon.sprite = chosenPassiveItemUpgrade.passiveItemData.Icon;
                }
            }
        }
    }

    void RemoveUpgradeOption()
    {
        foreach (var upgradeOption in upgradeUIOptions)
        {
            upgradeOption.upgradeButton.onClick.RemoveAllListeners();
        }
    }

    public void RemoveAndApplyUpgrades()
    {
        RemoveUpgradeOption();
        ApplyUpgradeOptions();
    }
}
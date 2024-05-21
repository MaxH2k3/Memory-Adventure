using Assets.Scripts.Enums;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBag : Singleton<ActiveBag>
{
    private int activeSlotIndexNum = 0;
    public Transform currentSlot { get; private set; }

    private PlayerControls playerControls;

    protected override void Awake()
    {
        base.Awake();

        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Start()
    {
        playerControls.Inventory.SwitchActiveBag.performed += ctx => ToggleActiveBag((int)ctx.ReadValue<float>());

        initWeaponInventory();

        ToggleActiveBag(activeSlotIndexNum + 1);
    }

    public void EquipStartingWeapon()
    {
        ToggleActiveBag(0);
    }

    private void ToggleActiveBag(int indexSlot)
    {
        // Turn off the current active bag
        transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(false);

        // Set the new active bag
        activeSlotIndexNum = indexSlot - 1;

        transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);

        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        // Destroy the current weapon
        ActiveWeapon.Instance.DestroyCurrentWeapon();

        // If the active slot is empty, return
        if (transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo() == null)
        {
            Debug.Log("No weapon in this slot");
            ActiveWeapon.Instance.WeaponNull();
            return;
        }
        
        // Get the current slot
        currentSlot = transform.GetChild(activeSlotIndexNum);
        // Get the weapon prefab from the active slot
        GameObject weaponSlot = currentSlot.GetComponentInChildren<InventorySlot>().GetWeaponInfo().weaponPrefab;
        // Instantiate the weapon prefab
        //GameObject newWeapon = Instantiate(weaponSlot, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        GameObject newWeapon = Instantiate(weaponSlot, ActiveWeapon.Instance.transform.position, Quaternion.identity);

        // Set the weapon prefab to the active weapon
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
        newWeapon.transform.SetParent(ActiveWeapon.Instance.transform);
        
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
        
    }

    public void UpdateInventorySlot()
    {
        // Change the weapon sprite in the inventory slot
        currentSlot.GetChild(1).GetComponent<Image>().sprite = ManageWeaponPlayer.Instance.weaponManager.SelectWeapon(WeaponType.Sword).GetComponent<SpriteRenderer>().sprite;
        // Change the weapon prefab in the inventory slot
        currentSlot.GetComponentInChildren<InventorySlot>().SetWeaponPrefab(ManageWeaponPlayer.Instance.weaponManager.SelectWeapon(WeaponType.Sword));
    }

    public void initWeaponInventory()
    {
        WeaponType[] weaponTypes = (WeaponType[])Enum.GetValues(typeof(WeaponType));

        for(int i = 0; i < weaponTypes.Length; i++)
        {
            // Get the current slot
            var currentSlot = transform.GetChild(i);
            // Get the weapon prefab
            var weaponPrefab = ManageWeaponPlayer.Instance.weaponManager.SelectWeapon(weaponTypes[i]);
            // Set the weapon sprite in the inventory slot
            currentSlot.GetChild(1).GetComponent<Image>().sprite = weaponPrefab.GetComponent<SpriteRenderer>().sprite;
            // Set the weapon prefab in the inventory slot
            currentSlot.GetComponentInChildren<InventorySlot>().SetWeaponPrefab(weaponPrefab);
        }
    }

}

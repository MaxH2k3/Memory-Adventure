using Assets.Scripts.Enums;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeaponPlayer : Singleton<ManageWeaponPlayer>
{
    public WeaponManager weaponManager { get; private set; }
    public bool upgradeWeapon = false;
    public int conditions = 3;

    private Dictionary<WeaponType, int> recordFarm = new Dictionary<WeaponType, int>
    {
        { WeaponType.Sword, 1 },
        { WeaponType.Bow, 1 },
        { WeaponType.Staff, 1},
        { WeaponType.Boomerang, 1},
        { WeaponType.Boom, 1}
    };

    protected override void Awake()
    {
        base.Awake();

        weaponManager = GetComponent<WeaponManager>();

        
    }

    public void UpdateWeapon(int amount)
    {
        IncreaseFarm(ActiveBag.Instance.currentWeapon.weaponType, amount);
        if (recordFarm[ActiveBag.Instance.currentWeapon.weaponType] >= conditions)
        {
            weaponManager.UpgradeWeapon(ActiveBag.Instance.currentWeapon.weaponType);
            ActiveBag.Instance.UpdateInventorySlot();
        }
    }

    public void IncreaseFarm(WeaponType weaponType, int amount)
    {
        recordFarm[weaponType] += amount;
    }

}

using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public WeaponDatabase weaponDatabase;
    
    private Dictionary<WeaponType, int> weapons = new Dictionary<WeaponType, int>
    {
        { WeaponType.Sword, 1 },
        { WeaponType.Bow, 1 },
        { WeaponType.Staff, 1},
        { WeaponType.Boomerang, 1},
        { WeaponType.Boom, 1}
    };

    /// <summary>
    /// Get the weapon prefab
    /// </summary>
    /// <param name="weaponType">Type of weapon</param>
    /// <param name="index">Index of weapon</param>
    /// <returns></returns>
    public GameObject SelectWeapon(WeaponType weaponType)
    {
        if(weaponType == WeaponType.Sword)
        {
            return weaponDatabase.GetWeaponSword(weapons[WeaponType.Sword] - 1).weaponPrefab;
        } else if(weaponType == WeaponType.Staff)
        {
            return weaponDatabase.GetWeaponStaff(weapons[WeaponType.Staff] - 1).weaponPrefab;
        } else if(weaponType == WeaponType.Bow)
        {
            return weaponDatabase.GetWeaponBow(weapons[WeaponType.Bow] - 1).weaponPrefab;
        } else if(weaponType == WeaponType.Boomerang)
        {
            return weaponDatabase.GetWeaponBoomerang(weapons[WeaponType.Boomerang] - 1).weaponPrefab;
        } else if(weaponType == WeaponType.Boom)
        {
            return weaponDatabase.GetWeaponBoom(weapons[WeaponType.Boom] - 1).weaponPrefab;
        }

        return null;
    }

    /// <summary>
    /// Get the number of weapon type
    /// </summary>
    /// <returns></returns>
    public int GetNumberOfWeaponType()
    {
        return Enum.GetValues(typeof(WeaponType)).Length;
    }

    /// <summary>
    /// Get the number of weapon in each type
    /// </summary>
    /// <param name="weaponType"></param>
    /// <returns></returns>
    public int GetNumberOfWeapon(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Sword)
        {
            return weaponDatabase.swords.Length;
        } else if (weaponType == WeaponType.Staff)
        {
            return weaponDatabase.staffs.Length;
        } else if (weaponType == WeaponType.Bow)
        {
            return weaponDatabase.bows.Length;
        } else if (weaponType == WeaponType.Boomerang)
        {
            return weaponDatabase.boomerangs.Length;
        } else if (weaponType == WeaponType.Boom)
        {
            return weaponDatabase.booms.Length;
        }

        return 0;
    }

    /// <summary>
    /// Upgrade the weapon and set the upgrade weapon to true
    /// </summary>
    /// <param name="weaponType"></param>
    public void UpgradeWeapon(WeaponType weaponType)
    {
        if(weaponDatabase.MaxLevelWeapon(weaponType) > weapons[weaponType])
        {
            weapons[weaponType] += 1;
        }
        
    }

}

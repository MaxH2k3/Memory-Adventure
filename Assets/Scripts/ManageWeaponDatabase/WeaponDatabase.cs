using Assets.Scripts.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "Database/WeaponDatabase")]
public class WeaponDatabase : ScriptableObject
{
    public Weapon[] swords;
    public Weapon[] staffs;
    public Weapon[] bows;
    public Weapon[] boomerangs;
    public Weapon[] booms;

    public Weapon GetWeaponSword(int index)
    {
        try
        {
            return swords[index];
        } catch
        {
            return swords[0];
        }
    }

    public Weapon GetWeaponStaff(int index)
    {
        try
        {
            return staffs[index];
        } catch
        {
            return staffs[0];
        }
    }

    public Weapon GetWeaponBow(int index)
    {
        try
        {
            return bows[index];
        } catch
        {
            return bows[0];
        }
    }

    public Weapon GetWeaponBoomerang(int index)
    {
        try
        {
            return boomerangs[index];
        } catch
        {
            return boomerangs[0];
        }
    }

    public Weapon GetWeaponBoom(int index)
    {
        try
        {
            return booms[index];
        } catch
        {
            return booms[0];
        }
    }

    public int MaxLevelWeapon(WeaponType weaponType)
    {
        if (weaponType == WeaponType.Sword)
        {
            return swords.Length;
        }
        else if (weaponType == WeaponType.Staff)
        {
            return staffs.Length;
        }
        else if (weaponType == WeaponType.Bow)
        {
            return bows.Length;
        }
        else if (weaponType == WeaponType.Boomerang)
        {
            return boomerangs.Length;
        }
        else if (weaponType == WeaponType.Boom)
        {
            return booms.Length;
        }

        return 0;
    }

}

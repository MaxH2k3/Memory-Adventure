using Assets.Scripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Đây là một thuộc tính được sử dụng để tạo một menu trong Unity để tạo mới một đối tượng GetWeaponInfo. 
/// Khi bạn nhấp chuột phải trong Project Window và chọn "Create" -> "New Weapon", 
/// Unity sẽ tạo một đối tượng GetWeaponInfo mới với tên "GetWeaponInfo" và đặt nó trong thư mục được chọn.
/// </summary>
/// <remarks>
/// Đây là một class chứa thông tin về vũ khí, bao gồm tên prefab, 
/// và thời gian cooldown giữa các lần bắn.
/// </remarks>
[CreateAssetMenu(fileName = "GetWeaponInfo", menuName = "New Weapon")]
public class WeaponInfo : ScriptableObject
{
    public GameObject weaponPrefab;
    public float weaponCooldown;
    public int weaponDamage;
    public float weaponRange;
    public WeaponType weaponType;
}

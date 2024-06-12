using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    public string weaponName;
    public GameObject weaponPrefab;
    [SerializeField]public GameObject skillPrefab;
}

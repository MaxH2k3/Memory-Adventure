using Assets.Scripts.Enums;
using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimationPrefab; // This is the slash animation prefab
    [SerializeField] private Transform slashPoint; // This is the spawn point where the slash animation will be instantiated
    [SerializeField] private WeaponInfo weaponInfo;

    private Transform weaponCollider; // This is the collider of the weapon
    private Animator animator;
    private GameObject slashAnimation; // This is the instantiated slash animation
    

    public WeaponInfo GetWeaponInfo() => weaponInfo;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashPoint = GameObject.Find("SlashSpawnPoint").transform;
    }

    private void Update()
    {
        FlipItem();

        
    }

    public void Attack()
    {
        animator.SetTrigger("Attack"); // trigger the attack animation
        weaponCollider.gameObject.SetActive(true); // enable the weapon collider which allows weapons to collide with other objects in the game

        slashAnimation = Instantiate(slashAnimationPrefab, slashPoint.position, Quaternion.identity); // instantiate the slash animation at the slash point and set its rotation to the identity quaternion
        slashAnimation.transform.SetParent(slashPoint.parent); // set the slash animation's parent to the slash point's parent. This is done so that the slash animation will follow the player's movement


            /*Vector3 cursorScreenPosition = Input.mousePosition;
            Vector3 cursorWorldPosition = Camera.main.ScreenToWorldPoint(cursorScreenPosition);
            cursorWorldPosition.z = 0; // Set z to 0 since we're working in 2D
            var skill = Instantiate(ManageWeaponPlayer.Instance.weaponManager.SelectSkillWeapon(WeaponType.Sword), cursorWorldPosition, Quaternion.identity);
            Destroy(skill, 2f);*/
    }

    public void DoneAttackingAnimation()
    {
        weaponCollider.gameObject.SetActive(false); // disable the weapon collider
        //Destroy(slashAnimation);
    }

    public void SwingUpFlipAnimation()
    {
        slashAnimation.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if(PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void SwingDownFlipAnimation()
    {
        slashAnimation.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnimation.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    public void FlipItem()
    {
        // Get the mouse position
        Vector3 mousePos = Input.mousePosition;

        // Convert the mouse position to world point and then to screen point. This helps us know the mouse position relative to the weapon's position.
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        // Calculate the angle between the weapon and the mouse position. This helps us rotate the weapon towards the mouse position.
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // If the mouse position is on the left side of the weapon, flip the weapon to face the left side. Otherwise, flip the weapon to face the right side.
        if (mousePos.x < screenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        } else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }


    }

}

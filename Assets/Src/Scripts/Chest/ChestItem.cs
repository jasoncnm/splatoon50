using UnityEngine;

public class ChestItem : Interactable
{
    public DropItemsSO dropItems;
 
    [SerializeField] Animator chestAnimator;


    public override void ReadyToInteract(Transform interactIcon)
    {
        base.ReadyToInteract(interactIcon);

    }

    public override void Interact()
    {
        base.Interact();
        chestAnimator.SetTrigger("Open");
    }

    public void Drop()
    {
 
        Transform item = null;

        Transform gunTr = GameObject.FindAnyObjectByType<PlayerGunController>().GunTr();

        if (gunTr.GetComponent<GunProperties>().type == Util.GunClass.PISTOL)
        {
            int index = Random.Range(0, dropItems.pistolUpgrades.Length);

            item = Instantiate(dropItems.pistolUpgrades[index]);

        }

        if (item)
        {
            Vector3 offset = new Vector3(0f, 0f);

            item.position = transform.position + offset;
        }
        Debug.Log("Drop");
    }

}

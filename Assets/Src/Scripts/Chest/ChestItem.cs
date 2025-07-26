using UnityEngine;

public class ChestItem : Interactable
{

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
        Transform gunTr = GameManager.instance.player.GetComponent<PlayerGunController>().gunTr;

        Transform item = null;

        if (gunTr.name == "Gun_Pistol")
        {
            int index = Random.Range(0, GameManager.instance.pistolUpgrades.Length);

            item = Instantiate(GameManager.instance.pistolUpgrades[index]);

        }

        if (item)
        {
            Vector3 offset = new Vector3(0f, 0f);

            item.position = transform.position + offset;
        }
        Debug.Log("Drop");
    }

}

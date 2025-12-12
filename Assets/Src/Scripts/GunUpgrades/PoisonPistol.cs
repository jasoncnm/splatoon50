using UnityEngine;

public class PoisonPistol : Interactable
{

    public override void ReadyToInteract(Transform interactIcon)
    {
        base.ReadyToInteract(interactIcon);
        interactIcon.transform.position = transform.position + 1.0f * Vector3.up;
    }


    public override void Interact()
    {
        base.Interact();
        GetComponent<Animator>().SetTrigger("PickUp");
        player.GetComponent<PlayerGunController>().SetGunElemental(ElementalDamage.POSION);
    }

}

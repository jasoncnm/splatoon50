using UnityEngine;

public class IcePistol : Interactable
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
        GameManager.instance.player.GetComponent<PlayerGunController>().gunProperties.elementalDamage = ElementalDamage.ICE;
    }

}

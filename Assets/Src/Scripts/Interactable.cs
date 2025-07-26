using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    Transform interactIcon = null;

    public virtual void ReadyToInteract(Transform interactIcon)
    {
        this.interactIcon = interactIcon;
        interactIcon.gameObject.SetActive(true);
        interactIcon.transform.position = transform.position + 2.0f * Vector3.up;
    }
    public virtual void Interact()
    {
        interactIcon.GetComponent<Animator>().SetTrigger("Open");
    }
}

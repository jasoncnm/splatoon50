using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    Transform interactIcon = null;
    float lifeTime = 15f;

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

    public virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

}

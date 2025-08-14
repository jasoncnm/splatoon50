
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunClassButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    public enum GunClass
    {
        PISTOL, AR, MAGNUM, SMG, SNIPER, NONE
    }

    [SerializeField] GameObject[] gunClasses;

    [SerializeField] GunClass id;


    private void Start()
    {
        if (id == GunClass.PISTOL)
        {
            GetComponent<Button>().Select();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted
        for (int i = 0; i < gunClasses.Length; i++)
        {
            if (i == (int)id)
            {
                gunClasses[i].SetActive(true);
            }
            else
            {
                gunClasses[i].SetActive(false);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected

        switch (id)
        {
            case GunClass.PISTOL:
                {
                    GameManager.startGunName = "Gun_Pistol";
                    GameManager.playerHealth = 3;
                    break;
                }
            case GunClass.AR:
                {
                    GameManager.startGunName = "Gun_AR";
                    GameManager.playerHealth = 5;
                    break;
                }
            case GunClass.MAGNUM:
                {
                    GameManager.startGunName = "Gun_Magnum";
                    GameManager.playerHealth = 3;
                    break;
                }
            case GunClass.SMG:
                {
                    GameManager.startGunName = "Gun_SMG";
                    GameManager.playerHealth = 2;
                    break;
                }
            case GunClass.SNIPER:
                {
                    GameManager.startGunName = "Gun_Sniper";
                    GameManager.playerHealth = 2;
                    break;
                }
        }
    }
}

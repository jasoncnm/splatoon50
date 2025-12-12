
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunClassButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    public GlobalGameStateSO gameGlobal;
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
                    gameGlobal.startGunName = "Gun_Pistol";
                    gameGlobal.playerHealth = 3;
                    break;
                }
            case GunClass.AR:
                {
                    gameGlobal.startGunName = "Gun_AR";
                    gameGlobal.playerHealth = 5;
                    break;
                }
            case GunClass.MAGNUM:
                {
                    gameGlobal.startGunName = "Gun_Magnum";
                    gameGlobal.playerHealth = 3;
                    break;
                }
            case GunClass.SMG:
                {
                    gameGlobal.startGunName = "Gun_SMG";
                    gameGlobal.playerHealth = 2;
                    break;
                }
            case GunClass.SNIPER:
                {
                    gameGlobal.startGunName = "Gun_Sniper";
                    gameGlobal.playerHealth = 2;
                    break;
                }
        }
    }
}

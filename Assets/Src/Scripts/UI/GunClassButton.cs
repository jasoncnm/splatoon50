
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GunClassButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{

    [SerializeField] Util.GunClass id;
    [SerializeField] RectTransform gunClassNode;

    public GlobalGameStateSO gameGlobal;
    public GunsSO gunData;



    List<GameObject> gunClassDescriptionList = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < gunClassNode.childCount; i++)
        {
            gunClassDescriptionList.Add(gunClassNode.GetChild(i).gameObject);
        }
        
        if (id == Util.GunClass.PISTOL)
        {
            GetComponent<Button>().Select();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //do your stuff when highlighted
        for (int i = 0; i < gunClassDescriptionList.Count; i++)
        {
            if (i == (int)id)
            {
                gunClassDescriptionList[i].SetActive(true);
            }
            else
            {
                gunClassDescriptionList[i].SetActive(false);
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        //do your stuff when selected
        gunData.selectedGunIndex = (int)id;
        switch (id)
        {
            case Util.GunClass.PISTOL:
                {
                    gameGlobal.playerHealth = 3;
                    break;
                }
            case Util.GunClass.AR:
                {
                    gameGlobal.playerHealth = 5;
                    break;
                }
            case Util.GunClass.MAGNUM:
                {
                    gameGlobal.playerHealth = 3;
                    break;
                }
            case Util.GunClass.SMG:
                {
                    gameGlobal.playerHealth = 2;
                    break;
                }
            case Util.GunClass.SNIPER:
                {
                    gameGlobal.playerHealth = 2;
                    break;
                }
        }
    }
}

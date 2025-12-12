using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    public GlobalGameStateSO gameGlobal;
    public void OnAniEnd()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CoinCollector"))
        {
            GetComponent<Animator>().SetTrigger("PickUp");
            gameGlobal.money++;
        }
    }
}

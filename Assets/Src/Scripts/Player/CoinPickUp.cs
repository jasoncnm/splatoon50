using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            collision.GetComponent<Animator>().SetTrigger("PickUp");
        }

    }
}

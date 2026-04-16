using UnityEngine;

public class VictoryZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            GameObject.Find("Canvas").GetComponent<UI>().ShowEndScreen(true);
        }
        else
            Destroy(collision.gameObject);
    }
}
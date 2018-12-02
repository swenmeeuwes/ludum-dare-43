using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (transform.position.y + transform.localScale.y / 2f > collision.transform.position.y + collision.transform.localScale.y / 2f)
            {
                var player = collision.GetComponent<Player>();
                if (player != null && player.enabled)
                {
                    player.Kill();
                }
            }
        }
    }
}

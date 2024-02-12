using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Bat‚É“–‚½‚Á‚Ä‚¢‚é‚©‚ğ”»’f
        if (collision.CompareTag("Bat"))
        {
            //’e‚ğíœ
            Destroy(gameObject);
        }
    }
}

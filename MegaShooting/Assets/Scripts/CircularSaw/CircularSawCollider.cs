using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSawCollider : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        //ƒvƒŒƒCƒ„[‚É“–‚½‚Á‚½‚©‚ğ”»’f
        if(other.gameObject.CompareTag("Player"))
        {
            //CircularSaw‚ğíœ
            Destroy(gameObject);
        }
    }
}

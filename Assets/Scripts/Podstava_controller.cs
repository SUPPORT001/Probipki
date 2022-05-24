using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Podstava_controller : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Взод триггера");
        GameObject game = collision.gameObject;
        game.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("Выход тригера");
        GameObject game = collision.gameObject;
        game.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
    }
}

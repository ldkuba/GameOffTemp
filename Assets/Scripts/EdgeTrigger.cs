using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeTrigger : MonoBehaviour
{
    public bool jumpRight;
    public bool jumpLeft;
    public bool fallRight;
    public bool fallLeft;

    private GameObject collisionLock;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (collisionLock != null)
            {
                if (collision.gameObject.Equals(collisionLock))
                {
                    return;
                }
            }

            collisionLock = collision.gameObject;
            AIController controller = (AIController)collision.gameObject.GetComponent<AIController>();
            controller.EdgeTriggerEvent(this);
        }   
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionLock = null;
    }

    public bool bothLeft() { return jumpLeft && fallLeft; }
    public bool bothRight() { return jumpRight && fallRight; }

}

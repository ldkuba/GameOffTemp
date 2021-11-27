using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestCollisionText : MonoBehaviour
{
    TextMeshPro tutorialText;
    BoxCollider2D collider2D;
    // Start is called before the first frame update
    void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision with text: " + collision.ToString());
    }
}

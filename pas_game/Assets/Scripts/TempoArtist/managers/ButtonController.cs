using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress)) spriteRenderer.sprite = pressedImage;

        if (Input.GetKeyUp(keyToPress)) spriteRenderer.sprite = defaultImage;
    }
}

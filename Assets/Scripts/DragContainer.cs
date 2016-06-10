using UnityEngine;
using System;

public class DragContainer : MonoBehaviour
{
    static private DragContainer singleton;

    public delegate bool callbackMethod(Vector2 position);
    private callbackMethod callback;

    private bool following;
    private SpriteRenderer view;

    void Awake()
    {
        singleton = this;
        view = GetComponent<SpriteRenderer>();
    }

    //FixedUpdate tem uma variação menor, nao deixa o elemento ficar "fricando" quando esta se movendo.
    void FixedUpdate()
    {
        if (following)
        {
            Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    //Update possui uma taxa de atualização maior, por isso é melhor para pegar inputs
    void Update()
    {
        if (following)
        {
            if (Input.GetMouseButton(00))
            {
                Apply();
            }
            else if (Input.GetMouseButton(01))
            {
                Disable();
            }
        }
    }

    static public void Enable(Sprite icon, callbackMethod callback)
    {
        //Movendo ele antes pra nao aparecer ele piscando no primeiro frame
        singleton.Move(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        singleton.callback = callback;

        Cursor.visible = false;
        singleton.following = true;
        singleton.view.enabled = true;
        singleton.view.sprite = icon;
    }

    void Move(Vector2 newPosition)
    {
        transform.position = newPosition;
    }

    void Disable()
    {
        singleton.following = false;
        singleton.view.enabled = false;
        Cursor.visible = true;
    }

    void Apply()
    {
        if (callback(transform.position))
            Disable();
    }
}
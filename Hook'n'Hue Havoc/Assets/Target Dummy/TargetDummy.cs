using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDummy : MonoBehaviour
{
    public int health = 5;
    Color color5 = new Color(0.3176f, 1f, 0f); // #51FF00
    Color color4 = new Color(0.2745f, 0.8039f, 0.1333f); // #46CD22
    Color color3 = new Color(0.2314f, 0.6039f, 0.2667f); // #3B9A44
    Color color2 = new Color(0.1882f, 0.4078f, 0.4f); // #306866
    Color color1 = new Color(0.1451f, 0.2078f, 0.5333f); // #253588


    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            health -= 1;

            switch (health)
            {
                case 5:
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", color5);
                    break;
                case 4:
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", color4);
                    break;
                case 3:
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", color3);
                    break;
                case 2:
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", color2);
                    break;
                case 1:
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", color1);
                    break;
            }
        }
    }
}
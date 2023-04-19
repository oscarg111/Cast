using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public float spread;
    public float decelRate;
    private SpriteRenderer sprender;
    private bool fadeIn = true;
    // Start is called before the first frame update
    void Start()
    {
        sprender = GetComponent<SpriteRenderer>();
        sprender.color = new Color(1, 1, 1, 0);
        StartCoroutine(FadeIn());
        StartCoroutine(absoluteDeath());
        float Rand = Random.Range(.75f, 2f);
        transform.localScale = new Vector3(transform.localScale.x * Rand, transform.localScale.y * Rand, transform.localScale.z);
        transform.Rotate(new Vector3(0, 0, Random.Range(-spread, spread)));
        rb.velocity = (Vector2)transform.right * speed + (Vector2)GameObject.Find("WaterMage").GetComponent<Rigidbody2D>().velocity * .5f;
    }

    IEnumerator FadeIn()
    {
        for(int i = 1; i <= 25; i++)
        {
            sprender.color = new Color(1, 1, 1, 4*i*.01f);
            yield return new WaitForSeconds(.01f);
        }
        fadeIn = false;
    }

    /*IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(.6f);
        for(int i = 1; i <= 15; i++)
        {
            sprender.color = new Color(1, 1, 1, 1 - 6.7f * i * .01f);
            yield return new WaitForSeconds(.01f);
        }
        Destroy(gameObject);
    }*/

    IEnumerator absoluteDeath()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private void Update()
    {
        
        if (!fadeIn)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, decelRate * Time.deltaTime);
            sprender.color = new Color(1, 1, 1, 2 * (rb.velocity.magnitude / speed));
            if (rb.velocity.magnitude < .1f)
            {
                Destroy(gameObject);
            }
        }
    }

}

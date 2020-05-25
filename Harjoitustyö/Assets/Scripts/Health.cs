using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public bool takeDamage;

    private void Start()
    {
        health = 3;
        numOfHearts = 3;

        takeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        HealthSize();

        if (health == 0)
        {
            health = numOfHearts - 1;
            Destroy(gameObject);
        }
    }

    public void HealthSize()
    {
        if (health > numOfHearts)
        {
            health = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = fullHeart;
            }

            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }

            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy(gameObject);
        if (other.CompareTag("Trap"))
        {
            DamageTaken();
        }
        
    }

    IEnumerator DontTakeDamage()
    {
        yield return new WaitForSeconds(2);
        takeDamage = true;
    }

    public void DamageTaken()
    {
        if (takeDamage == true)
        {
            health = health - 1;
            takeDamage = false;
            StartCoroutine(DontTakeDamage());         
        }
    }
}

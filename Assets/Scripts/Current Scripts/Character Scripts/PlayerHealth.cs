using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public int flashDelay;
    public int damagedTimer;
    public Slider healthSlider;
    public SpriteRenderer mySpriteRenderer;
    public bool damaged;

    private int flashCounter;
    private bool flashToggle = false;

    void Start()
    {
        healthSlider = GameObject.Find("PlayerHealthSlider").GetComponent<Slider>();
        currentHealth = startingHealth;
    }

    private void FixedUpdate()
    {
        if (damagedTimer > 0)
        {
            DamagedFlash(mySpriteRenderer);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        healthSlider.value = currentHealth;

        damaged = true;
        damagedTimer = (flashDelay * 7);

        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    void DamagedFlash(SpriteRenderer spriteRenderer)
    {
        if (flashCounter > flashDelay)      // flashCounter starts at 0, flashDelay starts at 100
        {
            flashCounter = 0;
            flashToggle = !flashToggle;
            if (flashToggle)
            {
                spriteRenderer.enabled = true;
            }
            else
            {
                spriteRenderer.enabled = false;
            }
        }
        else
        {
            flashCounter++;
        }

        damagedTimer--;

        if (damagedTimer == 0)
        {
            spriteRenderer.enabled = true;
            damaged = false;
        }
    }

    void PlayerDeath()
    {
        // fade to black?
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

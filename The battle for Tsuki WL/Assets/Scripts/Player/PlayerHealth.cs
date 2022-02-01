using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public Slider Slider;

    private int currentHealth;
    [HideInInspector]
    public bool isDead = false;

    private Animator animator;
    private Rigidbody2D rigidbody;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = MaxHealth;
    }

    private void Update()
    {
        Slider.value = Mathf.Lerp(Slider.value, currentHealth, 5 * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        GameManager.Instance.HurtEffect();
        currentHealth -= damage;

        animator.SetTrigger("Hit");

        if (currentHealth <= 0)
            Die();
    }

    private void Die() 
    {
        GameManager.Instance.AudioSource.PlayOneShot(PlayerMovement.Instance.deathSound);
        currentHealth = 0;
        Slider.value = 0;
        isDead = true;
        animator.SetBool("IsDead", true);
        this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePosition;
        this.gameObject.GetComponent<CharacterController2D>().enabled = false;
        this.gameObject.GetComponent<PlayerCombat>().enabled = false;
        this.gameObject.GetComponent<PlayerMovement>().enabled = false;
        this.gameObject.GetComponent<PlayerHealth>().enabled = false;
    }

    public void SetDeadPosition()
    {
        float x = transform.position.x;
        transform.position = new Vector2(x, -1.77f);
    }
}

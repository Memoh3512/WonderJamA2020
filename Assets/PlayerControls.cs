﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerControls : MonoBehaviour
{

    private float currHp;
    public Healthbar Healthbar;
    public SpriteRenderer SpriteRenderer;
    public float rateOfLoss;
    public float maxHp;
    private Manette manette;
    public float startTimeBtwAttack;
    private double timeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public float moveSpeed;

    public bool lockMovement = false;
    public float interactRadius = 0.75f;

    private bool justGotDamaged;
    private float dmgToDeal;
    private float z;
    
    private Animator animator;

    public Manette Manette { get => manette; set => manette = value; }

    private void Start()
    {
        z = 0;
        animator = transform.Find("Sprite").GetComponent<Animator>();
        currHp = maxHp;
        Healthbar.SetMaxHealth(maxHp);
    }

    public void GetPlayerGamepad(int index)
    {

        Manette = PlayerInputs.GetPlayerController(index);

    }

    private void Update()
    {
        //Attacks
        if (timeBtwAttack <= 0)
        {
            if (Manette.bButton.wasPressedThisFrame)
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().takeDamage(damage);
                    Debug.Log("Touche un enemie");
                }
                timeBtwAttack = startTimeBtwAttack;
            }
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }

        if (Manette.aButton.wasPressedThisFrame && !lockMovement) CheckInteraction();

        //animations
        AnimationControl();
        //Take Damage
        float tempDmg;
        tempDmg= rateOfLoss * Time.deltaTime * dmgToDeal;
        currHp -= tempDmg;
        dmgToDeal -= tempDmg;
        //Color
        if (justGotDamaged)
        {
            z = 0.1f;
            justGotDamaged = false;
        }

        if (z > 0)
        {
            SpriteRenderer.color=Color.red;
            z -= Time.deltaTime;
        }else
        {
            SpriteRenderer.color=Color.white;
        }
    }

    void AnimationControl()
    {
        
        animator.SetFloat("speed",Manette.leftStick.magnitude);
        if (Manette.leftStick.magnitude > 0.2) animator.SetBool("moving", true);
        else animator.SetBool("moving", false);
        
        //flip sprite
        if (Manette.leftStick.magnitude > 0.2)
        {

            animator.GetComponent<SpriteRenderer>().flipX = (Manette.leftStick.x < 0);
            if (Manette.leftStick.x < 0) attackPos.transform.localPosition = new Vector3(-0.87f,attackPos.transform.localPosition.y);
                else attackPos.transform.localPosition = new Vector3(0.87f,attackPos.transform.localPosition.y);

        }

        Healthbar.SetCurrentHealth(currHp);
    }

    void CheckInteraction()
    {
        
        Collider2D[] thingsNear = Physics2D.OverlapCircleAll(transform.position, interactRadius);
        foreach (var station in thingsNear)
        {

            if (station.CompareTag("Station"))
            {

                station.GetComponent<Interactable>().Interact(gameObject);
                break;
            }
            
        }

    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position,attackRange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,interactRadius);
    }

    private void FixedUpdate()
    {
        MovePlayer();

    }

    void MovePlayer()
    {
        if (!lockMovement) GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x,transform.position.y)+(Manette.leftStick*Time.deltaTime*moveSpeed));
    }
    public void takeDamage(int dmg)
    {
        justGotDamaged = false;
        dmgToDeal=dmg;
        justGotDamaged = true;
    }
}


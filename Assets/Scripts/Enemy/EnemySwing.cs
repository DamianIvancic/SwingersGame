﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwing : MonoBehaviour {

    public int Damage = 1;

    [HideInInspector]
    public bool _playerInRange = false;

    private Animator _anim;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            _anim.SetTrigger("IsSwinging");           
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _anim.SetTrigger("IsSwinging");
            _playerInRange = false;        
        }
    }

    private void Start()
    {
        _anim = GetComponentInParent<Animator>();
    }

}
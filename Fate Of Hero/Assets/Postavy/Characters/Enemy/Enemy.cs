﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.AI;
using UnityEngine.Events;
using Data;

[CreateAssetMenu(menuName = "CharacterData/Enemy")]
public class Enemy : Entity, IEnemyNPC
{
   // [SerializeField]
 //   private EntityTimePlan timePlan;
 [Header("Enemy")]
    [Tooltip("HearRadius")]
    [SerializeField]
    private float hearRadius;
    [Tooltip("CanSwim")]
    [SerializeField]
    private bool canSwim;
    [Tooltip("CanDive")]
    [SerializeField]
    private bool canDive;
    [SerializeField]
    private bool isOneHitEnemy = false;

    public float HearRadius { get => hearRadius > 0 ? hearRadius : 0; set => hearRadius = value; }
    public bool CanSwim { get => canSwim; }
    public bool CanDive { get => canDive; }
    //  public EntityTimePlan TimePlan { get => timePlan; set => timePlan = value; }
  
}

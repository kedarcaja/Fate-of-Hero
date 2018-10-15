using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void HealthChange(float health);
public delegate void characterRemove();
public class NPCScript : Character {

    [SerializeField]
    private Sprite portrait;

    

    public Sprite MyPortrait
    {
        get
        {
            return portrait;
        }

        set
        {
            portrait = value;
        }
    }

    public event HealthChange Healthchange;

    public void OnHealthChange(float health)
    {
        if (Healthchange != null)
        {
            Healthchange(health);
        }

    }

    
    
}

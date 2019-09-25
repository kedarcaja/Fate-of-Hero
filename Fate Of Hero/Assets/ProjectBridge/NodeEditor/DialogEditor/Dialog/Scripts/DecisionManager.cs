using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DecisionManager : MonoBehaviour
{
    public List<DecisionOptionValue> values = new List<DecisionOptionValue>();
    [SerializeField]
    private List<Sprite> icons = new List<Sprite>();
    public DecisionManager Instance { get; private set; }
    public List<Sprite> Icons { get => icons; }










    public Decision test;
    private void Awake()
    {
        Instance = FindObjectOfType<DecisionManager>();

    }






}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuTextHighliter : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> items = new List<TextMeshProUGUI>();
    private int index = 0, maxIndex, minIndex;

    public List<TextMeshProUGUI> Items { get => items;  }

    void Awake()
    {
        minIndex = 0;
        maxIndex = Items.Count;
        index = 0;
        Highlite(Color.yellow, Items[index]);
    }
    public int  GetSelectedIndex()
    {
        return index;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            if (index < maxIndex)
            {
                index++;
            }
            else if (index == maxIndex)
            {
                index = minIndex;
            }
            Highlite(Color.yellow, Items[index]);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (index > minIndex)
            {
                index--;
            }
            else if (index == minIndex)
            {
                index = maxIndex;
            }
            Highlite(Color.yellow, Items[index]);

        }

    }
    private void Highlite(Color col, TextMeshProUGUI item)
    {
        foreach (TextMeshProUGUI t in Items)
        {
            t.color = Color.white;
        }
        item.color = col;
        index = Items.IndexOf(item);
    }
    public void SelectItem(TextMeshProUGUI t)
    {
        Highlite(Color.yellow, t);
    }
}

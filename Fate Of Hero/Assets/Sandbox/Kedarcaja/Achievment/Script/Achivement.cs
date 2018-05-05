using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achivement
{
    private string name;
    public string Name
    {
        get
        {
            return name;
        }

        set
        {
            name = value;
        }
    }
    private string description;
    public string Description
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }
    private bool unlocked;
    public bool Unlocked
    {
        get
        {
            return unlocked;
        }

        set
        {
            unlocked = value;
        }
    }
    private int points;
    public int Points
    {
        get
        {
            return points;
        }

        set
        {
            points = value;
        }
    }
    private int spriteIndex;
    public int SpriteIndex
    {
        get
        {
            return spriteIndex;
        }

        set
        {
            spriteIndex = value;
        }
    }
    public string Child
    {
        get
        {
            return child;
        }

        set
        {
            child = value;
        }
    }
    private int currentProgression;
    private int maxProgression;

    private GameObject AchivementRef;

    private List<Achivement> dependencies = new List<Achivement>();

    private string child;

    public Achivement()
    {
    }

    public Achivement(string name, string description,int points, int spriteIndex, GameObject achivementRef,int maxProgression)
    {
        this.Name = name;
        this.Description = description;
        this.Unlocked = false;
        this.Points = points;
        this.SpriteIndex = spriteIndex;
        this.AchivementRef = achivementRef;
        this.maxProgression = maxProgression;
        LoadAchievment();
    }

    public void AddDependency(Achivement dependency)
    {
        dependencies.Add(dependency);
    }
    public bool EarnAchivement()
    {
        if (!Unlocked && !dependencies.Exists(x=> x.unlocked == false) && CheckProgress())
        {  
            AchivementRef.GetComponent<Image>().sprite = AchivementManager.Instance.unlockedSprite;
            SaveAchievment(true);
            if (child != null)
            {
                AchivementManager.Instance.EarnAchievment(child);
            }
            return true;
        }
        return false;
    }
    public void SaveAchievment(bool value)
    {
        Unlocked = value;

        int tmpPoints = PlayerPrefs.GetInt("Points");

        PlayerPrefs.SetInt("Points", tmpPoints += points);
        PlayerPrefs.SetInt("Progression" + Name, currentProgression);

        PlayerPrefs.SetInt(name, value ? 1 : 0);

        PlayerPrefs.Save();
        
    }

    public void LoadAchievment()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;
        if (unlocked)
        {
            AchivementManager.Instance.textPoint.text = "Points: " + PlayerPrefs.GetInt("Points");
            currentProgression = PlayerPrefs.GetInt("Progression" + Name);
            AchivementRef.GetComponent<Image>().sprite = AchivementManager.Instance.unlockedSprite;
        }
    }

    public bool CheckProgress()
    {
        currentProgression++;
       AchivementRef.transform.GetChild(0).GetComponent<Text>().text = Name + " " + currentProgression + "/" + maxProgression;
        SaveAchievment(false);
        if (maxProgression ==0)
        {
            return true;
        }
        if (currentProgression >= maxProgression)
        {
            return true;
        }
        return false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementManager : MonoBehaviour {
    #region Variables
    public GameObject achivementPrefab;
    public Sprite[] sprites;
    public Scrollbar scrollbar;
    private AchvementButton ActiveButton;
    public ScrollRect scrollRect;
    public Text textPoint;
    public GameObject visualAchievment;
    public GameObject achivementMenu;
    public Dictionary<string, Achivement> achievment = new Dictionary<string, Achivement>();
    public Sprite unlockedSprite;
    private static AchivementManager instance;
    private int fadeTime = 2;

    public static AchivementManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<AchivementManager>();
            }
            return AchivementManager.instance;
        }

 
    }

    #endregion
    #region Metods
    void Start ()
    {
        PlayerPrefs.DeleteAll();
       

        ActiveButton = GameObject.Find("GeneralBtn").GetComponent<AchvementButton>();
        CreateAchivement("General","Press W","Press W to unlock",5,1,0);
        CreateAchivement("General","Press S","Press S to unlock",5,1,0);
        CreateAchivement("General", "Press A", "Press A to unlock",5, 1,0);
        CreateAchivement("General", "Press D", "Press D to unlock", 5, 1,0);
        CreateAchivement("General", "Press all key", "all key to unlock", 25, 2,0, new string[] { "Press W", "Press S", "Press A", "Press D" });
        CreateAchivement("General", "Press L", "3x Press L to unlock", 5, 1, 3);

        CreateAchivement("Other", "Jablko", "Eat Apple", 2, 3,0);
        CreateAchivement("Other", "Višeň", "Eat Cherry", 2, 3,0);
        CreateAchivement("Other", "Banán", "Eat Banana", 2, 3,0);
        CreateAchivement("Other", "Víno", "Eat Wine", 2, 3,0);
        CreateAchivement("Other", "Hruška", "Eat Pear", 2, 3,0);
        CreateAchivement("Other", "Ovoxný salát", "Eat all fruit", 40, 4,0, new string[] { "Jablko", "Višeň", "Banán", "Víno","Hruška" });


        CreateAchivement("General", "1", "Press W to unlock", 5, 1, 0);
        CreateAchivement("General", "2", "Press W to unlock", 5, 1, 0);
        CreateAchivement("General", "3", "Press W to unlock", 5, 1, 0);
        CreateAchivement("General", "4", "Press W to unlock", 5, 1, 0);
        CreateAchivement("General", "5", "Press W to unlock", 5, 1, 0);

        foreach (GameObject achivementList in GameObject.FindGameObjectsWithTag("AchivementList"))
        {
            achivementList.SetActive(false);
        }
        ActiveButton.Click();
        achivementMenu.SetActive(false);
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            achivementMenu.SetActive(!achivementMenu.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievment("Press W");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EarnAchievment("Press S");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EarnAchievment("Press A");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            EarnAchievment("Press D");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            EarnAchievment("Press L");
        }

        if (Input.GetAxis("Mouse ScrollWheel")>0f)
        {
            scrollbar.value += 0.2f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            scrollbar.value -= 0.2f;
        }
    }
    public void EarnAchievment(string title)
    {
        if (achievment[title].EarnAchivement())
        {
            GameObject achievment = (GameObject)Instantiate(visualAchievment);
            SetAchivementInfo("EarnCanvas", achievment, title);
            textPoint.text = "Points: " + PlayerPrefs.GetInt("Points");
            StartCoroutine(FadeAchievment(achievment));
        }
    }
    public IEnumerator HideAchievment(GameObject achievment)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievment);
    }
    public void CreateAchivement(string parent, string title,string description, int poit, int spriteIndex, int progress, string[] dependencies = null)
    {

        GameObject achivment = Instantiate(achivementPrefab);
        Achivement newAchivement = new Achivement(title, description, poit, spriteIndex, achivment,progress);

        achievment.Add(title, newAchivement);
        SetAchivementInfo(parent, achivment,title,progress);
        if (dependencies !=null)
        {
            foreach (string achievmentTile in dependencies)
            {
                Achivement dependency = achievment[achievmentTile];
                dependency.Child = title;
                newAchivement.AddDependency(dependency);
            }
        }
    }

    public void SetAchivementInfo(string parent,GameObject achivment, string title, int progression = 0)
    {
        achivment.transform.SetParent(GameObject.Find(parent).transform);
        achivment.transform.localScale = new Vector3(1, 1, 1);

        string progress = progression > 0 ? " " + PlayerPrefs.GetInt("Progression" + title) + "/" + progression.ToString(): string.Empty;

        achivment.transform.GetChild(0).GetComponent<Text>().text = title + progress;
        achivment.transform.GetChild(1).GetComponent<Text>().text = achievment[title].Description;
        achivment.transform.GetChild(2).GetComponent<Text>().text = achievment[title].Points.ToString();
        achivment.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievment[title].SpriteIndex];
    }

    public void ChangeCategory(GameObject button)
    {
        AchvementButton achvementButton = button.GetComponent<AchvementButton>();

        scrollRect.content = achvementButton.achvementList.GetComponent<RectTransform>();
        achvementButton.Click();
        ActiveButton.Click();
        ActiveButton = achvementButton;
    }

    private IEnumerator FadeAchievment(GameObject achievment)
    {
        CanvasGroup canvasGroup = achievment.GetComponent<CanvasGroup>();
        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;

        
        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;

            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }
        Destroy(achievment);

    }
    #endregion
}

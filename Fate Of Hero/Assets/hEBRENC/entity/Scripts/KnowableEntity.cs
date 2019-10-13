using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class KnowableEntity : MonoBehaviour
{
	[SerializeField]
	private KnowlableEntityMainData mainData;
	private Image mainImage;
	[SerializeField]
	private TextMeshProUGUI nameText;



	[SerializeField]
	private List<OtherImageData> otherImages = new List<OtherImageData>();
	[SerializeField]
	private List<OtherTextData> otherTexts = new List<OtherTextData>();

	void Start()
	{
		mainImage = GetComponent<Image>();
		foreach (OtherImageData d in otherImages) // odebrat $
		{
			d.Data.IsKnown = false;
		}
		foreach (OtherTextData d in otherTexts)
		{
			d.Data.IsKnown = false;
		}
		mainData.IsKnown = false; // $
		UpdateMainData();
		GetComponent<Button>().onClick.AddListener(() => ShowData());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			SetAsKnown(mainData);

		}
	}
	public void SetAsKnown(KnowlableEntityData data)
	{
		data.IsKnown = true;
		UpdateMainData();
	}
	public void UpdateMainData()
	{
		mainImage.sprite = mainData.Sprite;
		nameText.text = mainData._Name;
	}
	public void ShowData()
	{
		if (this == KnowladngeManager.Instance.CurrentEntity) return;
		if(KnowladngeManager.Instance.CurrentEntity!=null)
		KnowladngeManager.Instance.CurrentEntity.HideData();
		KnowladngeManager.Instance.CurrentEntity = this;

		
		UpdateMainData();
		foreach (OtherImageData img in otherImages)
		{
			if (img.Data.IsKnown)
			{
				img.CreateInstance(transform.Find("Childs"));
			}
		}
		foreach (OtherTextData tx in otherTexts)
		{
			if (tx.Data.IsKnown)
			{
				tx.CreateInstance(transform.Find("Childs"));
			}
		}
	}
	public void HideData()
	{
		for (int i = 0; i < transform.Find("Childs").childCount; i++)
		{

			Destroy(transform.Find("Childs").GetChild(i).gameObject);
		}
	}

}
[Serializable]
public struct OtherImageData : IInstanceable
{

	public Image Image { get; private set; }
    [SerializeField]
    private Vector2 place;
	[SerializeField]
	private KnowlableOtherImageData data;

	public KnowlableOtherImageData Data
	{
		get
		{
			return data;
		}
	}

	public void CreateInstance(Transform parent)
	{
		Image = UnityEngine.Object.Instantiate(KnowladngeManager.Instance.ImagePrefab).GetComponent<Image>();
		Image.sprite = data.Sprite;
		Image.transform.parent = parent;
        Image.SetNativeSize();
		Image.transform.localPosition = place;

	}
}
[Serializable]
public struct OtherTextData : IInstanceable
{

	public TextMeshProUGUI Text { get; private set; }
	[SerializeField]
	private Vector2 place, size;
	[SerializeField]
	private KnowlableOtherTextData data;

	public KnowlableOtherTextData Data
	{
		get
		{
			return data;
		}
	}

	public void CreateInstance(Transform parent)
	{
		Text = UnityEngine.Object.Instantiate(KnowladngeManager.Instance.TextPrefab).GetComponent<TextMeshProUGUI>();
		Text.text = data.Text;
		Text.transform.parent = parent;
		Text.transform.localPosition = place;
		Text.GetComponent<RectTransform>().sizeDelta = size;

	}

}
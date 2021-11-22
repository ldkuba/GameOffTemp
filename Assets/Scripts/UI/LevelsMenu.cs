using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    [SerializeField]
    private float m_listSpacing;

    [SerializeField]
    private GameObject m_levelItemPrefab;

    [SerializeField]
    private GameObject m_listRoot;

    void OnEnable()
    {
        m_listRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(0, m_listSpacing * GameManager.Instance.Levels.Count);

        int i = 0;
        // Make list element for each ability and add to the window
        foreach(LevelDescriptor level in GameManager.Instance.Levels)
        {
            GameObject levelListItem = Instantiate(m_levelItemPrefab, m_listRoot.transform);
            levelListItem.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -i * m_listSpacing, 0);

            levelListItem.transform.Find("Thumbnail").GetComponent<Image>().sprite = level.thumbnail;
            levelListItem.transform.Find("LevelName").GetComponentInChildren<Text>().text = level.levelName;

            levelListItem.transform.Find("LevelName").GetComponent<Button>().onClick.AddListener(() => {
                OnLevelSelect(level);
            });

            i++;
        }
    }

    void OnLevelSelect(LevelDescriptor level)
    {
        GameManager.Instance.Play(level);
    }

    void OnDisable()
    {
        foreach(Transform child in m_listRoot.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

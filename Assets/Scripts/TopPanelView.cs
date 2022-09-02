using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TopPanelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _skillPointsLable;
    [SerializeField] private Button _earnSkillPoint;
    [SerializeField] private Button _learnSkill;
    [SerializeField] private Button _unlearnSkill;
    [SerializeField] private Button _unlearnAllSkills;

    public event Action EarnButtonClickEvent;
    public event Action LearnButtonClickEvent;
    public event Action UnlearnButtonClickEvent;
    public event Action UnlearnAllButtonClickEvent;

    private void Awake()
    {
        _earnSkillPoint.onClick.AddListener(OnEarnButtonClick);
        _learnSkill.onClick.AddListener(OnLearnButtonClick);
        _unlearnSkill.onClick.AddListener(OnUnlearnButtonClick);
        _unlearnAllSkills.onClick.AddListener(OnUnlearnAllButtonClick);
    }

    private void OnEarnButtonClick()
    {
        EarnButtonClickEvent?.Invoke();
    }

    private void OnLearnButtonClick()
    {
        LearnButtonClickEvent?.Invoke();
    }

    private void OnUnlearnButtonClick()
    {
        UnlearnButtonClickEvent?.Invoke();
    }

    private void OnUnlearnAllButtonClick()
    {
        EarnButtonClickEvent?.Invoke();
    }

    // Update is called once per frame
    public void UpdateSkillPoints(int skillPointsCount)
    {
        _skillPointsLable.text = skillPointsCount.ToString();
    }

    private void OnDestroy()
    {
        _earnSkillPoint.onClick.RemoveListener(OnEarnButtonClick);
        _learnSkill.onClick.RemoveListener(OnLearnButtonClick);
        _unlearnSkill.onClick.RemoveListener(OnUnlearnButtonClick);
        _unlearnAllSkills.onClick.RemoveListener(OnUnlearnAllButtonClick);
    }
}

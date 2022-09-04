using UnityEngine;
using UnityEngine.UI;
using System;

public class TopPanelView : MonoBehaviour
{
    [SerializeField] private string _skillPointsLabelMask = "Skill points: {0}";
    [SerializeField] private Text _skillPointsLabel;
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

    public void UpdateButtonsState(bool isLearnButtonInteractable, bool isUnlearnButtonInteractable)
    {
        _learnSkill.interactable = isLearnButtonInteractable;
        _unlearnSkill.interactable = isUnlearnButtonInteractable;
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
        UnlearnAllButtonClickEvent?.Invoke();
    }

    public void UpdateSkillPoints(int skillPointsCount)
    {
        _skillPointsLabel.text = string.Format(_skillPointsLabelMask, skillPointsCount);
    }

    private void OnDestroy()
    {
        _earnSkillPoint.onClick.RemoveListener(OnEarnButtonClick);
        _learnSkill.onClick.RemoveListener(OnLearnButtonClick);
        _unlearnSkill.onClick.RemoveListener(OnUnlearnButtonClick);
        _unlearnAllSkills.onClick.RemoveListener(OnUnlearnAllButtonClick);
    }
}

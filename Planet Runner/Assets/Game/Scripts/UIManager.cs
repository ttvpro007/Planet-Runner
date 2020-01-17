using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText = null;
    [SerializeField] Text gameOverScoreText = null;
    [SerializeField] Animator textAnimator = null;
    [SerializeField] RectTransform healthForeGround = null;
    [SerializeField] Animator healthBarAnimator = null;

    public void ChangeScoreText(float score)
    {
        scoreText.text = string.Format("{0}", score);
        gameOverScoreText.text = string.Format("YOUR SCORE: {0}", score); ;
    }

    public void ChangeHealthForeGround(float currentHealthFraction)
    {
        Vector3 scaleValue = healthForeGround.localScale;

        scaleValue.x = currentHealthFraction;

        healthForeGround.localScale = scaleValue;
    }

    public void TriggerTextAnimation()
    {
        textAnimator.SetTrigger("update");
    }

    public void TriggerHealthBarAnimation()
    {
        healthBarAnimator.SetTrigger("damage");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImage;
    public Image energyImage;

    private void Update()
    {
        //如果Delay的FillAmount大于正常血量的话，就一点一点减少
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
    }

    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
}

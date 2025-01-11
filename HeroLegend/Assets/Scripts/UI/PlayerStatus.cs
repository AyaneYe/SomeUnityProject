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
        //���Delay��FillAmount��������Ѫ���Ļ�����һ��һ�����
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

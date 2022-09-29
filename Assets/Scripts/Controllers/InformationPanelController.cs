using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationPanelController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;



    public void ShowItemInformations(Item item)
    {
        image.sprite = item.Sprite;
        text.text = item.Name;
    }


    private void OnEnable()
    {
        EventsManager.instance.clickedUI += ShowItemInformations;
    }


    private void OnDisable()
    {
        EventsManager.instance.clickedUI -= ShowItemInformations;
    }

}

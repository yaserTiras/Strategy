using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{





    public Button startButton;
    public Button continueButton;
    public Button retrytButton;
    public Image diamondImage;
    public TMP_Text diamondText;



    public GameObject inGamePanel;
    public GameObject winPanel;
    public GameObject loosePanel;
    public GameObject diamondPrefab;

    private List<Transform> diamondPool;
    private int currentDiamond = 0;
    private int diamondCount = 0;

    public bool IsOnUI;
    public bool IsOnUI2;

    public override void Initialize()
    {
        //diamondPool = new List<Transform>();
        //GameObject obj;

        //for (int i = 0; i < 35; i++)
        //{
        //    obj = Instantiate(diamondPrefab);
        //    obj.transform.SetParent(inGamePanel.transform);
        //    obj.SetActive(false);
        //    diamondPool.Add(obj.transform);
        //}

        //startButton.onClick.AddListener(StartGame);
        //continueButton.onClick.AddListener(Retry);
        //retrytButton.onClick.AddListener(Continue);



    }

    public void OnDrag(Vector2 pos)
    {
        Debug.Log("Pos " + pos);
    }

    private void Update()
    {
        //IsOnUI = eventSystem.IsPointerOverGameObject(1);
        //IsOnUI2 = eventSystem.IsPointerOverGameObject();
    }

    private void StartGame()
    {
        startButton.transform.DOScale(0.7f, 0.2f).OnComplete(() =>
         {
             startButton.transform.DOScale(1f, 0.15f).OnComplete(() =>
             {
                 startButton.gameObject.SetActive(false);
                 GameManager.instance.StartGame();
             });
         });

    }


    private void Retry()
    {
        retrytButton.transform.DOScale(0.7f, 0.2f).OnComplete(() =>
        {
            retrytButton.transform.DOScale(1f, 0.15f).OnComplete(() =>
            {
                retrytButton.gameObject.SetActive(false);
                GameManager.instance.Retry();
            });
        });
    }

    private void Continue()
    {
        continueButton.transform.DOScale(0.7f, 0.2f).OnComplete(() =>
        {
            continueButton.transform.DOScale(1f, 0.15f).OnComplete(() =>
            {
                continueButton.gameObject.SetActive(false);
                GameManager.instance.Retry();
            });
        });
    }

    private void GameEnded(bool? status)
    {
        if (status != null)
        {
            if (status.Value)
            {
                continueButton.transform.localScale = Vector3.zero;
                winPanel.SetActive(true);
                continueButton.transform.DOScale(1.25f, 0.2f).OnComplete(() =>
                {
                    continueButton.transform.DOScale(1f, 0.15f);
                });
            }
            else
            {
                retrytButton.transform.localScale = Vector3.zero;
                loosePanel.SetActive(true);
                retrytButton.transform.DOScale(1.25f, 0.2f).OnComplete(() =>
                {
                    retrytButton.transform.DOScale(1f, 0.15f);
                });
            }
        }
    }

    private void OnEnable()
    {
       // GameManager.instance.onGameEnded += GameEnded;
    }

    private void OnDisable()
    {
       // GameManager.instance.onGameEnded -= GameEnded;
    }

    private void AnimateDiamond(int Id, int value, Vector3 screen)
    {
        diamondPool[Id].localScale = Vector3.zero;
        diamondPool[Id].gameObject.SetActive(true);
        diamondPool[Id].position = (screen);

        diamondPool[Id].DOScale(1f, 0.1f).OnComplete(() =>
        {
            diamondPool[Id].DOMove(diamondImage.transform.position, 0.25f).OnComplete(() =>
            {
                diamondPool[Id].DOScale(0f, 0.1f);
                diamondImage.transform.DOScale(1.2f, 0.1f).OnComplete(() => diamondImage.transform.DOScale(1f, 0.15f));
                diamondCount += value;
                diamondText.text = diamondCount.ToString();
            });
        }
         );

    }

    public void AddDiamond(int value, Vector3 screenPosition)
    {
        AnimateDiamond(currentDiamond, value, screenPosition);
        if (currentDiamond == diamondPool.Count - 1)
        {
            currentDiamond = 0;
        }
        else
        {
            currentDiamond++;
        }

    }


    private void OnValidate()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject[] circlePrefabs;//�������
    public static Dictionary<string, GameObject> dictCircles = new();//������� �������� ��� ����� �������� ������
    private static List<GameObject> circlesDeck = new();//circles � ������
    private static int countSpawnGold = 10;
    private static int countMyGold = 100;
    public TextMeshProUGUI textMyGold;
    public TextMeshProUGUI textNumberWave;
    public Text textSpawnGold;
    private static int goldForKillEnemy = 10;
    public GameObject windowLoseGame;
    public GameObject gameWindow;
    public GameObject countDown;
    public static int numberWave = 1;
    [SerializeField] private GameObject[] deck;
    public static float goldForGame;
    public static float expForGame;
    [SerializeField] private Text textGoldForGame;
    [SerializeField] private Text textExpForGame;


    void Start()
    {
        FillDict();
        SetCirlesDeck();
        GlobalEventManager.Gold.AddListener(BuySpawn);
        GlobalEventManager.Gold.AddListener(IncreaseCostSpawn);
        GlobalEventManager.KillEnemy.AddListener(KillEnemyGold);
        GlobalEventManager.LoseGame.AddListener(AppearanceLoseGame);
        GlobalEventManager.CountDown.AddListener(StartCorutineDoCheck);
        GlobalEventManager.NextWave.AddListener(NextWave);
        UpdateMyGold();
        UpdateSpawnGold();
        GlobalEventManager.StartCountDown();
    }

    private void UpdateMyGold()
    {
        textMyGold.text = countMyGold.ToString();
    }

    private void UpdateSpawnGold()
    {
        textSpawnGold.text = countSpawnGold.ToString();
    }

    private void IncreaseCostSpawn()//��������� ��������� ������ circle
    {
        countSpawnGold += 10;
        UpdateSpawnGold();
    }

    private void KillEnemyGold()
    {
        IncreaseCountMyGold(goldForKillEnemy);

    }

    private void IncreaseCountMyGold(int gold)//���������� ������
    {
        countMyGold += gold;
        UpdateMyGold();
    }

    private void BuySpawn()//������� ������ circle
    {  
        countMyGold -= countSpawnGold;
        UpdateMyGold();
    }

    public static bool CheckGold()
    {
        if (countMyGold >= countSpawnGold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void AppearanceLoseGame()//��������� ���������
    {
        windowLoseGame.SetActive(true);
        var gameWindowCanvasGroup = gameWindow.GetComponent<CanvasGroup>();
        var UICanvasGroup = gameObject.GetComponent<CanvasGroup>();
        gameWindowCanvasGroup.alpha = 0.5f;
        gameWindowCanvasGroup.interactable = false;
        UICanvasGroup.alpha = 0.5f;
        UICanvasGroup.interactable = false;
        textGoldForGame.text = Convert.ToInt32(goldForGame).ToString() + " gold";
        textExpForGame.text = Convert.ToInt32(expForGame).ToString() + " exp";

        DataBase.ExecuteQueryWithAnswer($"UPDATE player SET Gold = Gold + '{(int)goldForGame}'"); // ���������� ����� � �������
        DataBase.ExecuteQueryWithAnswer($"UPDATE player SET Experience = Experience + '{(int)expForGame}'"); // ���������� ����� � �������
        DataBase.ExecuteQueryWithAnswer($"UPDATE player SET Record = '{(int)numberWave}'"); // ���������� ������� ����� � �������
    }

    private IEnumerator DoCheck()//������
    {
        Spawner.checkWave = true;
        countDown.SetActive(true);
        var textCountDown = countDown.GetComponent<Text>();
        var i = 5;
        while (i > 0)
        {
            textCountDown.text = i.ToString();
            i--;
            yield return new WaitForSeconds(1);
        }
        countDown.SetActive(false);
        GlobalEventManager.StartSpawn();
    }

    void StartCorutineDoCheck()
    {
        StartCoroutine(DoCheck());
    }

    private void NextWave()//����� ��������� �����
    {
        Enemy.UpHpNextWave();
        numberWave++;
        textNumberWave.text = numberWave + " wave";
    }

    private void SetCirlesDeck()//��������� circles � ������ 
    {
        var query = "SELECT name_cell, name_circle FROM main_deck JOIN circles ON circle = id_circle";
        var mainDeck = DataBase.GetTable(query);
        for (int i = 0; i < mainDeck.Rows.Count; i++)
        {
            var nameCircle = mainDeck.Rows[i]["name_circle"].ToString();
            var createdCircle = Instantiate(dictCircles[$"{nameCircle.ToLower()}"], parent: deck[i].transform);
            circlesDeck.Add(createdCircle);
            createdCircle.transform.position = deck[i].transform.position;
            createdCircle.GetComponent<Circle>().IsShoot = false;
            createdCircle.GetComponent<CanvasGroup>().blocksRaycasts = false;
            createdCircle.GetComponent<CanvasGroup>().interactable = false;
            createdCircle.GetComponent<BoxCollider2D>().enabled = false;
        }   
    }

    private void FillDict()//���������� ������� circles
    {
        foreach (var circle in circlePrefabs)
        {
            dictCircles.Add($"{circle.name}", circle);
        }
    }

    public static GameObject GetCircleForSpawn()//��������� circle ��� ������
    {
        var i = UnityEngine.Random.Range(0, circlesDeck.Count);
        return circlesDeck[i];
    }
}

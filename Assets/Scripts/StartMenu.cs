using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.Scripts;
using TMPro;
using UnityEngine.UI;
using System.Data;

public class StartMenu : MonoBehaviour
{
    public static GameObject[] buttonsNavigation;
    public static GameObject currentMenu;
    [SerializeField] private TextMeshProUGUI nameUser;
    [SerializeField] private TextMeshProUGUI levelUser;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private TextMeshProUGUI record;
    [SerializeField] private CanvasGroup menu;
    [SerializeField] private GameObject windowEnterName;
    [SerializeField] private GameObject enteredName;
    [SerializeField] private GameObject[] mainDeckGame;
    [SerializeField] private GameObject[] editDeckGame;
    [SerializeField] private GameObject addCircle;
    [SerializeField] private GameObject[] circlesInList;
    public static List<CircleInfo> circles = new();
    public static Player player = new();

    void Start()
    {
        GlobalEventManager.PressPlay.AddListener(SetMainDeckPlay);
        GlobalEventManager.PressCircles.AddListener(SetMainDeckCircles);
        GlobalEventManager.PressCircles.AddListener(SetOrderCirclesInList);
        currentMenu = GameObject.Find("PlayMenu");
        buttonsNavigation = GameObject.FindGameObjectsWithTag("button_navigation");
        GetInfoPlayer();
        GetCircles();
        SetMainDeckPlay();
    }

    public static void SetMenu(GameObject menu)
    {
        currentMenu.SetActive(false);
        menu.SetActive(true);
        currentMenu = menu;
    }

    private void GetInfoPlayer()// получение информации о игроке
    {
        var infoPlayer = DataBase.GetTable("select * from player");
        if (infoPlayer.Rows[0]["Name"].ToString() == "")
        {
            PopupEnterName(true);
            return;
        }
        player.Name = infoPlayer.Rows[0]["Name"].ToString();
        player.Gold = int.Parse(infoPlayer.Rows[0]["Gold"].ToString());
        //player.Lvl = int.Parse(infoPlayer.Rows[0]["Level"].ToString());
        player.Experience = int.Parse(infoPlayer.Rows[0]["Experience"].ToString());
        player.Record = int.Parse(infoPlayer.Rows[0]["Record"].ToString());
        UpdateCharacteristics();
    }

    private void PopupEnterName(bool IsPopup)// появление окна ввода имени
    {
        if (IsPopup)
        {
            menu.interactable = false;
            menu.alpha = 0.5f;
            windowEnterName.SetActive(true);
        }
        else
        {
            menu.interactable = true;
            menu.alpha = 1;
            windowEnterName.SetActive(false);
        }

    }

    private void UpdateCharacteristics()// обновление характеристик
    {
        gold.text = player.Gold.ToString();
        nameUser.text = player.Name.ToString();
        levelUser.text = player.Lvl.ToString();
        record.text = player.Record.ToString();
    }

    public void SetNameUser()// установка имени игрока
    {
        var text = enteredName.GetComponent<TMP_InputField>().text;
        if (text == "")
        {
            return;
        }
        else
        {
            var query = $"UPDATE player SET Name='{text}'";
            DataBase.ExecuteQueryWithAnswer(query);
            GetInfoPlayer();
            PopupEnterName(false);
        }
    }

    private void SetMainDeckPlay()// установка колоды в меню "Play"
    {
        foreach (GameObject circle in mainDeckGame)
        {
            if (circle.transform.childCount > 0)
            {
                for (int i = 0; i < circle.transform.childCount; i++)
                {
                    var chldren = circle.transform.GetChild(i).gameObject;
                    Destroy(chldren);
                }
            }
        }
        var query = "SELECT name_cell, name_circle FROM main_deck JOIN circles ON circle = id_circle";
        var mainDeck = DataBase.GetTable(query);
        for (int i = 0; i < mainDeck.Rows.Count; i++)
        {
            var nameCircle = mainDeck.Rows[i]["name_circle"].ToString();
            var createdCircle = Instantiate(addCircle, parent: mainDeckGame[i].transform);
            createdCircle.GetComponent<Image>().sprite = Resources.Load<Sprite>($"CirclesIcon/{nameCircle.ToLower()}");
            createdCircle.transform.position = mainDeckGame[i].transform.position;
        }
        
    }

    private void SetMainDeckCircles()// установка колоды в меню "Circles"
    {
        var query = "SELECT name_cell, id_circle FROM main_deck JOIN circles ON circle = id_circle";
        var mainDeck = DataBase.GetTable(query);
        for (int i = 0; i < mainDeck.Rows.Count; i++)
        {
            var idCircle = int.Parse(mainDeck.Rows[i]["id_circle"].ToString());
            foreach (var circle in circlesInList)
            {
                if (circle.GetComponent<CircleIcon>().id == idCircle)
                {

                    circle.transform.position = editDeckGame[i].transform.position;
                    circle.transform.SetParent(editDeckGame[i].transform);
                    editDeckGame[i].GetComponent<CellDeck>().fixedCircle = circle;
                    circle.GetComponent<CanvasGroup>().blocksRaycasts = false;
                    break;
                }
            }
        }
    }

    private void GetCircles()// получение всех circles 
    {
        var tableCircle = DataBase.GetTable("SELECT * FROM circles");
        for (int i = 0; i < tableCircle.Rows.Count; i++)
        {
            int ID =  int.Parse(tableCircle.Rows[i]["id_circle"].ToString());
            bool IS_HAVE = true;// = bool.Parse(tableCircle.Rows[i]["is_have"].ToString());
            bool IS_TAKE = false;// = bool.Parse(tableCircle.Rows[i]["is_take"].ToString());
            if (player.Lvl < int.Parse(tableCircle.Rows[i]["level_using"].ToString()))
            {
                IS_HAVE = false;
                Debug.Log(player.Lvl);
            }
            string NAME = tableCircle.Rows[i]["name_circle"].ToString();
            circles.Add(new CircleInfo(ID, IS_HAVE, IS_TAKE, NAME));    
        }
        
    }

    private void SetOrderCirclesInList()// установка порядка circles в меню "Circles"
    {
        //circlesInList = GameObject.FindGameObjectsWithTag("circle_icon");
        circles.OrderBy(x => x.Id);
        if (circlesInList.Length > 0)
        {
            foreach (var circle in circlesInList)
            {
                var index = circle.GetComponent<CircleIcon>().id - 1;
                circle.transform.SetSiblingIndex(index);
            }
        }
    }
}
public class CircleInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsHave { get; set; }
    public bool IsTake { get; set; }

    public CircleInfo(int id, bool isHave, bool isTake, string name)
    {
        Id = id;
        Name = name;
        IsHave = isHave;
        IsTake = isTake;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    [SerializeField]
    private Cell CellPrefab;

    [SerializeField]
    private Cell cellPrefabfinish;

    [SerializeField]
    private Cell cellWallPrefab;

    [SerializeField]
    private Player PlayerPrefab;

    [SerializeField]
    private Enemy EnemyPrefab;

    private Grid grid;

    private Player player;

    private List<Enemy> enemies = new List<Enemy>();
    
    public int Mcells;

    public int Ncells;

    private int x;

    private int y;

    private Cell Point;

    private Vector2 startPoint;

    private Vector2 finishPoint;

    private Vector2[] enemiesPoint;

    private bool i;
    private bool ready = true;

    public static int level = 1;

    public Button buttonlose;

    public Text Textfield;
    public Text TextLevel;
    public Text Timertext;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;
    private string timePlayingStr;
 

    public int GetLevel()
    {
        return level;
    }
    public string GetTime()
    {
        return timePlayingStr;
    }


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(ready == true)
        {
            foreach (Enemy ene in enemies)
            {
                if (player.GetPosition == ene.GetPosition)
                {
                    // //msg you loose and change the scene
                    EndTimer();
                    Time.timeScale = 0f;
                    buttonlose.gameObject.SetActive(true);
                    Textfield.text = "you died :c";
                }
            }
            if (player.GetPosition == finishPoint)
            {
                NextLevel();
            }
        }
    }

    private void PlayerMoveEnemiesUpdate(object sender, EventArgs e)
    {
        foreach (Enemy ene in enemies)
        {
            //FindPath --> grid + startx,y + finishx,y
            List<Cell> path =
                PathManager
                    .Instance
                    .FindPath(grid,
                    (int) ene.GetPosition.x,
                    (int) ene.GetPosition.y,
                    (int) player.GetPosition.x,
                    (int) player.GetPosition.y);
            ene.SetPath (path);
        }
    }    

    // Start is called before the first frame update
    private void Start()
    {
        //Active!!!!!
        // Ncells = InitialValues.Nvalue;
        // Mcells = InitialValues.Mvalue;

        Timertext.text = "Time: 00:00.00";
        timerGoing = false;

        level = 1;
        BeginTimer();
        Time.timeScale = 1f;
        //set final point
        TextLevel.text = "Layer: "+level.ToString();
        bool k = false;
        while(k == false)
        {
            int yfinal = Random.Range(0, Ncells);
            int xfinal = Random.Range(0, Ncells);
            finishPoint = new Vector2(xfinal, yfinal);
            grid =
                new Grid(Ncells,
                    Ncells,
                    1,
                    Mcells,
                    finishPoint,
                    cellPrefabfinish,
                    cellWallPrefab,
                    CellPrefab);

            //set random startpoint only if the point is isWalkable
            while (i == false)
            {
                y = Random.Range(0, Ncells);
                x = Random.Range(0, Ncells);
                Point = grid.GetGridObject(x, y);
                if (Point.isWalkable == true && x != xfinal && y != yfinal)
                {
                    i = true;
                    startPoint = new Vector2(x, y);
                }
            }
            List<Cell> path =PathManager.Instance.FindPath(grid,(int) startPoint.x,(int) startPoint.y,(int) finishPoint.x,(int) finishPoint.y);
            if(path != null)
            {
                player = Instantiate(PlayerPrefab, startPoint, Quaternion.identity);
                player.setGrid (grid);
                k = true;
            }else{
                DestroyGrid();
            }
        }
        //set random start point only if the point is isWalkable for enemy
        i = false;
        while (i == false)
        {
            y = Random.Range(0, Ncells);
            x = Random.Range(0, Ncells);
            Point = grid.GetGridObject(x, y);
            //add condition to different place to the player
            if (Point.isWalkable == true && x != startPoint.x && y != startPoint.y)
            {
                i = true;
                startPoint = new Vector2(x, y);
                Enemy enemy = Instantiate(EnemyPrefab, startPoint, Quaternion.identity);
                enemies.Add (enemy);
            }
        }
        player.OnPlayerMove += PlayerMoveEnemiesUpdate;
    }

    private void NextLevel()
    {
        // Color c = new Color32(144,2, 255, 100);
        ready = false;
        StartCoroutine(sendNotification("Next Layer Run!!! \n another ghost appear",Color.white, 1));
        if (level <= 4)
        {
            level++;
            TextLevel.text = "Layer: "+level.ToString();
            Destroy (GameObject.FindWithTag("Player"));
            GameObject[] taggedEnemies= GameObject.FindGameObjectsWithTag("Enemy");  
            foreach (GameObject ene in taggedEnemies) 
            {
	            Destroy(ene);
            }
            GameObject[] taggedCells = GameObject.FindGameObjectsWithTag("Cell");   
            foreach (GameObject cell in taggedCells) {
	            Destroy(cell);
            }
            enemies.Clear();
            startPoint = Vector2.zero;
            //set final point
            bool k = false;
            while(k == false){

                int yfinal = Random.Range(0, Ncells);
                int xfinal = Random.Range(0, Ncells);
                finishPoint = new Vector2(xfinal, yfinal);
                grid =
                    new Grid(Ncells,
                        Ncells,
                        1,
                        Mcells,
                        finishPoint,
                        cellPrefabfinish,
                        cellWallPrefab,
                        CellPrefab);
                //set random startpoint only if the point is isWalkable
                i = false;
                while (i == false)
                {
                    y = Random.Range(0, Ncells);
                    x = Random.Range(0, Ncells);
                    Point = grid.GetGridObject(x, y);
                    if (Point.isWalkable == true && x != xfinal && y != yfinal)
                    {
                        i = true;
                        startPoint = new Vector2(x, y);
                    }
                }
                List<Cell> path =PathManager.Instance.FindPath(grid,(int) startPoint.x,(int) startPoint.y,(int) finishPoint.x,(int) finishPoint.y);
                if(path != null)
                {
                    player = Instantiate(PlayerPrefab, startPoint, Quaternion.identity);
                    player.setGrid (grid);
                    k = true;
                }else{
                    DestroyGrid();
                }
            }  
            int cont = 0;
            i = false;
            while (i == false && cont != level)
            {
                y = Random.Range(0, Ncells);
                x = Random.Range(0, Ncells);
                Point = grid.GetGridObject(x, y);
                //add condition to different place to the player
                if(Point.isWalkable == true && x != startPoint.x && y != startPoint.y)
                {
                    startPoint = new Vector2(x, y);
                    Enemy enemy = Instantiate(EnemyPrefab, startPoint, Quaternion.identity);
                    enemies.Add(enemy);
                    cont++;
                }
                if(cont == level)
                {
                    i = true;
                }
            }
            ready = true;
            player.OnPlayerMove += PlayerMoveEnemiesUpdate;
        }
        else
        {
            EndTimer();
            SceneManager.LoadScene("Final");
        }
    }


    private void DestroyGrid()
    {
        GameObject[] taggedCells = GameObject.FindGameObjectsWithTag("Cell");   
        foreach (GameObject cell in taggedCells) {
	            Destroy(cell);
        }
    }

    IEnumerator sendNotification(string text,Color colors , int time)
    {
        Textfield.color = colors;
        Textfield.text = text;
        yield return new WaitForSeconds(time);
        Textfield.text = "";
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            Timertext.text = timePlayingStr;

            yield return null;  
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject[,] grass = new GameObject[100, 100];
    public GameObject[,] grassFantom = new GameObject[100, 100];
    GameObject plant;
    int i, j;
    public GameObject Grass;
    public GameObject rabbit5;
    public GameObject information;
    public GameObject changeparam;
    public GameObject help;
    public static float grassSum = 0;
    public static float healthRabbit = 0;
    public Text myText;
    public Text myText2;
    public Text Speedgame;
    public Text growthrateGrass;
    public Text Healthrabbit;
    public float grassSpeed = 1f;
    public float time = 1;
    public float CurrentTime;
    public float GameSeconds;
    public float GameMinutes;
    string StringSecond;
    string StringMinutes;
    public Text TextTime;
    [Header("Button")]
    public Button exit;
    public Button stop;
    public Button continue1;
    public Button close;
    public Button plusspeedgame;
    public Button minusspeedgame;
    public Button plusgrowthrate;
    public Button minusgrowthrate;
    public Button plushealthrabbit;
    public Button minushealthrabbit;
    public KeyCode showMenuKey = KeyCode.Escape;

    void Start()
    {
        exit.onClick.AddListener(Exit);
        stop.onClick.AddListener(Stop);
        continue1.onClick.AddListener(Continue);
        close.onClick.AddListener(Close);
        plusspeedgame.onClick.AddListener(Plusspeedgame);
        minusspeedgame.onClick.AddListener(Minusspeedgame);
        plusgrowthrate.onClick.AddListener(Plusgrowthrate);
        minusgrowthrate.onClick.AddListener(Minusgrowthrate);
        plushealthrabbit.onClick.AddListener(Plushealthrabbit);
        minushealthrabbit.onClick.AddListener(Minushealthrabbit);
        Time.timeScale = 10;
        grassSeeder(50, 50);
        grassSeeder(55, 50);
        grassSeeder(51, 53);
        InvokeRepeating("grassRun", grassSpeed, grassSpeed);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Stop()
    {
        Time.timeScale = 0f;
    }

    public void Continue()
    {
        Time.timeScale = 10f;
    }

    public void Close()
    {
        information.SetActive(false);
        help.SetActive(true);
    }

    public void Plusspeedgame()
    {
        Time.timeScale++;
    }

    public void Minusspeedgame()
    {
        Time.timeScale--;
    }

    public void Plusgrowthrate()
    {
        grassSpeed++;
    }

    public void Minusgrowthrate()
    {
        grassSpeed--;
    }

    public void Plushealthrabbit()
    {
        healthRabbit++;
    }

    public void Minushealthrabbit()
    {
        healthRabbit--;
    }

    private void grassSeeder(int x, int y)
    {
        GameObject plant = Instantiate(Grass, new Vector3(x * 0.5f + Random.Range(-0.4f, 0.4f), 0, y * 0.5f + Random.Range(-0.4f, 0.4f)), transform.rotation);
        grass[x, y] = plant; // - трава
    }

    private void grassFantomSeeder(int x, int y)
    {
        GameObject plant = Instantiate(Grass, new Vector3(x * 0.5f + Random.Range(-0.4f, 0.4f), 0, y * 0.5f + Random.Range(-0.4f, 0.4f)), transform.rotation);
        grassFantom[x, y] = plant; // - трава
        grassSum++;
    }

    private void grassRun()
    {
        for (i = 0; i < 100; i++)
        {
            for (j = 0; j < 100; j++)
            {
                grassFantom[i, j] = grass[i, j];  
            }
        }

        for (i = 0; i < 100; i++)
        {
            for (j = 0; j < 100; j++)
            {
                if (grass[i, j] != null)
                {
                    if (i != 100 - 1) if (grassFantom[i + 1, j] == null) grassFantomSeeder(i + 1, j);
                    if (i != 0) if (grassFantom[i - 1, j] == null) grassFantomSeeder(i - 1, j);
                    if (j != 100 - 1) if (grassFantom[i, j + 1] == null) grassFantomSeeder(i, j + 1);
                    if (j != 0) if (grassFantom[i, j - 1] == null) grassFantomSeeder(i, j - 1);
                }
            }
        }

        for (i = 0; i < 100; i++)//порекомендовали grass.Length
        {
            for (j = 0; j < 100; j++)
            {
                grass[i, j] = grassFantom[i, j];
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(showMenuKey))
        {
            information.SetActive(true);
            help.SetActive(false);
        }

        grassSum = grassSum + AI_rabbit.sumGrass;
        healthRabbit = AI_rabbit.Health;

        GameSeconds = GameSeconds + Time.deltaTime;
        StringSecond = GameSeconds.ToString();
        StringMinutes = GameMinutes.ToString();
        CurrentTime += Time.deltaTime;

        if (GameSeconds >= 60.0f)
        {
            GameMinutes = GameMinutes + 1.0f;
            GameSeconds = 0.0f;
        }

        myText.text = "Популяция кроликов = " + AI_rabbit.counter2;
        myText2.text = "Колличество травы = " + grassSum;
        Speedgame.text = "Скорость симуляции = " + Time.timeScale;
        growthrateGrass.text = "Скорость роста травы = " + grassSpeed;
        Healthrabbit.text = "Здоровье кролика = " + healthRabbit;
        TextTime.text = "Время - " + StringMinutes + ":" + StringSecond;
    }
}
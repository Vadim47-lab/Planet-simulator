using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class start : MonoBehaviour
{
    [Header("variable")]//название типа элемента в программе (вывод переменных)
    public float CurrentTime;//считает колличество секунд
    public float GameSeconds;//количество секунд
    public float GameMinutes;//количество минут
    string StringSecond;//количество секунд в виде строки
    string StringMinutes;//количество минут в виде строки
    [Header("AudioClip")]//название типа элемента в программе (вывод аудиоклипа)
    public AudioClip OpenMenu;//включение звука открытия меню
    public AudioClip CloseMenu;//включение звука закрытия меню
    [Header("GameObject")]//название типа элемента в программе (вывод игровых объектов)
    public GameObject Menu;//игровой объект в виде панели для отображения и изменения информации объектов симулятора  public GameObject Window_grapg1;//игровой объект в виде панели для отображения графика об информации в симуляторе
    [Header("Text")]//название типа элемента в программе (вывод текста и значений)
    public Text Speedgame;//блок для вывода информации о скорости игры
    public Text growthrateGrass;//блок для вывода информации о скорости роста травы
    public Text Healthrabbit;//блок для вывода информации о количестве здоровья кролика
    public Text Healthfox; //блок для вывода информации о количестве здоровья лисы
    public Text Healthcounter; //блок для вывода информации о количестве созданных лис
    public Text Eatgrass;//блок для вывода информации о количестве съеденной травы
    public Text TextTime;//блок для вывода информации о количестве времени действия симуляции
    [Header("Button")]//название типа элемента в программе (кнопки)
    public Button exit;//кнопка: выход из симулятора
    public Button settings;//кнопка: показ настроек игры
    public Button start1;//кнопка: запуск симуляции
    //public Button level1;//кнопка: запуск симуляции
    public Button close;//кнопка: закрытие панели настройки
    public Button plusspeedgame;//кнопка: увеличение скорости симуляции
    public Button minusspeedgame;//кнопка: уменьшение скорости симуляции
    public Button plusgrowthrate;//кнопка: увеличение скорости роста травы
    public Button minusgrowthrate;//кнопка: уменьшение скорости роста травы
    public Button plusrabbithealth;//кнопка увеличивающая жизнь кролику
    public Button minusrabbithealth;//кнопка уменьшающая жизнь кролику
    public Button pluseatgrass;//кнопка увеличивающая колличество сЪеденной травы
    public Button minuseatgrass;//кнопка уменьшающая колличество сЪеденной травы
    public Button plusfoxhealth;//кнопка увеличивающая жизнь лисе
    public Button minusfoxhealth;//кнопка уменьшающая жизнь лисе

    void Start()
    {
        Time.timeScale = 1f;
        exit.onClick.AddListener(Exit);
        settings.onClick.AddListener(Settings);
        start1.onClick.AddListener(Start1);
        //level1.onClick.AddListener(Level1);
        close.onClick.AddListener(Close);
        plusspeedgame.onClick.AddListener(Plusspeedgame);
        minusspeedgame.onClick.AddListener(Minusspeedgame);
        plusgrowthrate.onClick.AddListener(Plusgrowthrate);
        minusgrowthrate.onClick.AddListener(Minusgrowthrate);
        plusfoxhealth.onClick.AddListener(Plusfoxhealth);
        minusfoxhealth.onClick.AddListener(Minusfoxhealth);
        plusrabbithealth.onClick.AddListener(Plusrabbithealth);
        minusrabbithealth.onClick.AddListener(Minusrabbithealth);
        pluseatgrass.onClick.AddListener(Pluseatgrass);
        minuseatgrass.onClick.AddListener(Minuseatgrass);
    }

    public void Plusfoxhealth()
    {
        AI_fox.StartHealth++;
    }

    public void Minusfoxhealth()
    {
        AI_fox.StartHealth--;
    }

    public void Plusrabbithealth()
    {
        AI_rabbit.StartHealth++;
    }

    public void Minusrabbithealth()
    {
        AI_rabbit.StartHealth--;
    }

    public void Pluseatgrass()
    {
        AI_rabbit.counterGrass++;
    }

    public void Minuseatgrass()
    {
        AI_rabbit.counterGrass--;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        GetComponent<AudioSource>().PlayOneShot(OpenMenu);
        Menu.SetActive(true);
    }

    public void Close()
    {
        GetComponent<AudioSource>().PlayOneShot(CloseMenu);
        Menu.SetActive(false);
    }

    public void Start1()
    {
        if (SceneManager.GetActiveScene().name == "Start") SceneManager.LoadScene("SampleScene");
    }

   /* public void Level1()
    {
        if (SceneManager.GetActiveScene().name == "Start") SceneManager.LoadScene("Level1");
    }*/

    public void Plusspeedgame()
    {
        Main.speedGame++;
    }

    public void Minusspeedgame()
    {
        Main.speedGame--;
    }

    public void Plusgrowthrate()
    {
        Main.grassSpeed++;
    }

    public void Minusgrowthrate()
    {
        Main.grassSpeed--;
    }

    void Update()
    {
        GameSeconds = GameSeconds + Time.deltaTime + .0f;
        StringSecond = GameSeconds.ToString("###");
        if (GameMinutes != 0) StringMinutes = GameMinutes.ToString() + ":";
        else
        {
            StringMinutes = "";
        }
            //CurrentTime += Time.deltaTime;

            if (GameSeconds >= 60.0f)
        {
            GameMinutes = GameMinutes + 1.0f;
            GameSeconds = 0.0f;
        }

        if (GameMinutes >= 1.0f && GameSeconds >= 30.0f)
        {
            if (SceneManager.GetActiveScene().name == "Start") SceneManager.LoadScene("SampleScene");
        }

        Speedgame.text = "Скорость симуляции = " + Main.speedGame;
        growthrateGrass.text = "Скорость роста травы = " + Main.grassSpeed;
        Healthrabbit.text = "Здоровье кролика = " + AI_rabbit.StartHealth;
        Eatgrass.text = "Количество съеденной травы = " + AI_rabbit.counterGrass;
        Healthfox.text = "Здоровье лисы = " + AI_fox.StartHealth;
        TextTime.text = "Время - " + StringMinutes + StringSecond;
    }
}
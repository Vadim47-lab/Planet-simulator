using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AI_rabbit : MonoBehaviour
{
    private float z = 0;//коррдинаты кролика по оси z
    private float x = 0;//коррдинаты кролика по оси x
    private float y = 0;//коррдинаты кролика по оси y
    private int xGrass, yGrass;//коррдинаты травы
    private int counter = 0; //текущий счетчие съеденной травы (никуда не передается)
    public static int counter2 = 1;//количество кроликов, которое передается в файл Main для дальнейшего вывода на экран
    public float sumGrass = 0;//количество травы, которое съел кролик
    public static float grassSum = 0;//количество травы для отображения на экран через файл main
    public float Health = 40;//количество здоровья у кролика, которое передается в файл Main для дальнейшего вывода на экран
    public static float counterGrass = 4;//сколько надо съесть травы для размножения
    public static float health = 0;//количество здоровья у кролика, которое отображается в inspector в unity
    public static float Sumrabbit = 0;//количество кроликов, которое отображается в inspector в unity
    public static float eatGrass2 = 0;
    public float Sumgrass = 0;//количество травы, которое отображается в inspector в unity
    public int randomT;//мозг кролика
    public int random2;//обход камня
    public GameObject rabbit;//игровой объект кролик
    public GameObject game;//пустой игровой объект со скриптом главной логики симулятора
    public GameObject Graph;
    [Header("Button")]//название типа элемента в программе (кнопки)
    //public Button plusrabbithealth;//кнопка увеличивающая жизнь кролику
    //public Button minusrabbithealth;//кнопка уменьшающая жизнь кролику
    //public Button pluseatgrass;//кнопка увеличивающая колличество сЪеденной травы
    //public Button minuseatgrass;//кнопка уменьшающая колличество сЪеденной травы
    public int maxChild = 4;//сколько кроликов можно родить
    public static int MaxChild = 0;
    public int age = 220;//биологический возраст максимальный
    public static int Age = 0;
    public BoxCollider hitBox;

    void Start()
    {
        Main.Sumrabbit++;
        age = 220;
        maxChild = 4;
        //plusrabbithealth.onClick.AddListener(Plusrabbithealth);
        //minusrabbithealth.onClick.AddListener(Minusrabbithealth);
        //pluseatgrass.onClick.AddListener(Pluseatgrass);
        //minuseatgrass.onClick.AddListener(Minuseatgrass);
        xGrass = Random.Range(0, 99);
        yGrass = Random.Range(0, 99);
        if (tag == "rabbit") Health = 40;
        InvokeRepeating("brain", 0, 1f);
        InvokeRepeating("life", 1f, 1f);
    }

    void Update()
    {
        if (age > 200) hitBox.enabled = false;
        else hitBox.enabled = true;
        if (transform.position.y > 0.01)
        {
            //counter2--;
            //Main.Sumrabbit--;
            Destroy(gameObject);
        }
        int xRabbit, yRabbit;
        transform.Rotate(x, y, z);
        xRabbit = Mathf.RoundToInt(transform.position.x * 2f) - 1;
        yRabbit = Mathf.RoundToInt(transform.position.z * 2f) - 1;
        GameObject grass = null;
        if (xRabbit >= 0 && xRabbit < 100 && yRabbit >= 0 && yRabbit < 100)
        {
            grass = game.GetComponent<Main>().grass[xRabbit, yRabbit];
        }

        //if (Health > game.GetComponent<Main>().healthRabbit) Health = game.GetComponent<Main>().healthRabbit;
        if (grass != null && Health <= 20)
        {
            game.GetComponent<Main>().grass[xRabbit, yRabbit] = null;
            Destroy(grass);
            Main.grassSum1--;
            //sumGrass--;
            counter++;
            if (Main.start == true) eatGrass2++;
            if (counter >= counterGrass)
            {
                Health = 40;
                create();
                counter = 0;
                //counter2++;
                //Main.Sumrabbit++;
                maxChild--;
                if (maxChild == 0)
                {
                    // counter2--;
                    //Main.Sumrabbit--;
                    Destroy(gameObject);
                }
            }
            
        }
        health = Health;
        Sumgrass = counter;
        grassSum = Sumgrass;
        Sumrabbit = counter2;//для вывода информации в unity в inspector
        Age = age;
        MaxChild = maxChild;
        // List<int> valueList = new List<int>() { 5, 23, 54, 67, 98, 32, 54, 65, 32 };
        // valueList.Add(counter2);
        //if (AI_rabbit.counter2 > maxCounter2) maxCounter2 = AI_rabbit.counter2;
        //List<int> valueList = new List<int>() {5, 23, 54, 67, 98, 32, 54, 65, 32};
        // Graph.GetComponent<Window_graph>().ShowGraph(valueList);

        // не должен кролик передавать инфу в граф, их же легион, Main.Sumrabbit вот где хранится нужно число для отображения
    }

    public void Plusrabbithealth()
    {
        Health++;
    }

    public void Minusrabbithealth()
    {
        Health--;
    }

    public void Pluseatgrass()
    {
        counterGrass++;
    }

    public void Minuseatgrass()
    {
        counterGrass--;
    }

    private void brain()
    {
        age--;
        if (age == 0)
        {
            //counter2--;
            //Main.Sumrabbit--;
            Destroy(gameObject);
        }
        //Получить случайное число (в диапазоне от 0 до 1)
        randomT = Random.Range(0, 10);
        if (Health <= 20) randomT = 11;
        switch (randomT)
        {
           case 0:
               y = 0;
               if (tag == "rabbit") GetComponent<Animator>().SetBool("Run", false);
               break;
           case 1:
           case 2:
           case 3:
           case 4:
               if (tag == "rabbit") GetComponent<Animator>().SetBool("Run", true);
               y = 0;
               break;
           case 5:
           case 6:
           case 7:
                if (tag == "rabbit") GetComponent<Animator>().SetBool("Run", true);
               y = 2;
                break;
           case 8:
           case 9:
           case 10:
                if (tag == "rabbit") GetComponent<Animator>().SetBool("Run", true);
               y = -2;
                break;
           case 11:
                y = 0;
                GameObject grass = null;
                if (game.GetComponent<Main>().grass[xGrass, yGrass] == null)
                {
                    grass = findGrass();
                }
                else
                {
                    grass = game.GetComponent<Main>().grass[xGrass, yGrass];
                }
                if (grass == null)
                {
                    if (tag == "rabbit") GetComponent<Animator>().SetBool("Run", false);
                    break;
                }
                transform.LookAt(grass.transform);
                if (tag == "rabbit") GetComponent<Animator>().SetBool("Run", true);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        random2 = Random.Range(0, 1);
        if (other.tag == "stone" && random2 == 0)
        {
            y = 2;
        }
        if (other.tag == "stone" && random2 == 1)
        {
            y = -2;
        }
    }

    private void create()
    {
        Instantiate(rabbit, transform.position, transform.rotation);
    }

    private void life()
    {
         if (tag == "rabbit")
         {
             Health--;
        }
        if (Health <= 0 && tag == "rabbit")
        { 
            //GetComponent<Animator>().SetBool("Death", true);
            Destroy(gameObject);
            //counter2--;
            //Main.Sumrabbit--;
        }
    }

    private void OnDestroy()
    {
        Main.Sumrabbit--;
    }

    private GameObject findGrass()
    {
        int i, j;
        int count = 0;
        for (i = 0; i < 100; i++)
        {
            for (j = 0; j < 100; j++)
            {
                if (game.GetComponent<Main>().grass[i, j] != null) count++;
            }
        }
        int random = Random.Range(1, count);
        count = 0;
        for (i = 0; i < 100; i++)
        {
           for (j = 0; j < 100; j++)
           {
                GameObject grass = game.GetComponent<Main>().grass[i, j];
                if (grass != null)
                {
                    count++;
                    if (count == random)
                    {
                        xGrass = i;
                        yGrass = j;
                        return grass;
                    }
                }
           }
        }
        return null;
    }
}
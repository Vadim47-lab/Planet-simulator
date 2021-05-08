using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AI_rabbit : MonoBehaviour
{
    private float z = 0;//коррдинаты кролика по оси z
    private float x = 0;//коррдинаты кролика по оси x
    private float y = 0;//коррдинаты кролика по оси y
    private int xGrass, yGrass;//коррдинаты травы
    public static float counter = 0; //колличество кроликов, которое передается в файл Main для дальнейшего вывода на экран
    public static float counter2 = 0;//колличество кроликов, которое передается в файл Main для дальнейшего вывода на экран
    public static float sumGrass = 0;//колличество травы, которое передается в файл Main для дальнейшего вывода на экран
    public static float Health = 40;//колличество здоровья у кролика, которое передается в файл Main для дальнейшего вывода на экран
    public static float currentHealth = 40;//текущая установка здоровья кролика
    public static float counterGrass = 4;//текущая установка съеденной травы
    public float health = 0;//колличество здоровья у кролика, которое отображается в inspector в unity
    public float Sumrabbit = 0;//колличество травы, которое отображается в inspector в unity
    public float Sumgrass = 0;//колличество кроликов, которое отображается в inspector в unity
    public int randomT;//мозг кролика
    public GameObject rabbit;//игровой объект кролик
    public GameObject game;//пустой игровой объект со скриптом главной логики симулятора
    [Header("Button")]//название типа элемента в программе (кнопки)
    public Button plusrabbithealth;//кнопка увеличивающая жизнь кролику
    public Button minusrabbithealth;//кнопка уменьшающая жизнь кролику
    public Button pluseatgrass;//кнопка увеличивающая колличество сЪеденной травы
    public Button minuseatgrass;//кнопка уменьшающая колличество сЪеденной травы

    void Start()
    {
        plusrabbithealth.onClick.AddListener(Plusrabbithealth);
        minusrabbithealth.onClick.AddListener(Minusrabbithealth);
        pluseatgrass.onClick.AddListener(Pluseatgrass);
        minuseatgrass.onClick.AddListener(Minuseatgrass);
        xGrass = Random.Range(0, 99);
        yGrass = Random.Range(0, 99);
        if (tag == "rabbit") currentHealth = Health;
        InvokeRepeating("brain", 0, 1f);
        InvokeRepeating("life", 1f, 1f);
    }

    void Update()
    {
        if (transform.position.y > 0.01) Destroy(gameObject);
        int xRabbit, yRabbit;
        transform.Rotate(x, y, z);
        xRabbit = Mathf.RoundToInt(transform.position.x * 2f)-1;
        yRabbit = Mathf.RoundToInt(transform.position.z * 2f)-1;
        GameObject grass = null;
        if (xRabbit>=0 && xRabbit<100 && yRabbit >= 0 && yRabbit < 100)
        {
            grass = game.GetComponent<Main>().grass[xRabbit, yRabbit];
        }

        //if (Health > game.GetComponent<Main>().healthRabbit) Health = game.GetComponent<Main>().healthRabbit;
        if (grass != null && Health <= 20)
        {
            if (grass != null) Main.grassSum--;
            Destroy(grass);
            //if (grass == null) sumGrass--;
            //Debug.Log("sumGrass = " + Main.grassSum);
            counter++;
            if (counter >= counterGrass)
            {
                Health = currentHealth;
                create();
                counter = 0;
                counter2++;
            }
            game.GetComponent<Main>().grass[xRabbit, yRabbit] = null;
        }
        health = Health;
        Sumgrass = counter;
        Sumrabbit = counter2;//для вывода информации в unity в inspector
    }

    public void Plusrabbithealth()
    {
        currentHealth++;
        if (currentHealth > 40) currentHealth = 40;
    }

    public void Minusrabbithealth()
    {
        currentHealth--;
        if (currentHealth < 20) currentHealth = 20;
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
           case 9:
           case 5:
           case 6:
                if (tag == "rabbit") GetComponent<Animator>().SetBool("Run", true);
               y = 2;
               break;
           case 7:
           case 8:
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

    private void create()
    {
        Instantiate(rabbit, transform.position, transform.rotation);
    }

    private void life()
    {
        if (tag == "rabbit") Health--;
        if (Health <= 0 && tag == "rabbit")
        { 
            //GetComponent<Animator>().SetBool("Death", true);
            Destroy(gameObject, 2f);
            counter2--;
        }
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
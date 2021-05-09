using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AI_fox : MonoBehaviour
{
    private float z = 0;//коррдинаты кролика по оси z
    private float x = 0;//коррдинаты кролика по оси x
    private float y = 0;//коррдинаты кролика по оси y
    public float Health = 40;//колличество здоровья у лисы
    public static float health = 0;//колличество здоровья у лисы, которое передается в файл Main для дальнейшего вывода на экран
    public GameObject target = null;//игровой объект для поедания лисой
    public GameObject fox;//игровой объект лиса
    public int random;//мозг лисы
    public float counter = 1;//колисчество созданных лис
    public static float counter5 = 0;//колисчество созданных лис, которое передается в файл Main для дальнейшего вывода на экран
    public Button plusfoxhealth;//кнопка увеличивающая жизнь лисе
    public Button minusfoxhealth;//кнопка уменьшающая жизнь лисе

    void Start()
    {
        plusfoxhealth.onClick.AddListener(Plusrabbithealth);
        minusfoxhealth.onClick.AddListener(Minusrabbithealth);
        target = null;
        //GameObject[] rabbits = GameObject.FindGameObjectsWithTag("rabbit");
        //target = rabbits[Random.Range(0, rabbits.Length)];
        y = 0f;
        Health = 40;
        InvokeRepeating("brain", 0, 1f);
        InvokeRepeating("life", 1f, 1f);
    }

    public void Plusrabbithealth()
    {
        Health++;
    }

    public void Minusrabbithealth()
    {
        Health--;
    }

    private void Update()
    {
        health = Health;
        counter5 = counter;
    }

    void FixedUpdate()
    {
        transform.Rotate(x, y, z);
        //transform.LookAt(target.transform.position);
    }

    private void brain()
    {
        random = Random.Range(0, 10);
        if (Health <= 20) random = 11;
        switch (random)
        {
            case 0:
                y = 0;
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", false);
                break;
            case 1:
            case 2:
            case 3:
            case 4:
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                y = 0;
                break;
            case 5:
            case 6:
            case 7:
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                y = 2;
                break;
            case 8:
            case 9:
            case 10:
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                y = -2;
                break;
            case 11:
                if (tag == "fox") GetComponent<Animator>().SetBool("Run", true);
                if (target == null)
                {
                    GameObject[] rabbits = GameObject.FindGameObjectsWithTag("rabbit");
                    target = rabbits[Random.Range(0, rabbits.Length)];
                }
                else
                {
                    transform.LookAt(target.transform);
                }
                break;
        }
    }

    private void life()
    {
        Health--;
        if (Health <= 0 && tag == "fox")
        {
            //GetComponent<Animator>().SetBool("Death", true);
            Destroy(gameObject, 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rabbit" && Health < 20)
        {
            Health = 40;
            target = null;
            Destroy(other.gameObject);
            Instantiate(fox, transform.position, transform.rotation);
            counter++;
        }
    }
}
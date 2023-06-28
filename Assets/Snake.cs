using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 dir = Vector2.up;
    List<Transform> tail = new List<Transform> ();
    public GameObject tailPrefab;
    bool eat = false;
    Spawn new_food;
    void Start()
    {
        InvokeRepeating("Move", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //obiekt jest przesuwany ca³y czas do przodu, a po klikniêciu klawisza jest obracany na osi Z
            //dir = Vector2.right;
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, -90);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //dir = Vector2.left;
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 90);
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            //dir = Vector2.up;
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) )
        {
            //dir = Vector2.down;
            GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 180);
        }
    }
    void Move()
    {
        Vector2 ruch = transform.position;
        transform.Translate(dir);

        if (eat) // Je¿eli snake natrafi na jedzenie, zwiêksza ogon o jeden element
        {
            GameObject f = (GameObject)Instantiate(tailPrefab, ruch, Quaternion.identity);
            tail.Insert(0, f.transform);
            eat = false;
        }

        else if (tail.Count>0)//Sprawdzam, czy snake posiada ogon i je¿eli tak to przesuwam go
        {
            tail.Last().position = ruch;
            //Je¿eli tak to przesuwam ogon o jeden
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count-1);
        }
    }

    void gameReset()
    {
        //Funkcja gameReset bêdzie usuwaæ elementy w liscie tail i sprowadzaæ g³owe snake do punktu pocz¹tkowego
        for(int i = 0;i<tail.Count;i++)
        {
            Destroy(tail[i].gameObject);
        }
        tail.Clear();
        this.transform.position = Vector2.zero; 
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.name.StartsWith("FoodPrefab"))
        {
            eat = true;
            //Niszcze znaleziony obiekt, w tym przypadku food
            Destroy(coll.gameObject);
            //£¹czê klasy poprzed taga, który posiada foodPrefab pod nazw¹ Player i wyci¹gam funkcje spawn()
            new_food = GameObject.FindGameObjectWithTag("Player").GetComponent<Spawn>();
            new_food.spawn();
        }
        //W pozosta³ych wypadkach wywo³uje funkcje gameReset
        if (coll.name.StartsWith("BorderTop") || coll.name.StartsWith("BorderBottom") || coll.name.StartsWith("BorderRight") || coll.name.StartsWith("BorderLeft"))
        {
            gameReset();
        }
        else if (coll.name.StartsWith("TailPrefab"))
        {
            gameReset();
        }
    }
}

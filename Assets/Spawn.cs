using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject foodPrefab;
    public Transform BorderTop;
    public Transform BorderBottom;
    public Transform BorderLeft;
    public Transform BorderRight;
    public BoxCollider2D gridarea;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("spawn", 2, 3);
        spawn();
    }

    // Update is called once per frame
    public void spawn()
    {
        //ogranicza pojawianie siê jedzenia do zakresu x i y gridarea, ¿eby jedzenie nie pojawia³o siê w borderach
        Bounds bounds = this.gridarea.bounds;
        int i = (int)Random.Range(bounds.min.x,bounds.max.x);
        int r = (int)Random.Range(bounds.min.y,bounds.max.y);
        /*GameObject f =  Instantiate(foodMom, new Vector2(i, r), Quaternion.identity);
        f.name = "food_" + id++;*/
        Instantiate(foodPrefab, new Vector2(i, r), Quaternion.identity);
    }
}

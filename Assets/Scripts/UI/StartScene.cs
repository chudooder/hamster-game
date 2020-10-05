using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private bool hasStarted = false;
    private int count = 0;
    [SerializeField] private GameObject HamsterPrefab;
    [SerializeField] private int HamsterLimit = 500;
    [SerializeField] private float HamsterRate = 2;
    private Vector2 minPoint;
    private Vector2 maxPoint;
    [SerializeField] private HamsterDefaultValues hamsterDefaultValues;

    
    public void StartGame()
    {
        GameManager.instance.BeginRun();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HamsterRain());

        minPoint = Camera.main.ViewportToWorldPoint(Vector2.zero);
        maxPoint = Camera.main.ViewportToWorldPoint(Vector2.one);
        Debug.Log(minPoint);
        var colObj = Instantiate(new GameObject());
        
        EdgeCollider2D col = colObj.AddComponent<EdgeCollider2D>();
        col.points = new[]
        {
            new Vector2(minPoint.x, maxPoint.y), 
            new Vector2(minPoint.x, minPoint.y), 
            new Vector2(maxPoint.x, minPoint.y), 
            new Vector2(maxPoint.x, maxPoint.y), 
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey && !hasStarted)
        {
            StartGame();
            hasStarted = true;
        }
    }

    IEnumerator HamsterRain()
    {
        while (count < HamsterLimit)
        {
            SpawnHamsterAtPoint();   
            count++;
            yield return new WaitForSeconds(1f / HamsterRate);
        }
    }

    void SpawnHamsterAtPoint()
    {
        Color bellyColor = hamsterDefaultValues.bellyColors[Random.Range(0, hamsterDefaultValues.bellyColors.Count)];
        Color bodyColor = hamsterDefaultValues.bodyColors[Random.Range(0, hamsterDefaultValues.bodyColors.Count)];
        float x = Random.Range(minPoint.x, maxPoint.x);
        float y = Random.Range(minPoint.y, maxPoint.y);
        GameObject obj = Instantiate(HamsterPrefab, new Vector3(x, y), Quaternion.identity);
        obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = bellyColor;
        obj.transform.GetChild(1).GetComponent<SpriteRenderer>().color = bellyColor;
        obj.transform.GetChild(2).GetComponent<SpriteRenderer>().color = bellyColor;
        obj.transform.GetComponent<SpriteRenderer>().color = bodyColor;
    }
}

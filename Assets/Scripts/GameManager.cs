using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Current variables
    private int _currentScore = 0;
    private int _overallScore = 0;
    private int _currentHealth = 0;

    

    
    // Settings Variables
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int ScoreMultiplier = 1; // multiply this by the distance and add the score
    
    // Scenes
    [SerializeField] private Object mainMenuScene; 
    [SerializeField] private Object storeScene;
    [SerializeField] private Object loadOutScene;
    [SerializeField] private Object gameScene;

    public int MaxHealth => _maxHealth;
    public int CurrentScore => _currentScore;
    public int CurrentHealth => _currentHealth;
    
    public Action HealthListener;
    public Action ScoreListener;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
 
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChanged;

        _currentHealth = _maxHealth;
        _currentScore = 0;
    }

    public void BeginRun()
    {
        _currentHealth = _maxHealth;
        _currentScore = 0;
        SceneManager.LoadScene(gameScene.name);
        //todo enable overlay
        //todo spawn hamsters chosen by user in relevant places
    }

    private void OnSceneChanged(Scene old, Scene next)
    {
        if (next.name.Equals(gameScene.name))
        {
            HamsterManager.instance.BeginRun();
        }
    }

    public void AddDistance(int distance)
    {
        _currentScore += ScoreMultiplier * distance;
        //TODO update overlay
        //TODO check/update maturity of baby hamsters
    }
    
    public void Repair(int repairAmount)
    {
        _currentHealth += repairAmount;
        //TODO update overlay
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;
        //TODO update overlay
        if (_currentHealth < 0)
        {
            EndRun();
        }
        HealthListener.Invoke();
    }

    public void AddScore(int score)
    {
        _currentScore += score;
        ScoreListener.Invoke();
    }
    
    private void EndRun()
    {
        //TODO play death/run end animation
        //TODO change scene
        _overallScore += _currentScore;
        SceneManager.LoadScene(storeScene.name);
    }

}

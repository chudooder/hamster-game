using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Current variables
    private int _currentScore = 0;
    private int _overallScore = 0;
    private int _currentHealth = 0;

    public int CurrentScore => _currentScore;
    public int CurrentHealth => _currentHealth;
    
    // Settings Variables
    [SerializeField] private int MaxHealth = 100;
    [SerializeField] private int ScoreMultiplier = 1; // multiply this by the distance and add the score
    
    // Scenes
    [SerializeField] private Scene mainMenuScene; 
    [SerializeField] private Scene storeScene;
    [SerializeField] private Scene loadOutScene;
    [SerializeField] private Scene gameScene;

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
    }

    public void BeginRun()
    {
        _currentHealth = MaxHealth;
        _currentScore = 0;
        SceneManager.LoadScene(gameScene.name);
        //todo enable overlay
        //todo update overlay
        //todo spawn hamsters chosen by user in relevant places
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
    }

    private void EndRun()
    {
        //TODO play death/run end animation
        //TODO change scene
        _overallScore += _currentScore;
        SceneManager.LoadScene(storeScene.name);
    }

}

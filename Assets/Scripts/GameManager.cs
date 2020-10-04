using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // Current variables
    private int _currentScore = 0;
    private int _overallScore = 0;
    private int _currentHealth = 0;
    
    // Settings Variables
    [SerializeField] private readonly int MaxHealth = 100;
    [SerializeField] private readonly int ScoreMultiplier = 1; // multiply this by the distance and add the score
    
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
        //todo change scenes
        //todo enable overlay
        //todo spawn hamsters chosen by user in relevant places
    }

    public void AddDistance(int distance)
    {
        _currentScore += ScoreMultiplier * distance;
        //TODO update UI
        //TODO check/update maturity of baby hamsters
    }
    
    public void Repair(int repairAmount)
    {
        _currentHealth += repairAmount;
        //TODO update UI
    }

    public void Damage(int damage)
    {
        _currentHealth -= damage;
        //TODO update UI
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
    }

}

using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public static PlayerLevelManager Instance { get; private set;}
    public System.Action<int> OnLevelUp;
    public int currentLevel = 1;
    public float playerExp = 0;
    public float nextLevelThreshold = 21;

    public void Awake()
    {
        Instance = this;
    }

    public void TriggerLevelUp()
    {
        nextLevelThreshold += nextLevelThreshold;
        playerExp = 0;
        OnLevelUp.Invoke(currentLevel);
        currentLevel += 1;
        Debug.Log($"Level-up! New Level: {currentLevel}");
    }
    public void GainExp(float expValue)
    {
        playerExp += expValue;

        if (playerExp > nextLevelThreshold)
        {
            TriggerLevelUp();
        }
    }

    void Update()
    {
        // testing only
        if (Input.GetKeyDown(KeyCode.L))
        {
            TriggerLevelUp();
        }

    }
}

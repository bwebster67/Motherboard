using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public static PlayerLevelManager Instance { get; private set;}
    public System.Action<int> OnLevelUp;
    public System.Action<float, float> OnGainExp;
    public int currentLevel = 1;
    public float playerExp = 0;
    public float nextLevelThreshold = 10;

    public void Awake()
    {
        Instance = this;
    }

    public void TriggerLevelUp()
    {
        playerExp -= nextLevelThreshold;
        nextLevelThreshold += nextLevelThreshold/2;
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

        OnGainExp.Invoke(playerExp, nextLevelThreshold);
    }

    void Update()
    {
        if (GameStateMachine.debug) 
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                TriggerLevelUp();
            }
        }

    }
}

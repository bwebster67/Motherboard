using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public static PlayerLevelManager Instance { get; private set;}
    public System.Action<float> OnLevelUp;
    public System.Action<float> OnGainExp;
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
        OnLevelUp.Invoke(nextLevelThreshold);
        currentLevel += 1;
        Debug.Log($"Level-up! New Level: {currentLevel}");
    }

    public void GainExp(float expValue)
    {
        playerExp += expValue;

        if (playerExp >= nextLevelThreshold)
        {
            TriggerLevelUp();
        }

        OnGainExp.Invoke(playerExp);
    }

    void Update()
    {
        if (GameStateMachine.debug) 
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                GainExp(nextLevelThreshold - playerExp);
            }
        }

    }
}

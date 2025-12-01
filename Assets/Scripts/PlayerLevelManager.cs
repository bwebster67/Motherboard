using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    public System.Action<int> OnLevelUp;
    public int currentLevel = 1;

    public void TriggerLevelUp()
    {
        OnLevelUp.Invoke(currentLevel);
        currentLevel += 1;
        Debug.Log($"Level-up! New Level: {currentLevel}");
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

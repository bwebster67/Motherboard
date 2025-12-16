using UnityEngine;
using UnityEngine.UI;

public class PlayUIManager : MonoBehaviour
{
    public GameObject expBarGO;
    public Slider expBarSlider;
    public PlayerLevelManager playerLevelManager;

    void OnEnable()
    {
        playerLevelManager.OnGainExp += HandleGainExp; 
    }
    void OnDisable()
    {
        playerLevelManager.OnGainExp -= HandleGainExp; 
    }

    void Awake()
    {
        if (playerLevelManager == null) playerLevelManager = FindAnyObjectByType<PlayerLevelManager>();
        expBarSlider = expBarGO.GetComponent<Slider>();
    }

    void Start()
    {
        UpdateExpBar(playerLevelManager.playerExp, playerLevelManager.nextLevelThreshold);
    }

    private void UpdateExpBar(float currentExp, float nextLevelThreshold)
    {
        expBarSlider.maxValue = nextLevelThreshold;
        expBarSlider.value = currentExp;
    }
    private void HandleGainExp(float currentExp, float nextLevelThreshold)
    {
        UpdateExpBar(currentExp, nextLevelThreshold);
    }
}

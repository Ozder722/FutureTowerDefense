using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveHandler waveHandler;
    [SerializeField] private TMP_Text currentWave;
    [SerializeField] private TMP_Text allWaves;

    public void Start()
    {
        // Start med at sætte tælleren til 0 og opdatér teksten
        waveHandler.waveCounter = 0;
        currentWave.text = waveHandler.waveCounter.ToString();
    }

    // Vi fjerner Update() helt, da det er "overkill" at tjekke hver frame
    // for noget, der kun sker ved et klik.

    public void PlayButtonPressed()
    {
        // 1. Læg 1 til din waveCounter
        waveHandler.waveCounter++;

        // 2. Opdatér teksten med den nye værdi
        currentWave.text = waveHandler.waveCounter.ToString();
    }

}
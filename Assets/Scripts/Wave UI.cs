using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveHandler waveHandler;
    [SerializeField] private WaveHandler waves;
    [SerializeField] private WaveHandler currentWaveIndex;
    [SerializeField] private TMP_Text currentWave;
    [SerializeField] private TMP_Text allWaves;

    public void Start()
    {
        // Start med at sætte tælleren til 0 og opdatér teksten
        waveHandler.waveCounter = 0;
        currentWave.text = waveHandler.waveCounter.ToString();
    }

    public void PlayButtonPressed()
    {
            // + 1 til din waveCounter
            waveHandler.waveCounter++;

            // Opdatere teksten
            currentWave.text = waveHandler.waveCounter.ToString();
     
    }

}
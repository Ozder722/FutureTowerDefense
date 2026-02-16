using UnityEngine;
using TMPro;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private WaveHandler waveHandler;
    [SerializeField] private TMP_Text currentWave;
    [SerializeField] private TMP_Text allWaves;

    public void Start()
    {
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
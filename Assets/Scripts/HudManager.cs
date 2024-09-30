using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour
{
    public static HudManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private GameObject playerObject;
    
    public Slider hpBar;
    public Text hpText;

    public Slider transformCdBar;
    public Image transformCdImg;

    public Image BearBg;
    public Image TurtleBg;
    public Image LeopardBg;

    public GameObject GameOverHud;
    public GameObject EndGameHud;
    

    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        BarControl();
    }
    private void BarControl()
    {
        hpBar.maxValue = playerObject.GetComponent<EntityStats>().maxHp;
        hpBar.value = playerObject.GetComponent<EntityStats>().hp;
        hpText.text = $"{playerObject.GetComponent<EntityStats>().hp} | {playerObject.GetComponent<EntityStats>().maxHp}";

        transformCdBar.maxValue = playerObject.GetComponent<TransformBehavior>().transformCdr;
        transformCdBar.value = playerObject.GetComponent<TransformBehavior>().transformCooldown;
        if (transformCdBar.value >= playerObject.GetComponent<TransformBehavior>().transformCdr)
        {
            transformCdImg.color = Color.green;
        } 
        else
        {
            transformCdImg.color = Color.yellow;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene"); // Altere "SampleScene" para o nome exato da sua cena
        Time.timeScale = 1;
    }
}

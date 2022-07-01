///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   TriggerToNextScene.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/03/12
///   Description       : A trigger for the tutorial level change scene/level
///   Revision History  : 5th edition.  Turns off tutorial flames when player warps to Main level
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class TriggerToNextScene : MonoBehaviour
{
    public Level whichLevelTriggerIsThis;

    public Transform MainLevelSpawnPoint;

    public TextMeshProUGUI ObjectiveDescrition;

    public GameObject loadingScreen;
    public GameObject onScreenControls;

    public ParticleSystem particles;
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = false;
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
        particles.Stop();
    }
    private void Update()
    {
        if (GameManager.GetInstance().currentLevel == Level.Tutorial)
        {
            if (Data.spellBooksCount <= 0)
            {
                particles.Play();
            }
        }
        else if (GameManager.GetInstance().currentLevel == Level.MainLevel)
        {
            if (Data.enemyCount <= 0)
            {
                particles.Play();
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
            else
            {
                particles.Stop();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Tutorial Level
        if (GameManager.GetInstance().currentLevel == Level.Tutorial)
        {
            if (Data.spellBooksCount <= 0)
            {
                
                other.gameObject.GetComponent<CharacterController>().enabled = false;
                other.gameObject.GetComponent<Transform>().position = MainLevelSpawnPoint.position;
                other.gameObject.GetComponent<CharacterController>().enabled = true;

                // Transition to next scene
                loadingScreen.SetActive(true);
                loadingScreen.GetComponentInChildren<Animator>().SetTrigger("Loading");
                onScreenControls.SetActive(false);
                GameManager.GetInstance().levelIsLoading = true;
                StartCoroutine(DelayForLoadingScreen());
                GameManager.GetInstance().DeactivateTutorialFlames();
                GameManager.GetInstance().DeactivateTutorialSpikes();

            }
        }
        else if (GameManager.GetInstance().currentLevel == Level.MainLevel)
        {
            if (Data.enemyCount <= 0)
            {
                Data.isWon = true;
                SceneManager.LoadScene("EndScreen");
                float volume = 0;
                if (PlayerPrefs.HasKey("Volume"))
                {
                    AudioManager.GetInstance().PlaySceneTrack(AudioManager.MusicTrack.BGM_EndScene, 0.5f, volume);
                }
            }
        }
    }

    IEnumerator DelayForLoadingScreen()
    {
        yield return new WaitForSeconds(2.5f);
        loadingScreen.SetActive(false);
        onScreenControls.SetActive(true);
        GameManager.GetInstance().currentLevel = Level.MainLevel;
        ObjectiveDescrition.text = "Kill all enemies";
        GameManager.GetInstance().TMP_ObjectiveText.text = GameManager.GetInstance().levelObjective;
        GameManager.GetInstance().levelIsLoading = false;
        gameObject.SetActive(!gameObject.activeInHierarchy);

    }
}

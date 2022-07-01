///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   SceneController.cs
///   Author            : Geek's Garage
///   Last Modified     : 2022/04/07
///   Description       : SceneController script manages the transition between scenes.
///   Revision History  : 5th ed. Commented out Debug.Log
///----------------------------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [Header("Inventory Menu")]
    private GameObject inventory;
    private GameObject inventoryButton;

    [Header("Pause Menu")]
    private GameObject pausePanel;
    private GameObject pauseButton;
    public GameObject OnScreenControls;
    public GameObject objectiveText;
    [Header("unloaded Scene")]
    private static string unloadedScene;

    [Header("Inventory Slots")]
    public GameObject[] slots;
    public GameObject listOfPickUps;
    public IEnumerable<ISaveable> saveables;
    private float volume;
    private void Update()
    {
        if (SaveManager.isLoading)
        {

            saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();

            if (saveables.Count() > 0)
            {
                SaveManager.isLoading = false;
                SaveManager.Load(saveables);
            }
        }
    }
    public void OnStartButtonPressed()
    {
        
        SceneManager.LoadScene("GameplayScreen");
        volume = ChangeVolume();
        AudioManager.GetInstance().PlaySceneTrack(AudioManager.MusicTrack.BGM_PlayScene,0.5f ,volume);
    }

    public void OnLoadButtonPRessed()
    {
        SaveManager.isLoading = true;
        SceneManager.LoadScene("GameplayScreen");
        volume = ChangeVolume();
        AudioManager.GetInstance().PlaySceneTrack(AudioManager.MusicTrack.BGM_PlayScene, 0.5f, volume);

    }
    public void OnSaveButtonPRessed()
    {
        saveables = FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>().ToList();
        SaveManager.Save(saveables);
    }
    public void OnMainMenuButtonPressed()
    {
        SceneManager.LoadScene("MenuScreen");
         volume = ChangeVolume();
        AudioManager.GetInstance().PlaySceneTrack(AudioManager.MusicTrack.BGM_StartScene, 0.5f,volume);
    }
    public void OnCloseButtonPressed()
    {
        
        Scene currentScene = SceneManager.GetActiveScene();
        
        switch (SceneManager.GetActiveScene().name)
        {
            //if the current scene is gameplay scene it means the user clicked on the close inventory button
            case "GameplayScreen":
                OnScreenControls.SetActive(true);
                inventory.SetActive(false);
                    inventoryButton.GetComponent<Button>().interactable = true; ;
                
                break;
                //REMOVE
            case "InventoryTest":

                inventory.SetActive(false);
             
                inventoryButton.GetComponent<Button>().interactable = true; ;

                break;
            //if the current scene is options scene and the previous scene was main menu it means
            //the player has gone from menu to options scene so now we want to go back to the main menu scene

            //if the current scene is options scene and the previous scene was gameplay it means
            //the player has gone from gameplay scene to options scene so now we want to go back to the gameplay scene
            case "OptionsScreen":
                if(unloadedScene=="GameplayScreen")
                SceneManager.LoadScene("GameplayScreen");
                else if (unloadedScene == "MenuScreen")
                {
                    SceneManager.LoadScene("MenuScreen");
                    var volume = ChangeVolume();
                    AudioManager.GetInstance().PlaySceneTrack(AudioManager.MusicTrack.BGM_StartScene, 0.5f,volume);
                }
               
                break;

        }
    }
    private void OnSceneUnloaded(Scene current)
    {
       unloadedScene= current.name;
       
    }
    public void OnExitButtonPressed()
    {
        Application.Quit();

        #region EditorQuit
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#endif


        #endregion

    }
    public void OnPauseButtonPressed()
    {
        if (pausePanel == null)
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject go in allObjects)
            {
                if (go.CompareTag("PausePanel"))
                    pausePanel = go;
            }
        }
        OnScreenControls.SetActive(false);
        objectiveText.SetActive(false);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton");
        pauseButton.GetComponent<Button>().interactable = false;
    }
    public void OnInventoryButtonPressed()
    {
        if (inventory == null)
        {
           GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>(true);
            foreach (GameObject go in allObjects)
            {
                if (go.CompareTag("Inventory"))
                    inventory = go;
            }
        }
        OnScreenControls.SetActive(false);
        inventory.SetActive(true);
     
      
        inventoryButton = GameObject.FindGameObjectWithTag("InventoryButton");
        inventoryButton.GetComponent<Button>().interactable = false ;

    }
    public void OnResumeButtonPressed()
    {
        OnScreenControls.SetActive(true);
        objectiveText.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        pauseButton.GetComponent<Button>().interactable = true; ;
    }
  

    // @negar: Temporary function, please feel free to remove the component associated with it.
    public void ChangeToEndScene()
    {
        SceneManager.LoadScene(3);
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        var volume = ChangeVolume();
        //Debug.Log("volume of end scene" + volume);
        AudioManager.GetInstance().PlaySceneTrack(AudioManager.MusicTrack.BGM_EndScene, 0.5f,volume);
    }

    /// <summary>
    /// Change volume for screen transitions
    /// </summary>
    /// <returns></returns>
    private float ChangeVolume()
    {
        float volume = 0;
        if (PlayerPrefs.HasKey("Volume"))
        {
            volume = PlayerPrefs.GetFloat("Volume");
        }
        else
        {
            volume = AudioManager.GetInstance().GetMaxVolume();
        }

        return volume;
    }
}

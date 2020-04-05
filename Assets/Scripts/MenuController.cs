using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;
    
    // outlets
    public GameObject topMenu;
    public GameObject abilityMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    public bool isMainMenu = false;

    void Awake()
    {
        instance = this;
        if (isMainMenu)
        {
            Show();
        }
        else
        {
            Hide();
        }
        
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (PlayerController.instance != null)
        {
            PlayerController.instance.isPaused = false;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        ShowTopMenu();
        Time.timeScale = 0;
        if (PlayerController.instance != null)
        {
            PlayerController.instance.isPaused = true;
        }
        
    }

    void SwitchMenu(GameObject someMenu)
    {
        // turn off all menus
        topMenu.SetActive(false);
        if (abilityMenu != null)
        {
            abilityMenu.SetActive(false);
        }
        settingsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        
        // turn on one menu
        someMenu.SetActive(true);
    }


    public void ShowTopMenu()
    {
        SwitchMenu(topMenu);
    }

    public void ShowAbilityMenu()
    {
        SwitchMenu(abilityMenu);
    }

    public void ShowSettingsMenu()
    {
        SwitchMenu(settingsMenu);
    }

    public void ShowControlsMenu()
    {
        SwitchMenu(controlsMenu);
    }

    public void LoadThisScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void NewGame()
    {
        LoadThisScene("Tutorial");
    }
}

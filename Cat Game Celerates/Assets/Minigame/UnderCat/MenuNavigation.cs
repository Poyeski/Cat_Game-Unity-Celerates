using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuNavigation : MonoBehaviour
{
    public Button[] mainMenuButtons;
    public GameObject submenuPanel;
    public Button[] submenuButtons;
    public Button attackButton;
    public Button defendButton;
    public Button HelpButton;
    public string nextSceneName;
    public TextMeshProUGUI messageText; 
    public TextMeshProUGUI helpText;
    private int currentMainMenuButtonIndex = 0;
    private int currentSubmenuButtonIndex = 0;
    private bool isInSubmenu = false;
    private bool isHelpDisplayed = false;
    public static Enemy selectedEnemy;
    private BulletHellManager bulletHellManager;

    void Start()
    {
        bulletHellManager = FindObjectOfType<BulletHellManager>();
        ShowMessage(); 
    }

    void Update()
    {
        if (bulletHellManager.IsBulletHellCompleted())
        {
            HandleMenuNavigation();
        }
    }

    void HandleMenuNavigation()
    {
        if (!isInSubmenu)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NavigateMainMenu(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                NavigateMainMenu(1);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                mainMenuButtons[currentMainMenuButtonIndex].onClick.Invoke();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                NavigateSubmenu(-1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                NavigateSubmenu(1);
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                submenuButtons[currentSubmenuButtonIndex].onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                HideSubmenu();
                ShowMessage(); 
            }
        }
    }

    void NavigateMainMenu(int direction)
    {
        mainMenuButtons[currentMainMenuButtonIndex].OnDeselect(null);
        currentMainMenuButtonIndex = (currentMainMenuButtonIndex + direction + mainMenuButtons.Length) % mainMenuButtons.Length;
        mainMenuButtons[currentMainMenuButtonIndex].Select();
    }

    void NavigateSubmenu(int direction)
    {
        submenuButtons[currentSubmenuButtonIndex].OnDeselect(null);
        currentSubmenuButtonIndex = (currentSubmenuButtonIndex + direction + submenuButtons.Length) % submenuButtons.Length;
        submenuButtons[currentSubmenuButtonIndex].Select();
    }

    public void ShowSubmenu()
    {
        isInSubmenu = true;
        submenuPanel.SetActive(true);
        currentSubmenuButtonIndex = 0;
        submenuButtons[currentSubmenuButtonIndex].Select();
        HideMessage();
        HideHelp();
    }

    public void HideSubmenu()
    {
        isInSubmenu = false;
        submenuPanel.SetActive(false);
        currentMainMenuButtonIndex = 0;
        mainMenuButtons[currentMainMenuButtonIndex].Select();
    }

    public void ShowMainMenu()
    {
        isInSubmenu = false;
        submenuPanel.SetActive(false);
        currentMainMenuButtonIndex = 0;
        mainMenuButtons[currentMainMenuButtonIndex].Select();
        ShowMessage();
    }

    public void ShowMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(true);
        }
    }

    public void HideMessage()
    {
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
    }

    public void SelectEnemy(Enemy enemy)
    {
        selectedEnemy = enemy;
        FindObjectOfType<AttackTimingBar>().StartAttack();
    }

    public void StartDefense()
    {
        HideSubmenu(); 
        bulletHellManager.StartBulletHell(true);
        DisableAttackButton();
        DisableDefendButton();
        DisableHelpButton();
    }
    public void ShowHelp()
    {
        isHelpDisplayed = true;
        HideMessage();
        HideSubmenu();
        helpText.gameObject.SetActive(true); 
    }

    public void HideHelp()
    {
        isHelpDisplayed = false;
        helpText.gameObject.SetActive(false); 
    }

    public void DisableAttackButton()
    {
        if (attackButton != null)
        {
            attackButton.interactable = false;
        }
    }

    public void EnableAttackButton()
    {
        if (attackButton != null)
        {
            attackButton.interactable = true;
        }
    }

    public void DisableDefendButton()
    {
        if (defendButton != null)
        {
            defendButton.interactable = false;
        }
    }

    public void EnableDefendButton()
    {
        if (defendButton != null)
        {
            defendButton.interactable = true;
        }
    }

    public void DisableHelpButton()
    {
        if (HelpButton != null)
        {
            HelpButton.interactable = false;
        }
    }

    public void EnableHelpButton()
    {
        if (HelpButton != null)
        {
            HelpButton.interactable = true;
        }
    }

    public void TransitionToNextScene()
    {
        GameSceneManager.Instance.TransitionToNextScene(nextSceneName);
    }

    public void RestartScene()
    {
        GameSceneManager.Instance.RestartScene();
    }
}
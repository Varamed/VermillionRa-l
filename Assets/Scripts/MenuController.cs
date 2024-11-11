using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject newGamePanel;
    [SerializeField] GameObject continuePanel;
    [SerializeField] GameObject bestiaryPanel;
    [SerializeField] GameObject settingsPanel;

    public void MainMenu()
    {
        CloseAllMenus();
        mainMenuPanel.SetActive(true);
    }
    public void NewGame()
    {
        CloseAllMenus();
        newGamePanel.SetActive(true);
    }
    public void Continue()
    {
        CloseAllMenus();
        continuePanel.SetActive(true);
    }
    public void Bestiary()
    {
        CloseAllMenus();
        bestiaryPanel.SetActive(true);
    }
    public void Settings()
    {
        CloseAllMenus();
        settingsPanel.SetActive(true);
    }
    private void CloseAllMenus()
    {
        mainMenuPanel.SetActive(false);
        newGamePanel.SetActive(false);
        continuePanel.SetActive(false);
        settingsPanel.SetActive(false);
        bestiaryPanel.SetActive(false);
    }
    public void Quit()
    {
       Application.Quit();
    }
}

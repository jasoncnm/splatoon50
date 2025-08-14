using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject main;
    [SerializeField] GameObject options;
    [SerializeField] GameObject languages;


    private void Start()
    {
        loadMainPage();
    }

    public void loadMainPage()
    {
        main.SetActive(true);
        options.SetActive(false);
        languages.SetActive(false);
    }

    public void loadOptionsPage()
    {
        main.SetActive(false);
        options.SetActive(true);
        languages.SetActive(false);
    }

    public void loadLanguagePage()
    {
        main.SetActive(false);
        options.SetActive(false);
        languages.SetActive(true);
    }

}

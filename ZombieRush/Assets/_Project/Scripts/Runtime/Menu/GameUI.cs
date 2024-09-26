using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;

    private void Start()
    {
        mainMenu.newGameButton.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame()
    {
        mainMenu.gameObject.SetActive(false);
    }
}

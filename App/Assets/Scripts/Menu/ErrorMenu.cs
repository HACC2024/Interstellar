using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ErrorMenu : Panel
{
    [SerializeField] private TextMeshProUGUI errorText = null;
    [SerializeField] private TextMeshProUGUI buttonText = null;
    [SerializeField] private Button actionButton = null;

    public enum Action
    {
        None = 0, StartService = 1, SignIn = 2, OpenAuthMenu = 3
    }
    
    private Action action = Action.None;
    
    public override void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }
        actionButton.onClick.AddListener(ButtonAction);
        base.Initialize();
    }

    public override void Open()
    {
        action = Action.None;
        base.Open();
    }
    
    public void Open(Action action, string error, string button)
    {
        Open();
        this.action = action;
        if (string.IsNullOrEmpty(error) == false)
        {
            errorText.text = error;
        }
        if (string.IsNullOrEmpty(button) == false)
        {
            buttonText.text = button;
        }
    }
    
    private void ButtonAction()
    {
        Close();
        switch (action)
        {
            case Action.StartService:
                MenuManager.Singleton.StartClientService();
                break;
            case Action.SignIn:
                MenuManager.Singleton.SignInWithUsernameAndPasswordAsync("user", "password"); // Adjust this if needed
                break;
            case Action.OpenAuthMenu:
                PanelManager.CloseAll();
                PanelManager.Open("auth");
                break;
        }
    }
}

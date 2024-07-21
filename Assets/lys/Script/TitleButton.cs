using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButton : MonoBehaviour
{
    public enum ButtonType
    {
        Start,
        Option,
        Exit,
    }

    public ButtonType type;

    public Button btn;

    void Start()
    {
        var btnMgr = FindObjectOfType<ButtonManager>();
        switch (type)
        {
            case ButtonType.Start:
                btn.onClick.AddListener(btnMgr.StartGame);
                break;
            case ButtonType.Option:
                btn.onClick.AddListener(btnMgr.OptionpPanel);
                break;
            case ButtonType.Exit:
                btn.onClick.AddListener(btnMgr.ExitGame);
                break;
        }
    }
}

///----------------------------------------------------------------------------------
///   Riona the Empire Saviour
///   UIController.cs
///   Author            : Geek’s Garage
///   Last Modified     : 2022/04/18
///   Description       : UI Controller
///   Revision History  : 4th ed. Added SFX for change element
///----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("UI Button Controller")]
    public Button CombatButton;
    public Button JumpButton;
    public Button AttackButton;
    public Button ElementButton;
    public Image elementIcon;
    public GameObject AttackJoystick;

    public static bool CombatMode;

    public static bool JumpButtonDown;

    private PlayerBehaviour player;

    public List<Sprite> ElementSymbols;

    private void Start()
    {
        CombatMode = false;
        player = FindObjectOfType<PlayerBehaviour>();
    }

    public void OnJumpButtonDown()
    {
        JumpButtonDown = true;
    }
    public void OnJumpButtonUp()
    {
        JumpButtonDown = false;
    }

    /// <summary>
    /// Toggle Combat mode toggle
    /// </summary>
    public void Button_CombatModeToggle()
    {
        CombatMode = !CombatMode;
        player.ToggleCombatMode();
        if (CombatMode)
        {
            int i = 0;
            foreach (var image in CombatButton.GetComponentsInChildren<Image>())
            {
                image.color = Color.white;
            }
        }
        else
        {
            Color white = Color.white;
            // Set half transparency
            white.a = 0.5f;

            foreach (var image in CombatButton.GetComponentsInChildren<Image>())
            {
                image.color = white;
            }
        }

        // (De/)Activate jump and attack buttons
        JumpButton.gameObject.SetActive(!JumpButton.gameObject.activeInHierarchy);
        AttackButton.gameObject.SetActive(!AttackButton.gameObject.activeInHierarchy);
        ElementButton.gameObject.SetActive(!ElementButton.gameObject.activeInHierarchy);
        AttackJoystick.SetActive(!AttackJoystick.activeInHierarchy);
        
    }

    /// <summary>
    /// Attack function on button press
    /// </summary>
    public void Button_Attack()
    {
        player.Attack();
    }


    /// <summary>
    /// Change the element of the attack with visual feedback
    /// </summary>
    public void Button_ChangeElement()
    {
        AttackTypes currentType = player.ChangeElement();

        switch (currentType)
        {
            case AttackTypes.FIRE:
                elementIcon.sprite = ElementSymbols[(int)AttackTypes.FIRE];
                break;

            case AttackTypes.ICE:
                elementIcon.sprite = ElementSymbols[(int)AttackTypes.ICE];
                break;

            case AttackTypes.LIGHTNING:
                elementIcon.sprite = ElementSymbols[(int)AttackTypes.LIGHTNING];
                break;

            default:
                elementIcon.sprite = ElementSymbols[(int)AttackTypes.FIRE];
                break;
        }
    }
}

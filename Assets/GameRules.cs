using Assets.Scripts.VehicleControllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameRules : MonoBehaviour {

    public int playerHP;

    [SerializeField]
    public List<Image> hpSprites;

    [SerializeField]
    private Image pBarSprite;

    [SerializeField]
    private Vehicle playerVehicle;
    [SerializeField]
    private PlayerVehicleController playerVehicleController;

    void Update () {
        for (int i = 0; i < hpSprites.Count; ++i)
        {
            hpSprites[i].enabled = playerVehicle.GetHP() > i;
        }
        pBarSprite.fillAmount = playerVehicle.IsPheonix() ?
            playerVehicleController.GetPheonixPercentage() :
            playerVehicleController.GetGearingPercentage();
        pBarSprite.color = playerVehicle.IsPheonix() ?
            Color.red :
            Color.white;
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class AbilityManager : MonoBehaviour
{

    public Image speedBar;
    public Image jumpBar;
    public Image projectileBar;
    public Image powerUpBar;
    public Text pointsLeft;

    public int maxIncrements = 10;

    public int startSpeedAbility;
    public int startJumpAbility;
    public int startProjectileAbility;
    public int startPowerUpAbility;
    public int startLevels;

    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        UpdateBars();
    }

    void LoadData(){
        if(File.Exists(Application.dataPath + "/Saves/save.txt")){
            string loadString = File.ReadAllText(Application.dataPath + "/Saves/save.txt");
            //Debug.Log(loadString);

            SaveObject loadObject = JsonUtility.FromJson<SaveObject>(loadString);

            startSpeedAbility = (int) loadObject.speedAbility;
            startJumpAbility = (int) loadObject.jumpAbility;
            startProjectileAbility = (int) loadObject.projectileAbility;
            startPowerUpAbility = (int) loadObject.powerUpAbility;
            startLevels = (int) loadObject.levelsCompleted;

        }
        else{
            startSpeedAbility = 0;
            startJumpAbility = 0;
            startProjectileAbility = 0;
            startPowerUpAbility = 0;
            startLevels = 0;
        }
        
        
    }

    public void Save(){
        SaveObject saveObject = new SaveObject {
        speedAbility = (float)startSpeedAbility,
        jumpAbility = (float)startJumpAbility,
        projectileAbility = (float)startProjectileAbility,
        powerUpAbility = (float)startPowerUpAbility,
        levelsCompleted = startLevels,
        };

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/Saves/save.txt", json);
        //Debug.Log("Saving with "+json);
    }

    void UpdateBars(){
        speedBar.fillAmount = 0.05f + ((float)startSpeedAbility / (float)maxIncrements) * 0.95f;
        jumpBar.fillAmount = 0.05f + ((float)startJumpAbility / (float)maxIncrements) * 0.95f;
        projectileBar.fillAmount = 0.05f + ((float)startProjectileAbility / (float)maxIncrements) * 0.95f;
        powerUpBar.fillAmount = 0.05f + ((float)startPowerUpAbility / (float)maxIncrements) * 0.95f;

        pointsLeft.text = "Points Left:  " + CalculatePointsAvailable().ToString();
    }

    public void AddSpeed(){
        if(startSpeedAbility < maxIncrements){
            startSpeedAbility ++;
        }
        UpdateBars();
    }

    public void SubtractSpeed(){
        if(startSpeedAbility > 0){
            startSpeedAbility --;
        }
        UpdateBars();
    }

    public void AddJump(){
        if(startJumpAbility < maxIncrements){
            startJumpAbility ++;
        }
        UpdateBars();
    }

    public void SubtractJump(){
        if (startJumpAbility > 0){
            startJumpAbility --;
        }
        UpdateBars();
    }

    public void AddProjectile(){
        if (startProjectileAbility < maxIncrements){
            startProjectileAbility ++;
        }
        UpdateBars();

    }

    public void SubtractProjectile(){
        if (startProjectileAbility > 0){
            startProjectileAbility --;
        }
        UpdateBars();
    }

    public void AddPowerup(){
        if (startPowerUpAbility < maxIncrements){
            startPowerUpAbility ++;
        }
        UpdateBars();
    }

    public void SubtractPowerup(){
        if (startPowerUpAbility > 0){
            startPowerUpAbility --;
        }
        UpdateBars();        
    }

    public void NextLevel(){
        if (CalculatePointsAvailable() == 0){
            Save();
            //Debug.Log("Go to the next level");
        }
        else{
            Debug.Log(CalculatePointsAvailable().ToString());
        }
    }

    int CalculatePointsAvailable(){
        int initPoints = 25;

        return initPoints - (startJumpAbility + startPowerUpAbility + startProjectileAbility + startSpeedAbility);
    }

    private class SaveObject {
        public float speedAbility;
        public float jumpAbility;
        public float projectileAbility;
        public float powerUpAbility;
        public int levelsCompleted;
    }
}
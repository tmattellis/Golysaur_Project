using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameHandler : MonoBehaviour
{


    public static GameHandler instance;
    public int curLevels;
    public float curSpeedAbility;
    public float curJumpAbility;
    public float curProjectileAbility;
    public float curPowerUpAbility;

    void Start(){
        instance = this;
        LoadData();
        if(PlayerController.instance){
            LoadPlayer();
        }
    }

    public void Save(){
        SaveObject saveObject = new SaveObject {
        speedAbility = PlayerController.instance.speedAbility,
        jumpAbility = PlayerController.instance.jumpAbility,
        projectileAbility = PlayerController.instance.projectileAbility,
        powerUpAbility = PlayerController.instance.powerUpAbility,
        levelsCompleted = curLevels,
        };

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/Saves/save.txt", json);
    }

    public void LoadData(){
        if(File.Exists(Application.dataPath + "/Saves/save.txt")){
            string loadString = File.ReadAllText(Application.dataPath + "/Saves/save.txt");
            //Debug.Log(loadString);

            SaveObject loadObject = JsonUtility.FromJson<SaveObject>(loadString);

            curSpeedAbility = loadObject.speedAbility;
            curJumpAbility = loadObject.jumpAbility;
            curProjectileAbility = loadObject.projectileAbility;
            curPowerUpAbility = loadObject.powerUpAbility;
            curLevels = loadObject.levelsCompleted;

        }
        else{
            curSpeedAbility = 2;
            curJumpAbility = 2;
            curProjectileAbility = 2;
            curPowerUpAbility = 2;
            curLevels = 0;
        }
        
        
    }

    public void LoadPlayer(){

        PlayerController.instance.speedAbility = curSpeedAbility;
        PlayerController.instance.jumpAbility = curJumpAbility;
        PlayerController.instance.projectileAbility = curProjectileAbility;
        PlayerController.instance.powerUpAbility = curPowerUpAbility;
    }

    public void ClearSaveData()
    {
        File.Delete(Application.dataPath + "/Saves/save.txt");
    }

    private class SaveObject {
        public float speedAbility;
        public float jumpAbility;
        public float projectileAbility;
        public float powerUpAbility;
        public int levelsCompleted;
    }
}

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
    public string curDifficulty;

    void Start(){
        instance = this;
        LoadData();
        if(PlayerController.instance){
            LoadPlayer();
        }
    }

    public void NewSave(){

        SaveObject saveObject = new SaveObject {
        speedAbility = 5,
        jumpAbility = 5,
        projectileAbility = 5,
        powerUpAbility = 5,
        levelsCompleted = 0,
        difficulty = curDifficulty,
        };
        string json = JsonUtility.ToJson(saveObject);

        if(!File.Exists(Application.persistentDataPath + "/save.txt")){
            File.Create(Application.persistentDataPath + "/save.txt").Dispose();
        }

        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
    }

    public void Save(){
        SaveObject saveObject = new SaveObject {
        speedAbility = PlayerController.instance.speedAbility,
        jumpAbility = PlayerController.instance.jumpAbility,
        projectileAbility = PlayerController.instance.projectileAbility,
        powerUpAbility = PlayerController.instance.powerUpAbility,
        levelsCompleted = curLevels,
        difficulty = curDifficulty,
        };

        string json = JsonUtility.ToJson(saveObject);

        if(!File.Exists(Application.persistentDataPath + "/save.txt")){
            File.Create(Application.persistentDataPath + "/save.txt").Dispose();
        }

        File.WriteAllText(Application.persistentDataPath + "/save.txt", json);
    }

    public void LoadData(){
        if(File.Exists(Application.persistentDataPath + "/save.txt")){
            string loadString = File.ReadAllText(Application.persistentDataPath + "/save.txt");
            //Debug.Log(loadString);

            SaveObject loadObject = JsonUtility.FromJson<SaveObject>(loadString);

            curSpeedAbility = loadObject.speedAbility;
            curJumpAbility = loadObject.jumpAbility;
            curProjectileAbility = loadObject.projectileAbility;
            curPowerUpAbility = loadObject.powerUpAbility;
            curLevels = loadObject.levelsCompleted;
            curDifficulty = loadObject.difficulty;

        }
        else{
            curSpeedAbility = 2;
            curJumpAbility = 2;
            curProjectileAbility = 2;
            curPowerUpAbility = 2;
            curLevels = 0;
            curDifficulty = "Easy";
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
        File.Delete(Application.persistentDataPath + "/save.txt");
    }

    public void SetEasy(){
        curDifficulty = "Easy";
    }

    public void SetNormal(){
        curDifficulty = "Normal";
    }

    public void SetHard(){
        curDifficulty = "Hard";
    }

    private class SaveObject {
        public float speedAbility;
        public float jumpAbility;
        public float projectileAbility;
        public float powerUpAbility;
        public int levelsCompleted;
        public string difficulty;
    }
}

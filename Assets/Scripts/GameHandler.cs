using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameHandler : MonoBehaviour
{


    public static GameHandler instance;
    public int curLevels;

    void Start(){
        instance = this;
        if(PlayerController.instance){
            Load();
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

    public void Load(){
        if(File.Exists(Application.dataPath + "/Saves/save.txt")){
            string loadString = File.ReadAllText(Application.dataPath + "/Saves/save.txt");
            Debug.Log(loadString);

            SaveObject loadObject = JsonUtility.FromJson<SaveObject>(loadString);

            PlayerController.instance.speedAbility = loadObject.speedAbility;
            PlayerController.instance.jumpAbility = loadObject.jumpAbility;
            PlayerController.instance.projectileAbility = loadObject.projectileAbility;
            PlayerController.instance.powerUpAbility = loadObject.powerUpAbility;

            curLevels = loadObject.levelsCompleted;
        
        }
        
        
    }

    public void SavePlusLevel(){
        SaveObject saveObject = new SaveObject {
        speedAbility = PlayerController.instance.speedAbility,
        jumpAbility = PlayerController.instance.jumpAbility,
        projectileAbility = PlayerController.instance.projectileAbility,
        powerUpAbility = PlayerController.instance.powerUpAbility,
        levelsCompleted = curLevels + 1,
        };

        string json = JsonUtility.ToJson(saveObject);

        File.WriteAllText(Application.dataPath + "/Saves/save.txt", json);
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

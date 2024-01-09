using System.IO;
using UnityEngine;

public class JsonManager : Singleton<JsonManager>
{
    public UserData userData = new UserData();

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        DataLoad();

        return true;

    }

    public void DataLoad() // 파일 불러오기
    {
        string path = Path.Combine(Application.dataPath + "/UserData.json");
        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists) // 파일있으면 true
        {
            LoadUserDataFromJson();
        }
        else
        {
            DefaultUserData();
            SaveUserDataToJson();
        }
    }

    public void SaveUserDataToJson()
    {
        string jsonData = JsonUtility.ToJson(userData, true);
        string path = Path.Combine(Application.dataPath + "/UserData.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadUserDataFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/UserData.json");
        string jsonData = File.ReadAllText(path);
        userData = JsonUtility.FromJson<UserData>(jsonData);
    }


    public void DefaultUserData()
    {
        Debug.Log("저장파일이 없습니다. 기본값으로 설정됩니다.");

        userData.masterVolume = 100;
        userData.mouseReverse = false;
        userData.fov = 90;
        userData.sensitivity = 50;


    }
}

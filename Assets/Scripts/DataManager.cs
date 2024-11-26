using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

[Serializable]
public class Achievements{
    public bool credit500;
    public bool credit1000;
    public bool credit2000;
    public bool credit5000;

    public bool shot1000;
    public bool shot2000;
    public bool shot5000;
    public bool shot10000;
    public bool shot20000;

    public bool allJackpotRoulette;
    public bool jackpot10;
    public bool jackpot20;
    public bool jackpot30;

    public bool rareTetrapod1;
    public bool rareTetrapod2;
    public bool rareTetrapod3;
}

[Serializable]
public class Statistics{
    public int shots;
    public int playtime;
    public int[] lottery = new int[4];
    public int[] roulette = new int[4];
    public int jackpots;
    public int jackpotMax;
    public int[] jackpotResults = new int[256];
}

[Serializable]
public class SaveData{
    public int credits;
    public Achievements achievements;
    public Statistics statistics;
}

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private GameObject statistics;
    [SerializeField]
    private TMP_Text statisticsText;

    public SaveData data;
    private string path;
    private long awakeTime;
    void Awake(){
        path = Application.dataPath + "/save.json";
        if(!File.Exists(path)){
            Save(data, path);
            return;
        }
        data = Load(path);
        awakeTime = DateTime.Now.Ticks;
        statistics.transform.localScale = Vector3.one / 1920 * Screen.width;
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.S)){
            if(statistics.activeSelf){
                statistics.SetActive(false);
            }else{
                statistics.SetActive(true);
                int sum = 0, i = 0;
                for(i=0; i<256; i++){
                    if(data.statistics.jackpotResults[i] == 0) break;
                    sum += data.statistics.jackpotResults[i];
                }
                int playtime = (int)(data.statistics.playtime + Time.time);
                String playtimeS = $"{playtime / 3600:D2}:{playtime % 3600 / 60:D2}:{playtime % 60:D2}";
                statisticsText.text = $"Statistics\n\nShots : {data.statistics.shots}\nPlay Time : {playtimeS}\n" + 
                $"Lottery :\n    Total : {data.statistics.lottery.Sum()}\n    0 Hits : {data.statistics.lottery[0]}  1 Hits : {data.statistics.lottery[1]}  2 Hits : {data.statistics.lottery[2]}  3 Hits : {data.statistics.lottery[3]}\n" +
                $"Roulette : \n    Total : {data.statistics.roulette.Sum()}\n    Jackpot : {data.statistics.roulette[0]}  Gold Tetrapod : {data.statistics.roulette[1]}  20 Lotteries : {data.statistics.roulette[2]}  Double Shot : {data.statistics.roulette[3]}\n" +
                $"Jackpot : \n    Total : {data.statistics.jackpots}  Max Win : {data.statistics.jackpotMax}   Average Win : {(i==0 ? 0 : (float)sum / i)}";
            }
        }
    }

    private void Save(SaveData data, String path){
        string json = JsonUtility.ToJson(data);
        StreamWriter wr = new StreamWriter(path, false);
        wr.WriteLine(json);
        wr.Close();
    }

    private SaveData Load(string path){
        StreamReader rd = new StreamReader(path);
        string json = rd.ReadToEnd();
        rd.Close();
        return JsonUtility.FromJson<SaveData>(json); 
    }

    void OnDestroy(){
        data.statistics.playtime += (int)((DateTime.Now.Ticks - awakeTime) / 10_000_000);
        Save(data, path);
    }
}

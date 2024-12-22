using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int deathCount;
    public int currentHealth;
    public string check;
    public int spawnPointIndex;
    public int sceneIndex;
    public bool death;
    public bool newGame;
    public int chapter;
    public int perCnt;
    public int saveId;
    public GameData() { 
        this.deathCount = 0;
        this.currentHealth = 100;
        this.check = "No funciona";
        this.spawnPointIndex = 0;
        this.sceneIndex = 1;
        this.death = false;
        this.newGame = true;
        this.chapter = 0;
        this.perCnt = 0;
        this.saveId = 1;
    }
}

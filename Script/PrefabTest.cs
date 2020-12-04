using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    public GameObject[] CubePrefabs; //オブジェクトを格納する配列変数
    private float time; //出現する間隔を制御するための変数
    private int number; //ランダム情報を入れるための変数
    public int bear_number = 0;
    public int cougar_number = 0;
    public int rabbit_number = 0;
    public int tiger_number = 0;

    void Start()
    {
        time = 1.0f; //時間を待たず、最初の1回を出現
    }

    void Update()
    {
        time -= Time.deltaTime; //timeから時間を減らす
        if (time <= 0.0f) //0秒になれば
        {
            time = 1.0f; //1秒にする
            number = Random.Range(0, CubePrefabs.Length); //Random.Range (最小値, 最大値) 整数の場合は最大値は除外
            switch (number)
            {
                case 0:
                    bear_number++;
                    break;
                case 1:
                    cougar_number++;
                    break;
                case 2:
                    rabbit_number++;
                    break;
                case 3:
                    tiger_number++;
                    break;
                default:
                    break;
            }
            //Debug.Log(bear_number + ", " + cougar_number + ", " + rabbit_number + ", " + tiger_number);
            //Instantiate(CubePrefabs[number], new Vector3(-10, 0, 0), Quaternion.identity); //X座標-10にランダム出現、向きの設定は無し
            int i = 0;
            while(i < 10) {
                i++;
                Instantiate(CubePrefabs[number], new Vector3(Random.Range(-55.0f, 40.0f), 0, Random.Range(-45.0f, 45.0f)), Quaternion.Euler(0f, Random.Range(-180.0f, 180.0f), 0f));
            }
        }
    }
}
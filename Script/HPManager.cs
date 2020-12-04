using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp = 100;//hpを5にする。SliderのMaxValueとValueはここの値に合わせます
    private Slider _slider;//Sliderの値を代入する_sliderを宣言
    public GameObject slider;//体力ゲージに指定するSlider

    void Start()
    {
        _slider = slider.GetComponent<Slider>();//sliderを取得する
    }

    // Update is called once per frame
    void Update()
    {
        _slider.value = hp;//スライダーとHPの紐づけ
    }
}

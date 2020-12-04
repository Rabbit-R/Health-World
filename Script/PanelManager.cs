using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject NoticePanel;
    [SerializeField] GameObject thisPanel;
    GameObject ObjectCreate;
    PrefabTest counter;
    public Text textUI;

    void Start()
    {
        NoticePanel.SetActive(false);
        ObjectCreate = GameObject.Find("ObjectCreate");
        counter = ObjectCreate.GetComponent<PrefabTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StepPanel()
    {
        NoticePanel.SetActive(true);
        textUI.text = "You need walk more";
    }

    public void SleepPanel()
    {
        NoticePanel.SetActive(true);
        textUI.text = "You need sleep more";
    }

    public void AnimalPanel()
    {
        NoticePanel.SetActive(true);
        textUI.text = "You now have:" +
                        "\n Bear: " + counter.bear_number +
                         "\n Cougar: " + counter.cougar_number +
                         "\n Rabbit: " + counter.rabbit_number +
                        "\n Tiger: " + counter.tiger_number;
    }

    public void ClosePanel()
    {
        thisPanel.SetActive(false);
    }
}

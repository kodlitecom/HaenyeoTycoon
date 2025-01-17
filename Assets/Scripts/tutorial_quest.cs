﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class tutorial_quest : MonoBehaviour
{
    //공통 오브젝트
    public GameObject quest_ui,quest_box,quest_UIs;
    public Image touch_bg, quest_bg,text_box,triangle;
    public Text text,hilight_text;
    public static int step, quest_num;
    public static int IsQuest, item_num = 0;     //퀘스트 확인용
    public AudioSource item_click, icon_click;
    public UI_manager UI_manager;

    //farm object
    public GameObject hilight_parent, bubble_parent;
    public Image[] hilight;
    public Image sache, speech_bubble;
    public Text bubble_text;
    public string[] tutorial_texts;
    public Image Scrollbar_tuto;
    public Image sea_icon_fake;


    public IEnumerator text_coroutine, triangle_coroutine;
    static int text_done;

    void Start() {
        item_click.volume = PlayerPrefs.GetFloat("Effect_volume", 1);
        icon_click.volume = PlayerPrefs.GetFloat("Effect_volume", 1);
    }

    public void Initialize()
    {
        touch_bg.gameObject.SetActive(false);
        quest_bg.gameObject.SetActive(false);
        quest_box.SetActive(false);
        hilight_parent.SetActive(false);
        bubble_parent.gameObject.SetActive(false);
        Scrollbar_tuto.gameObject.SetActive(false);
        quest_ui.gameObject.SetActive(false);
    }

    //다음 텍스트
    public void Next_text()
    {
        item_click.PlayOneShot(item_click.clip);

        step++;
        switch (quest_num)
        {
            case 0:
                Initialize();
                break;
            case 1:
                if (text_done == 0)
                {
                    StopCoroutine(text_coroutine);
                    text_coroutine = null;
                    text_done = 1;
                    text.text = "어이 꼬맹이 너가 대신 빚을 갚겠다고? 그래 좋다.. 대신 매일 돈을 송금해야 할거다. 두고보자";
                }
                else
                {
                    StopCoroutine(triangle_coroutine);
                    triangle_coroutine = null;
                    Initialize();
                    Daddy1();
                }
                break;
            case 2:
                if (step == 2)
                {
                    bubble_text.text = tutorial_texts[1];
                }
                else if (step == 3)
                {
                    quest_bg.gameObject.SetActive(false);
                    hilight[0].gameObject.SetActive(true);

                    //bubble_parent.transform.position = new Vector3(640, 260, 0); // 화면 하단 위치
                    bubble_text.text = tutorial_texts[2];
                }
                else
                {
                    if (step != 11)
                    {
                        hilight[step - 4].gameObject.SetActive(false);
                        hilight[step - 3].gameObject.SetActive(true);
                        bubble_text.text = tutorial_texts[step - 1];
                        if (step == 7) {
                            Scrollbar_tuto.gameObject.SetActive(true);
                            touch_bg.gameObject.SetActive(false);
                        }
                        if (step == 8) {
                            Scrollbar_tuto.gameObject.SetActive(false);
                            touch_bg.gameObject.SetActive(true);
                        }
                        if (step == 9) {
                            bubble_parent.transform.position = new Vector3(bubble_parent.transform.position.x, bubble_parent.transform.position.y - 200, bubble_parent.transform.position.z);
                        }
                    }
                    else if (step == 11)
                    {
                        bubble_parent.transform.position = new Vector3(bubble_parent.transform.position.x, bubble_parent.transform.position.y + 300, bubble_parent.transform.position.z);
                        hilight[step - 4].gameObject.SetActive(false);
                        hilight[step - 3].gameObject.SetActive(true);
                        touch_bg.gameObject.SetActive(false);
                        bubble_text.text = tutorial_texts[step - 1];
                        sea_icon_fake.gameObject.SetActive(true);
                    }
                }
                break;
            case 3:
                Initialize();
                break;
            case 4:
                if (step < 4)
                {
                    bubble_text.text = tutorial_texts[step - 1];
                }
                else
                {
                    Initialize();
                }
                break;
        }
    }

    // 사채업자의 첫번째 빚재촉
    public void Sache1()
    {
        Initialize();
        touch_bg.gameObject.SetActive(true);
        quest_bg.gameObject.SetActive(true);
        quest_box.SetActive(true);
        sache.gameObject.SetActive(true);
        quest_UIs.SetActive(true);
        hilight_text.text = "";
        step = 1; quest_num = 1;

        string tmp_text = "어이 꼬맹이 너가 대신 빚을 갚겠다고? 그래 좋다.. 대신 매일 돈을 송금해야 할거다. 두고보자";

        text_coroutine = text_effect(tmp_text);
        StartCoroutine(text_coroutine);

        triangle_coroutine = triangle_effect();
        StartCoroutine(triangle_coroutine);
    }

    IEnumerator text_effect(string tmp_text)
    {
        text_done = 0;
        for (int i = 0; i < tmp_text.Length; i++)
        {
            text.text = tmp_text.Substring(0, i + 1);
            yield return new WaitForSeconds(0.05f);
        }
        text_done = 1;
    }

    IEnumerator triangle_effect()
    {
        while (true)
        {
            triangle.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            triangle.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
    }

    //아빠 1 - 게임 ui 설명
    public void Daddy1()
    {
        tutorial_texts= new string[]
        {
            "아빠 때문에 네가 고생하는 것 같아 마음이 편하지가 않구나..",
            "아직 양식장 구성에 대해 잘 모를테니 몇가지 설명을 해주마..",
            "이건 너의 소지금을 나타낸단다...",
            "이건.. 한 달동안 갚아야할 돈의 액수란다..",
            "이건 너의 체력인데, 체력이 0이되면 하루를 마무리 짓게 된단다.",
            "이건 해야할 일 목록이란다. 그 날 해야하는 내용을 확인할 수 있지..",
            "앗차, 해변가에도 알려줄 게 있었지. 이걸 한 번 눌러보겠니?",
            "양식장과 해변가는 이렇게 이동할 수 있단다.",
            "해변에는 아이템을 팔거나 살 수 있는 상점이 있고,",
            "ATM 기계를 통해 빚쟁이에게 돈을 송금할 수 있어.",
            "그리고 이 표지판을 따라가면 바다로 갈 수 있단다.. 이 참에 물질하는 법도 알려주마. 한 번 눌러보렴"
        };
        touch_bg.gameObject.SetActive(true);
        quest_bg.gameObject.SetActive(true);
        bubble_parent.SetActive(true);
        hilight_parent.SetActive(true);
        for (int i = 0; i < hilight.Length; i++)
        {
            hilight[i].gameObject.SetActive(false);
        }

        quest_Data.tutorial_quest_list[0].state = 1;    //진행중인 퀘스트로 변경
        GameObject.Find("quest_manager").GetComponent<quest_manager>().quest_contents_update(); //실시간 반영
        step = 1; quest_num = 2;
        //bubble_parent.transform.position = new Vector3(640, 450, 0); // 화면 상단 위치
        bubble_text.text = tutorial_texts[0];
    }

    //fake_sea_icon 눌렀을 때 함수
    public void sea_open()
    {
        Debug.Log("눌림");
        icon_click.PlayOneShot(icon_click.clip);
        Initialize();
        PlayerPrefs.SetInt("isQuest", 3);
        SceneManager.LoadScene("sea"); // 바다로 이동
        hilight[8].gameObject.SetActive(false);
    }


    public void Update()
    {
        IsQuest = PlayerPrefs.GetInt("isQuest", 1);
        Check_quest(IsQuest);     // quest 완료 검사
    }

    // 퀘스트 완료 확인
    public void Check_quest(int IsQuest)
    {
        switch (IsQuest)
        {
            case 3:
                for (int i = 0; i < 9; i++)
                {
                    item_num += Haenyeo.sea_item_number[i];
                }
                if (item_num > 0)
                {
                    Initialize();
                    touch_bg.gameObject.SetActive(true);
                    quest_bg.gameObject.SetActive(true);
                    bubble_parent.SetActive(true);

                    quest_Data.tutorial_quest_list[0].state = -1;    //퀘스트 목록에서 삭제
                    quest_Data.tutorial_quest_list[1].state = 1;    //진행중인 퀘스트로 변경
                    GameObject.Find("quest_manager").GetComponent<quest_manager>().quest_contents_update(); ; //실시간 반영

                    step = 1; quest_num = 3;
                    //bubble_parent.transform.position = new Vector3(640, 450, 0); // 화면 상단 위치
                    bubble_text.text = "역시 우리 해녀로구나.. 이제 잡은 자원을 양식해보렴";

                    item_num = 0;
                    PlayerPrefs.SetInt("isQuest", 4);
                }
                break;
            case 4:
                for (int i = 0; i < 9; i++)
                {
                    item_num += Haenyeo.farm_item_number[i];
                }
                if (item_num > 0)
                {
                    Initialize();
                    touch_bg.gameObject.SetActive(true);
                    quest_bg.gameObject.SetActive(true);
                    bubble_parent.SetActive(true);

                    quest_Data.tutorial_quest_list[1].state = -1;    //퀘스트 목록에서 삭제
                    quest_Data.tutorial_quest_list[2].state = 1;    //진행중인 퀘스트로 변경
                    GameObject.Find("quest_manager").GetComponent<quest_manager>().quest_contents_update(); //실시간 반영

                    step = 1; quest_num = 3;

                    if (PlayerPrefs.GetInt("storeNew", 1) == 0) quest_Data.tutorial_quest_list[2].state = -1;
                    if (PlayerPrefs.GetInt("storeNew", 1) != 0) bubble_text.text = "그렇지~ 자원은 그렇게 양식하는 거란다.. 상점도 한번 둘러보겠니?";

                    item_num = 0;
                    PlayerPrefs.SetInt("isQuest", 5);
                }
                break;
                
            case 5:
                if (PlayerPrefs.GetInt("storeNew", 1) == 0)
                {
                    tutorial_texts = new string[]
                    {
                        "체력이 0이 되면 바다에 더이상 못들어 가게 된단다...",
                        "ATM을 이용하여 돈을 송금하게 되면 하루가 지나고 체력이 다시 차게 되니",
                        "효율적으로 체력을 관리하도록 해.. 그럼 잘 부탁한다 해녀야.."
                    };

                    touch_bg.gameObject.SetActive(true);
                    quest_bg.gameObject.SetActive(true);
                    bubble_parent.SetActive(true);
                    step = 1; quest_num = 4;

                    bubble_text.text = tutorial_texts[0];
                    PlayerPrefs.SetInt("isQuest", 6);
                }
                break;
                
        }
    }

    //quest_ui open
    public void quest_ui_open()
    {
        icon_click.PlayOneShot(icon_click.clip);
        GameObject.Find("quest_manager").GetComponent<quest_manager>().quest_contents_update(); //실시간 반영
        StartCoroutine(UI_manager.UI_On(UI_manager.UIstate.quest));
    }

    public void quest_ui_close()
    {
        UI_manager.AllUIoff();
    }

    // quest_data를 gameobject로 바꿔줌
    public void Awake()
    {
        GameObject.Find("quest_manager").GetComponent<quest_manager>().quest_contents_update();
    }
    
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    [SerializeField] private TMP_Text timeText;
    [SerializeField] private GameObject timePopUp;
    [SerializeField] private GameObject clearPopUp;
    [SerializeField] private GameObject player;
    [SerializeField] private MainCamera camera;
    [SerializeField] private float goalTime = 60f;
    private float timer = 0f;

    private Vector3 initPlayerPos;
    private Quaternion initPlayerRot;

    bool timeCheck = true;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        timePopUp.SetActive(false);
        timeText.text = goalTime.ToString();
        initPlayerPos = player.transform.position;
        initPlayerRot = player.transform.rotation;
    }

    void Update()
    {
        if (goalTime <= timer && timeCheck)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            timePopUp.SetActive(true);
            timeCheck = false;
            timeText.gameObject.SetActive(false);
            player.GetComponent<Player>().DisableControl();
            player.GetComponent<Player>().ChangeMotion(Player.PLAYERANIM.DISAPPOINT);
        }
        else
        {
            timer += Time.deltaTime;
            timeText.text = Mathf.Round(goalTime - timer).ToString();
            if (Mathf.Round(goalTime - timer) <= 5f)
            {
                timeText.color = Color.red;
            }
        }
    }

    public void ReStart()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        camera.SetToInitPos();
        clearPopUp.SetActive(false);
        timeText.gameObject.SetActive(true);
        timePopUp.SetActive(false);
        player.GetComponent<Player>().SetPosition(initPlayerPos, initPlayerRot);
        player.GetComponent<Player>().EnableControl();
        timeCheck = true;
        timer = 0f;
        timeText.text = goalTime.ToString();
        timeText.color = Color.white;
    }

    public void GameClear()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        clearPopUp.SetActive(true);
        timeCheck = false;
        timeText.gameObject.SetActive(false);
        player.GetComponent<Player>().DisableControl();
        player.GetComponent<Player>().ChangeMotion(Player.PLAYERANIM.HAPPY);
    }
}

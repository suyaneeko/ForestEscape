using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CombatManager : MonoBehaviour
{
    private static CombatManager instance = null;
    [SerializeField] private Monster[] monsters;
    [SerializeField] private Player player;
    [SerializeField] private GameObject ballThrowUI;
    [SerializeField] private TextMeshProUGUI ballTimerText;
    [SerializeField] private Bat bat;

    public Vector3 startPoint; // 시작점
    public Vector3 endPoint; // 끝점
    public float travelTime = 1.7f; // 걸리는 시간
    public GameObject ballPrefab; // 생성할 공 프리팹

    private Vector3 controlPoint1;
    private Vector3 controlPoint2;
    private float ballThrowTimer = 0f;

    enum COMBAT_ID
    {
        COMBAT_BEE,
        COMBAT_SLIME
    }

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

    public static CombatManager Instance
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

    void Update()
    {
        if(ballThrowTimer > 0f)
        {
            ballThrowTimer -= Time.deltaTime;
            ballTimerText.text = Mathf.Round(ballThrowTimer).ToString();

            if (ballThrowTimer <= 0f)
            {
                ballThrowTimer = 0f;
                ballTimerText.gameObject.SetActive(false);
                PrepareBall();
            }
        }
        if(Input.GetMouseButtonDown(1))
        {
            BallStart();

        }
    }

    private void PrepareBall()
    {
        // 컨트롤 포인트 설정
        controlPoint1 = startPoint + (endPoint - startPoint) * 0.5f + Vector3.up * 2f; ;
        controlPoint2 = startPoint + (endPoint - startPoint) * 0.75f + Vector3.up * 4f;

        // 공 이동 시작
        LaunchBall();
    }

    private void LaunchBall()
    {
        GameObject ball = Instantiate(ballPrefab, startPoint, Quaternion.identity);
        Ball ballMovement = ball.GetComponent<Ball>();
        if (ballMovement != null)
        {
            ballMovement.InitiateMovement(controlPoint1, controlPoint2, endPoint, travelTime);
        }
    }

    void StartCombat(COMBAT_ID eID)
    {
        switch(eID)
        {
            case COMBAT_ID.COMBAT_BEE:
                break;
            case COMBAT_ID.COMBAT_SLIME:
                break;
        }
    }

    public void CombatStart(int combatNum, Vector3 pos, Vector3 fowardVec)
    {
        //endPoint = pos + new Vector3(0f, 2.15f, 0.7f);
        endPoint = pos;
        Debug.Log(pos);
        startPoint = endPoint + fowardVec * 8f;
        Vector3 monsterPos = pos + fowardVec * 1.1f;
        monsterPos.y += 0.3f;

        Instantiate(monsters[combatNum], monsterPos, Quaternion.identity);
        Vector3 newTarget = pos + fowardVec * 2f;
        newTarget.y += 1f;
        bat.SetTartget(newTarget);

        ballThrowUI.SetActive(true);
        player.ReadyCombat();
    }

    public void BallStart()
    {
        ballThrowUI.SetActive(false);
        ballTimerText.gameObject.SetActive(true);
        ballThrowTimer = 3f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum MONSTER_ID
{
    COMBAT_BEE,
    COMBAT_SLIME
}

public class CombatManager : MonoBehaviour
{
    private static CombatManager instance = null;

    [SerializeField] private Monster[] monsters;
    [SerializeField] private Player player;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private GameObject ballThrowUI;
    [SerializeField] private TextMeshProUGUI ballTimerText;
    [SerializeField] private Bat bat;

    public Vector3 startPoint;
    public Vector3 endPoint;
    public float travelTime = 1.7f;

    private Vector3 controlPoint1;
    private Vector3 controlPoint2;
    private float ballThrowTimer = 0f;
    private CombatInfo curCombat;
    private Vector3 monsterPos;

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
        // 베지어 곡선 컨트롤 포인트 설정
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

    public void CombatStart(CombatInfo combatInfo, Vector3 pos, Vector3 fowardVec)
    {
        curCombat = combatInfo;
        endPoint = pos;
        Debug.Log(pos);
        startPoint = endPoint + fowardVec * 8f;
        monsterPos = pos + fowardVec * 1.1f;
        monsterPos.y += 0.3f;

        Instantiate(monsters[(int)combatInfo.monsterID], monsterPos, Quaternion.identity);
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

    public void CheckCombat(uint deadNum)
    {
        curCombat.monsterNum -= deadNum;
        if (curCombat.monsterNum == 0)
        {
            // 전투 끝남
            Debug.Log("End Game");
            player.EndCombat();
        }
        else
        {
            if (deadNum > 0)
            {
                StartCoroutine(MonsterDelay());
            }
            else
                BallStart();
        }
    }

    IEnumerator MonsterDelay()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(monsters[(int)curCombat.monsterID], monsterPos, Quaternion.identity);
        BallStart();
    }
}

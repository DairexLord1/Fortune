using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading.Tasks;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class WheelController : MonoBehaviour
{
    [Tooltip ("base place for wheel")]
    [SerializeField] Transform wheelParent;
    WheelBase wheel;

    /// <summary>
    /// this is for simple game, can be mofyfied wth different prefabs
    /// </summary>
    [Tooltip ("wheel prefab")]
    [SerializeField] GameObject wheelPrefab;


    /// <summary>
    /// text witch current coins status
    /// </summary>
    [SerializeField] TMP_Text coinsText;
    [SerializeField] DB dB;
    [SerializeField] TMP_Text winText;

    [Tooltip ("Time wheel rotates in seconds")]
    [SerializeField] float rotateTime = 3;

    [SerializeField] AudioManager audioManager;

    private void Awake()
    {
        winText.gameObject.SetActive(false);
        ShowCoins(0);
    }

    private void Start()
    {
        InstantiateWheel(wheelPrefab);
    }

    void InstantiateWheel(GameObject prefab)
    {
       GameObject go = Instantiate(prefab, wheelParent);
       wheel = go.GetComponent<WheelBase>();
       wheel.sppenButton.onClick.AddListener(() => PrepareRotate());
    }


    async void PrepareRotate()
    {
        if (!wheel.sppenButton.enabled)
            return;

        audioManager.PlayAuido(2,false);

        wheel.sppenButton.enabled = false;

        if (await SetRandomReward())
        {
           await RotateWheel();
           GetResult();
        }
    }

    async  Task  RotateWheel()
    {
        float t = rotateTime;
        audioManager.PlayAuido(1, true);
        wheel.rigid.AddTorque(1000, ForceMode2D.Force);


        while (t > 0)
        {
            wheel.rigid.angularVelocity -= Time.deltaTime * 100;
            t -= Time.deltaTime;
            await Task.Yield();
        }

        wheel.rigid.angularVelocity = 0;
        audioManager.StopAudio();
        return;
    }


    void ShowCoins(int coins)
    {
        dB.coins += coins;
        coinsText.text = FormatNumber(dB.coins);
    }

    async void GetResult()
    {
        audioManager.PlayAuido(3, false);
        string result = await wheel.GetWin(wheel.transform.eulerAngles.z);

        winText.text = "You Win!!/" + result;

        int coins = int.Parse(result);
        ShowCoins(coins);

        winText.gameObject.SetActive(true);
      
        await Task.Delay(2000);
      
        wheel.sppenButton.enabled = true;
        winText.gameObject.SetActive(false);
        audioManager.StopAudio();
    }

    static string FormatNumber(int number)
    {
        if (number >= 1000000)
        {
            return (number / 1000000).ToString("0.0") + "m";
        }
        else if (number >= 1000)
        {
            return (number / 1000).ToString("0.0") + "k";
        }
        else
        {
            return number.ToString("0");
        }
    }

    async Task<bool>  SetRandomReward()
    {
        List<int> nums = await GetNumber(16);
        wheel.SetSectorsData(nums.ToArray());

        return true;
    }

    public async Task<List<int>> GetNumber(int n)
    {
        List<int> uniqueCoinValues = new List<int>();

        int minCoins = 1000;

        for (int i = 0; i < n; i++)
        {
            int newNum = minCoins * Random.Range(1, 101);

            if (i > 0)
            {
                while (uniqueCoinValues[i - 1] + 1000 == newNum || uniqueCoinValues[i - 1] - 1000 == newNum)
                {
                    newNum = minCoins * Random.Range(1, 101);
                    await Task.Yield();
                }
            }

            uniqueCoinValues.Add(newNum);
        }

        return uniqueCoinValues;
    }
}

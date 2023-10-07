using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;
using System;
using Unity.VisualScripting;

public abstract class WheelBase: MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    public Rigidbody2D rigid => _rigidbody;

    private Button _sppenButton;
    public Button sppenButton => _sppenButton;

    protected TextMeshProUGUI[] _sectors;
    public TextMeshProUGUI[] sectors => _sectors;

    protected int sectorsNum;

    private void Awake()
    {
        _rigidbody = GetComponentInChildren<Rigidbody2D>();
        _sppenButton = GetComponentInChildren<Button>();
        _sectors = GetComponentsInChildren<TextMeshProUGUI>();
        sectorsNum = _sectors.Length;
    }

    public void SetSectorsData(int[] data)
    {
        for (int i = 0; i < sectors.Length; i++)
        {
           sectors[i].text = data[i].ToString();
        }
    }

    public abstract  Task<string> GetWin(float degree);
   
}

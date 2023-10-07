using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class WheelSixteen : WheelBase
{
    public override async Task<string> GetWin(float degree)
    {
        float sector = 0;

        ///count  sector degree
        float secDegr = 360 / sectorsNum;

        sector = Math.Abs(degree) / secDegr;
        int sctr = (int)sector;

        _rigidbody.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (secDegr * sctr) + secDegr / 2);

        return sectors[sctr].text;
    }
}

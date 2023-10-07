using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class PointsGenerator 
{
	public static async  Task<List<int>> GetNumber(int n)
	{
        List<int> uniqueCoinValues = new List<int>();
        // Очистка списка уникальных значений перед каждой генерацией
        uniqueCoinValues.Clear();

        int minCoins = 1000;
    


       for(int i =0; i< n; i++)
        {
            int newNum = minCoins * UnityEngine.Random.Range(1, 101);

            if (i > 0)
            {
                while (uniqueCoinValues[i-1] + 1000 == newNum || uniqueCoinValues[i - 1] - 1000 == newNum)
                {
                    newNum = minCoins * UnityEngine.Random.Range(1, 101);
                    await Task.Yield();
                }
            }

            uniqueCoinValues.Add(newNum);
        }

        return uniqueCoinValues;
    }

}


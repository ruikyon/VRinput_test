using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField]
    private GameObject wallPrefab;

    private void Awake()
    {
        var mapText = (Resources.Load("map", typeof(TextAsset)) as TextAsset).text;
        var mapData = mapText.Split('\n');

        Debug.Log(mapText);

        for (var i = 0; i < mapData.Length; i++)
        {
            Debug.Log(mapData[i].Length);
            for (var j = 0; j < mapData[0].Length; j++)
            {
                if (mapData[i][j] == '#')
                {
                    var basePos = new Vector3(2 * i + 1, 1, 2 * j + 1);
                    //壁生成
                    //(0, 0), (max, max)は例外的に左、右を開ける
                    if (i == 0 || mapData[i - 1][j] == '.')
                    {
                        Instantiate(wallPrefab, basePos+Vector3.left,new Quaternion(), transform);
                    }
                    if (i == mapData.Length - 1 || mapData[i + 1][j] == '.')
                    {
                        Instantiate(wallPrefab, basePos + Vector3.right, new Quaternion(), transform);
                    }
                    if (j == 0 || mapData[i][j - 1] == '.')
                    {
                        if (i == 0) continue;
                        Instantiate(wallPrefab, basePos + Vector3.back, Quaternion.Euler(0, 90, 0), transform);
                    }
                    if (j == mapData[0].Length - 2 || mapData[i][j + 1] == '.')
                    {
                        if (i == mapData.Length - 1) continue;
                        Instantiate(wallPrefab, basePos + Vector3.forward, Quaternion.Euler(0, 90, 0), transform);
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

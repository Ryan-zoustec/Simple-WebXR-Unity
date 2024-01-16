/*************************************************
  * 名稱：MonoManager
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using UnityEngine;
using System.Collections;

/// <summary>讓純C#也能使用MonoBehaviour內的涵式，無需與Inspector交互</summary>
public class MonoManager : MonoBehaviour
{
    private static MonoManager instance;

    public static MonoManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("MonoManager");
                instance = go.AddComponent<MonoManager>();
            }
            return instance;
        }
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
/*************************************************
  * 名稱：Extenstions
  * 作者：RyanHsu
  * 功能說明：附加運算集
  * ***********************************************/
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtenstions
{
    public static void RemoveAllChild(this Transform transform)
    {
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                UnityEngine.Object.Destroy(child.gameObject);
            }
            transform.DetachChildren();
        }
    }

    /// <summary> 在transform下新增prefab </summary>
    /// <param name="prefab">prefab物件</param>
    /// <param name="name">GameObject Name</param>
    /// <returns>回傳GameObject</returns>
    public static GameObject AddChild(this Transform transform, GameObject prefab, string name = "")
    {
        GameObject instance = UnityEngine.Object.Instantiate(prefab, transform);
        if (name != "") instance.name = name;
        return instance;
    }
    /// <summary> 在transform下新增prefab </summary>
    /// <param name="prefab">prefab物件</param>
    /// <param name="name">GameObject Nam</param>
    /// <returns>回傳"<T>"</returns>
    public static T AddChild<T>(this Transform transform, GameObject prefab, string name = "") => AddChild(transform, prefab, name).GetComponent<T>();

}

public static class GameObjectExtenstions
{
    public static GameObject ReName(this GameObject gameObject, string name)
    {
        gameObject.name = name;
        return gameObject;
    }

    public static bool TryGetComponentInChildren<T>(this GameObject gameObject, out T component)
    {
        Transform tf = gameObject.transform;
        component = gameObject.GetComponentInChildren<T>();
        return component != null;
    }

    public static bool TryGetComponentsInChildren<T>(this GameObject gameObject, out T[] component)
    {
        Transform tf = gameObject.transform;
        component = gameObject.GetComponentsInChildren<T>();
        return component != null;
    }

}

public static class ArrayExtenstions
{
    /// <summary>ForEach</summary>
    public static void ForEach<T>(this T[] array, Action<T> action)
    {
        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(array[i]);
        }
    }

    /// <summary>帶編號的ForEach</summary>
    public static void ForEach<T>(this T[] array, Action<int, T> action)
    {
        int count = array.Length;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(i, array[i]);
        }
    }

}

public static class ListExtenstions
{
    /// <summary>將List泛類T輸出為字串</summary>
    public static string ToString<T>(this List<T> list)
    {
        string str = "[ ";
        int i = 0;
        foreach (T item in list)
        {
            str += str == "[ " ? "" : " , ";
            str += (i++) + ":";
            str += item.ToString();
        }
        str += " ]";
        return str;
    }

    /// <summary>帶編號的ForEach</summary>
    public static void ForEach<T>(this List<T> list, Action<int, T> action)
    {
        int count = list.Count;
        for (int i = 0; i < count; i++)
        {
            action.Invoke(i, list[i]);
        }
    }

}

public static class stringExtenstions
{
    /// <summary>將string去除空格與換行</summary>
    public static string format(this string str)
    {
        return str.ToString().Replace("\r", "").Replace("\n", "").Replace(" ", "");
    }

}

public static class floatExtenstions
{
    public static TweenerCore<float, float, FloatOptions> To(this float value, float endValue, float duration, Action<float> action)
    {
        return DOTween.To(() => value, x => value = x, endValue, duration).OnUpdate(() => action.Invoke(value)).OnComplete(() => action.Invoke(value));
    }

}


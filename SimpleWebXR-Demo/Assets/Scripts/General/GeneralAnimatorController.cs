/*************************************************
  * 名稱：GeneralAnimatorController
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>Animator數值設定類</summary>
public class GeneralAnimatorController
{
    Animator animator;//注入Animator
    Hashtable hashtable = new Hashtable();//記錄TweenerCore指針，將重覆的Tween停止

    public GeneralAnimatorController(Animator animator)
    {
        this.animator = animator;
    }

    /// <summary>通用移動控速</summary>
    public GeneralCallBack SetFloat(float to, string floatName, float time)
    {
        if (animator == null) Debug.LogError("需要在初始化階段注入animator");

        GeneralCallBack callBack = new GeneralCallBack();
        callBack.To(to);
        MonoManager.Instance.StartCoroutine(GeneralSetFloat(callBack, floatName, time));
        return callBack;
    }

    /// <summary> 強制停止TweenCore </summary>
    public GeneralAnimatorController Kill(string floatName, float? value = null)
    {
        if (value != null) animator.SetFloat(floatName, value ?? 0f);
        var tweenerCore = hashtable[floatName] as TweenerCore<float, float, FloatOptions>;
        if (tweenerCore != null && tweenerCore.IsPlaying()) { tweenerCore.Kill(); hashtable[floatName] = null; }
        return this;
    }

    IEnumerator GeneralSetFloat(GeneralCallBack callBack, string floatName, float time)
    {
        yield return new WaitForEndOfFrame();//緩一幀用以取得後置的callBack值

        if (!hashtable.ContainsKey(floatName)) hashtable.Add(floatName, null);
        var tweenerCore = hashtable[floatName] as TweenerCore<float, float, FloatOptions>;
        if (tweenerCore != null && tweenerCore.IsPlaying()) { tweenerCore.Kill(); hashtable[floatName] = null; }
        //
        float from = (callBack.from == null) ? animator.GetFloat(floatName) : callBack.from ?? 0f;
        float to = (callBack.to == null) ? 1f : callBack.to ?? 1f;
        if (callBack.io ?? false)
        {
            float now = from;
            from = to;
            to = now;
        }
        //
        var core = from.To(to, time, m => animator.SetFloat(floatName, m)
        ).OnStart(() =>
        {
            if (callBack.onStart != null) callBack.onStart.Invoke(callBack);
        }
        ).OnPlay(() =>
        {
            if (callBack.onProgress != null) callBack.onProgress.Invoke(from);
        }
        ).OnComplete(() =>
        {
            if (callBack.onComplete != null) callBack.onComplete.Invoke();
            callBack.Dispose();
        });

        hashtable[floatName] = core;
    }

}
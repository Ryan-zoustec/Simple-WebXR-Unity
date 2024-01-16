/*************************************************
  * 名稱：GeneralCallBack
  * 作者：RyanHsu
  * 功能說明：通用事件類別，可將常用事件通過此涵式統一處理
  * ***********************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class GeneralCallBack : IDisposable
{
    /// <summary>Action Object</summary>
    public Action<GeneralCallBack> onStart;
    /// <summary>Action Object</summary>
    public Action<float> onProgress;
    /// <summary>Action Object</summary>
    public Action onComplete;
    /// <summary>Action Object</summary>
    public Action<bool> onActive;
    //
    public GeneralCallBack IO(bool? io) { m_io = io; return this; }
    public bool? io { get => m_io; }
    bool? m_io = null;
    //
    public GeneralCallBack EnumInt(int? enumInt) { m_enumInt = enumInt; return this; }
    public int? enumInt { get => m_enumInt; }
    int? m_enumInt = null;
    //
    public GeneralCallBack From(float? from) { m_from = from; return this; }
    public float? from { get => m_from; }
    float? m_from = null;
    //
    public GeneralCallBack To(float? to) { m_to = to; return this; }
    public float? to { get => m_to; }
    float? m_to = null;
    //
    /// <summary>CallBack</summary>
    public GeneralCallBack OnStart(Action<GeneralCallBack> action)
    {
        onStart += action;
        return this;
    }
    /// <summary>CallBack</summary>
    public GeneralCallBack OnProgress(Action<float> action)
    {
        onProgress += action;
        return this;
    }
    /// <summary>CallBack</summary>
    public GeneralCallBack OnComplete(Action action)
    {
        onComplete += action;
        return this;
    }
    /// <summary>CallBack</summary>
    public GeneralCallBack OnActive(Action<bool> action)
    {
        onActive += action;
        return this;
    }
    /// <summary>CallBack</summary>
    public GeneralCallBack DelayCall(float delayTime, Action action)
    {
        Task.Run(async () =>
        {
            await Task.Delay(Mathf.FloorToInt(delayTime * 1000f));
            action.Invoke();
        });
        return this;
    }
    /// <summary>CloneCallBack</summary>
    /// <param name="callBack">克隆來源</param>
    /// /// <param name="overrideProperty">null:不克隆 false:只克隆非null屬性 true:克隆且複寫屬性 </param>
    /// <returns></returns>
    public GeneralCallBack CloneProperty(GeneralCallBack callBack, bool? overrideProperty = false)
    {
        if (overrideProperty != null)
        {
            if (overrideProperty == true)
            {
                m_io = callBack.m_io;
                m_enumInt = callBack.m_enumInt;
                m_from = callBack.m_from;
                m_to = callBack.m_to;
            } else
            {
                if (callBack.m_io != null) m_io = callBack.m_io;
                if (callBack.m_enumInt != null) m_enumInt = callBack.m_enumInt;
                if (callBack.m_from != null) m_from = callBack.m_from;
                if (callBack.m_to != null) m_to = callBack.m_to;
            }
        }
        return this;
    }

    /// <summary>CloneCallBack</summary>
    /// <param name="callBack">克隆來源</param>
    /// <param name="overrideEvent">null:不克隆 false:克隆並疊加事件 true:克隆且複寫事件</param>
    /// <returns></returns>
    public GeneralCallBack CloneEvent(GeneralCallBack callBack, bool? overrideEvent = null)
    {
        if (overrideEvent != null)
        {
            if (overrideEvent == true)
            {
                onStart = callBack.onStart;
                onComplete = callBack.onComplete;
                onProgress = callBack.onProgress;
                onActive = callBack.onActive;
            } else
            {
                if (callBack.onStart != null) onStart += callBack.onStart;
                if (callBack.onComplete != null) onComplete += callBack.onComplete;
                if (callBack.onProgress != null) onProgress += callBack.onProgress;
                if (callBack.onActive != null) onActive += callBack.onActive;
            }
        }
        return this;
    }

    public void Dispose()
    {
        onStart = null;
        onComplete = null;
        onProgress = null;
    }
}
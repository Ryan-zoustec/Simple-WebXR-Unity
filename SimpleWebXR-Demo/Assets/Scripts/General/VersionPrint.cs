/*************************************************
  * 名稱：VersionPrint
  * 作者：RyanHsu
  * 功能說明：
  * ***********************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
[RequireComponent(typeof(Text))]
public class VersionPrint : MonoBehaviour
{
    [DisplayOnly] public Text versionText;
    void Start() => versionText.text = "Version " + Application.version;

#if UNITY_EDITOR
    void OnValidate()
    {
        versionText = GetComponent<Text>();
    }
#endif

}
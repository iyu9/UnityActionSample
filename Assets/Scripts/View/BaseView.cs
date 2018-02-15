using UnityEngine;
using System.Collections;

/// <summary>
/// 基底ビュークラス
/// </summary>
public class BaseView : MonoBehaviour
{
  protected void Awake()
  {
    Debug.Log("BaseView.Awake");
  }

  protected void Start()
  {
    Debug.Log("BaseView.Start");
  }

  protected void Open()
  {
    Debug.Log("BaseView.Open");
  }

  protected void Close()
  {
    Debug.Log("BaseView.Close");
  }
}

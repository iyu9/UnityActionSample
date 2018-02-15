using UnityEngine;
using System.Collections;

/// <summary>
/// システム共通処理
/// </summary>
public class CommonDefine
{
  /// <summary>
  /// 汎用的な配列, リストチェック
  /// </summary>
  public static bool IsNullOrEmpty<T>(T[] array)
  {
    return (array == null || array.Length <= 0);
  }
  public static bool IsNullOrEmpty<T>(System.Collections.Generic.List<T> list)
  {
    return (list == null || list.Count <= 0);
  }

  /// <summary>
  /// 子のコンポーネント探索時のメソッドチェイン短縮
  /// </summary>
  public static T FindComponent<T>(Component root, string path) where T : Component
  {
    return root.transform.Find(path).GetComponent<T>();
  }
  public static T[] FindComponents<T>(Component root, string path) where T : Component
  {
    return root.transform.Find(path).GetComponentsInChildren<T>();
  }

  /// <summary>
  /// Multipleに分けられた画像の中から対象画像を返却
  /// </summary>
  /// <param name="fileName">対象ファイル名</param>
  /// <param name="spriteName">対象画像名</param>
  public static Sprite LoadMultiple(string fileName, string spriteName)
  {
    Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
    return System.Array.Find<Sprite>(sprites, (Sprite) => Sprite.name.Equals(spriteName));
  }

  /// <summary>
  /// オブジェクト移動に用いる3次補間処理(ease-in-out)
  /// </summary>
  public static float EaseInOut(float t)
  {
    return (3f * t * t) - (2f * t * t * t);
  }
}

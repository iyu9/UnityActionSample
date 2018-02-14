using UnityEngine;
using System.Collections;

/// <summary>
/// メイン画面の制御を行います。
/// </summary>
public class MainView : MonoBehaviour
{
  private const float MoveWeight = 1f;
  private const int BulletOutBoundary = 30;
  private const int BulletNumLimit = 10;
  private const float ShotInterval = 0.1f;

  [SerializeField]
  private Player player;

  private System.Collections.Generic.List<GameObject> bulletObserver = null;

  private float lastShotTimer = 0f;

  void Start()
  {
    player = GameObject.Find("Player").GetComponent<Player>();
    bulletObserver = new System.Collections.Generic.List<GameObject>();
  }

  void Update()
  {
    CheckInput();
    UpdateBullet();
  }

  /// <summary>
  /// 入力のチェックを行います。
  /// </summary>
  private void CheckInput()
  {
    if (Input.GetKey(KeyCode.LeftArrow))
    {
      player.transform.Translate(MoveWeight * Vector3.left);
    }
    if (Input.GetKey(KeyCode.RightArrow))
    {
      player.transform.Translate(MoveWeight * Vector3.right);
    }

    if (Input.GetKeyDown(KeyCode.Z))
    {
      Jump();
    }
    if (Input.GetKey(KeyCode.X))
    {
      Shot();
    }
    if (Input.GetKeyDown(KeyCode.C))
    {
      Cancel();
    }
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      CallMenu();
    }
  }

  /// <summary>
  /// 弾丸の更新を行う。
  /// </summary>
  private void UpdateBullet()
  {
    if (bulletObserver == null || bulletObserver.Count <= 0)
    {
      return;
    }

    //ショット間隔制御
    lastShotTimer += Time.deltaTime;

    for (int i = 0; i < bulletObserver.Count; i++)
    {
      GameObject bullet = bulletObserver[i];

      //直進
      Vector3 pos = bullet.transform.localPosition;
      bullet.transform.localPosition =
        new Vector3(pos.x + 0.5f, pos.y, pos.z);

      //シーン範囲外判定
      if (Mathf.Abs(bullet.transform.localPosition.x) > BulletOutBoundary)
      {
        Destroy(bullet, 1f);
        bulletObserver.RemoveAt(i);
      }
    }
  }

  /// <summary>
  /// ジャンプ操作を行う
  /// </summary>
  private void Jump()
  {
    StartCoroutine(AsyncJumping());
  }

  private IEnumerator AsyncJumping()
  {
    const float G = 2f;

    float t = 0f;
    float vy = 0.5f * G;

    while (player.transform.localPosition.y >= -5f)
    {
      t += Time.deltaTime;
      Vector3 pos = player.transform.localPosition;
      pos.y += vy - (G * t);
      player.transform.localPosition = pos;
      yield return null;
    }

    Vector3 endPos = player.transform.localPosition;
    endPos.y = -5f;
    player.transform.localPosition = endPos;
  }

  /// <summary>
  /// ショットオブジェクトの生成を行う
  /// </summary>
  private void Shot()
  {
    if (bulletObserver.Count >= BulletNumLimit)
    {
      return;
    }
    if (bulletObserver.Count > 0 && lastShotTimer < ShotInterval)
    {
      return;
    }

    var bulletPrefab = Resources.Load<GameObject>("Prefabs/Bullet");

    Vector3 pos = player.transform.localPosition;
    pos.x += 1f;
    bulletPrefab.transform.localPosition = pos;

    Quaternion angle = Quaternion.Euler(Vector3.zero);
    GameObject bullet = Instantiate(bulletPrefab, pos, angle) as GameObject;

    bulletObserver.Add(bullet);
  }

  /// <summary>
  /// キャンセル操作を行う
  /// </summary>
  private void Cancel()
  {
    
  }

  /// <summary>
  /// メニュー呼び出し操作を行う
  /// </summary>
  private void CallMenu()
  {

  }
}
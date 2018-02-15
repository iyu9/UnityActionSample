using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
  public enum ActionState
  {
    OnGround,
    OnJumping,
  }

  private ActionState actState;
  private BoxCollider2D collider;
  private Image imgPlayer;

  void Start()
  {
    Initialize();
  }

  void Update()
  {
    //switch (actState)
    //{
    //  case ActionState.Float:
    //    this.transform.Rotate(10f * Vector3.forward);
    //    break;
    //}
  }

  private void GetRefs()
  {
    imgPlayer = GetComponent<Image>();
    collider = GetComponent<BoxCollider2D>();
  }

  private void Initialize()
  {
    actState = ActionState.OnGround;
    GetRefs();
  }

  public void Jump()
  {
    StartCoroutine(AsyncJumping());
  }

  private System.Collections.IEnumerator AsyncJumping()
  {
    const float G = 2f;

    float t = 0f;
    float vy = 0.5f * G;

    while (this.transform.localPosition.y >= -5f)
    {
      t += Time.deltaTime;
      Vector3 pos = this.transform.localPosition;
      pos.y += vy - (G * t);
      this.transform.localPosition = pos;
      yield return null;
    }

    Vector3 endPos = this.transform.localPosition;
    endPos.y = -5f;
    this.transform.localPosition = endPos;
  }
}

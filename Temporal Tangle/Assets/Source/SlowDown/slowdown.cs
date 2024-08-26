using UnityEngine;

public class slowdown : MonoBehaviour
{
    public float slowDownFactor = 0.2f;
    public float slowDownDuration = 20f;

    private float originalTimeScale;
    private float originalFixedDeltaTime;
    private float slowDownEndTime;
    private bool isSlowingDown = false;

    void Start()
    {
      originalTimeScale = Time.timeScale;
      originalFixedDeltaTime = Time.fixedDeltaTime;
      Application.targetFrameRate = 60;
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.F)) {
        if (isSlowingDown) {
          EndSlowDown();
        } else {
          StartSlowDown();
        }
      }

      if (isSlowingDown) {
        if (Time.unscaledTime >= slowDownEndTime) {
          EndSlowDown();
        }
      }
    }

    public void StartSlowDown()
    {
      Time.timeScale = slowDownFactor;
      Time.fixedDeltaTime = originalFixedDeltaTime * slowDownFactor / 2f;
      slowDownEndTime = Time.unscaledTime + slowDownDuration;
      isSlowingDown = true;
    }

    public void EndSlowDown() {
      Time.timeScale = originalTimeScale;
      Time.fixedDeltaTime = originalFixedDeltaTime;
      isSlowingDown = false;
    }
}

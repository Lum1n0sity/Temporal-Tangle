using System.Collections.Generic;
using UnityEngine;

public class RewindObject : MonoBehaviour
{
  private List<Snapshot> snapshots = new List<Snapshot>();

  void Update() {
    // record a snapshot of player attributes every frame
    if (!RewindManager.isRewinding) {
      RecordSnapshot();
    }
  }

  public void Rewind() {
    if (snapshots.Count > 1) {
      Snapshot currentSnapshot = snapshots[0];
      Snapshot nextSnapshot = snapshots[1];

      Debug.Log(currentSnapshot.ToString());
      Debug.Log(nextSnapshot.ToString());

      float timeDifference = currentSnapshot.time - nextSnapshot.time;
      float lerpFactor = (Time.time - nextSnapshot.time) / timeDifference;

      transform.position = Vector2.Lerp(nextSnapshot.position, currentSnapshot.position, lerpFactor);
      
      float newRotation = Mathf.Lerp(nextSnapshot.rotation, currentSnapshot.rotation, lerpFactor);
      transform.rotation = Quaternion.Euler(0, 0, newRotation);

      if (lerpFactor >= 1f) {
        snapshots.RemoveAt(0);
      }
    } else {
      RewindManager.AddString(gameObject.name);
    }
  }

  private void RecordSnapshot() {
    Vector2 currentPosition = transform.position;
    float currentRotation = transform.eulerAngles.z;
    float currentTime = Time.time;

    snapshots.Insert(0, new Snapshot(currentPosition, currentRotation, currentTime));

    if (snapshots.Count > Mathf.CeilToInt(20f / Time.fixedDeltaTime)) {
      snapshots.RemoveAt(snapshots.Count - 1);
    }
  }

  private class Snapshot {
    public Vector2 position;
    public float rotation;
    public float time;

    public Snapshot(Vector2 pos, float rot, float t) {
      position = pos;
      rotation = rot;
      time = t;
    }

    // Debug/Test
    public override string ToString() {
      return $"Snapshot(Position: {position}, Rotation: {rotation}, Time: {time})";
    }
  }
}

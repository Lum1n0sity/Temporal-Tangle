using System.Collections.Generic;
using UnityEngine;

public class RewindManager : MonoBehaviour
{
  public static bool isRewinding = false;
  private List<RewindObject> rewindableObjects = new List<RewindObject>();
  private static List<string> finishedRewindableObjects = new List<string>();

  void Start() {
    rewindableObjects.AddRange(Object.FindObjectsByType<RewindObject>(FindObjectsSortMode.None));
  }

  void Update() {
    if (rewindableObjects.Count == finishedRewindableObjects.Count) {
      Debug.Log("All objects finished");
      isRewinding = false;
      ToggleRewind(false);
      ClearList();
    } else {
      if (isRewinding) {
        foreach (var obj in rewindableObjects) {
          obj.Rewind();
        }
      }

      if (Input.GetKeyDown(KeyCode.R)) {
        ToggleRewind(true);
      } else if (Input.GetKeyUp(KeyCode.R)) {
        ToggleRewind(false);
      }
    }
  }

  public void ToggleRewind(bool rewindActive) {
    isRewinding = rewindActive;

    foreach (var obj in rewindableObjects) {
      if (rewindActive) {
        obj.Rewind();
      }
    }
  }

  public static void AddString(string value) {
    if (!finishedRewindableObjects.Contains(value)) {
      finishedRewindableObjects.Add(value);
    }
  }

  public static void RemoveString(string value) {
    finishedRewindableObjects.Remove(value);
  }

  public static void ClearList() {
    finishedRewindableObjects.Clear();
  }
}

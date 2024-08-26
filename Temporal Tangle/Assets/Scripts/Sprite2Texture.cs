using UnityEngine;
using UnityEditor;
using System.IO;

public class sprite2texture : EditorWindow
{
  private Sprite sprite;
  private Vector2 tiling = new Vector2(2, 2);

  [MenuItem("Tools/Sprite To Tiled Texture")]
  public static void ShowWindow() {
    GetWindow<sprite2texture>("Sprite to Tiled Texture");
  }
  
  private void OnGUI() {
    GUILayout.Label("Generate Tiled Texture", EditorStyles.boldLabel);

    sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", sprite, typeof(Sprite), false);
    tiling = EditorGUILayout.Vector2Field("Tiling", tiling);

    if (GUILayout.Button("Generate Tiled Texture")) {
      GenerateTiledTexture();
    }
  }

  private void GenerateTiledTexture() {
    if (sprite == null) {
      Debug.LogError("Sprite is not assigned!");
      return;
    }

    Texture2D originalTexture = sprite.texture;

    int newWidth = Mathf.FloorToInt(originalTexture.width * tiling.x);
    int newHeight = Mathf.FloorToInt(originalTexture.height * tiling.y);

    Texture2D tiledTexture = new Texture2D(newWidth, newHeight);

    for (int i = 0; i < newWidth; i++) {
      for (int j = 0; j < newHeight; j++) {
        Color pixelColor = originalTexture.GetPixel(i % originalTexture.width, j % originalTexture.height);
        tiledTexture.SetPixel(i, j, pixelColor);
      }
    }

    tiledTexture.Apply();

    SaveTiledTexture(tiledTexture, sprite.name);
  }

  private void SaveTiledTexture(Texture2D texture, string spriteName) {
    string directoryPath = Application.dataPath + "/Assets/Textures";
    string filePath = directoryPath + "/" + spriteName + "_Tiled.png";
    byte[] bytes = texture.EncodeToPNG();
    File.WriteAllBytes(filePath, bytes);

    Debug.Log("Tiled texture saved at: " + filePath);
    AssetDatabase.Refresh();
  }
}

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Services
{
    public static class MenuTool
    {
        [MenuItem("Tools/Scene/Scene0 _`")]
        //用于便捷地返回0场景
        public static void OpenScene0()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/0.unity");
            SceneAsset asset = AssetDatabase.LoadAssetAtPath<SceneAsset>("Assets/Scenes/0.unity");
            ProjectWindowUtil.ShowCreatedAsset(asset);
        }
    }
}
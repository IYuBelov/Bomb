using GameLogic;
using UnityEditor;


public class GameConstantsWindow : EditorWindow
{
    [MenuItem("Window/Game Constants")]
    public static void ShowWindow()
    {
        GetWindow<GameConstantsWindow>("Game Constants");
    }
    
    private void OnGUI()
    {
        EditorGUILayout.LabelField("Настройки Игры", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("CountdownTime:", Constants.CountdownTime.ToString());
        EditorGUILayout.LabelField("MinBombAliveTime:", Constants.MinBombAliveTime.ToString());
        EditorGUILayout.LabelField("MaxBombAliveTime:", Constants.MaxBombAliveTime.ToString());
        EditorGUILayout.LabelField("BonusBombAliveTime:", Constants.BonusBombAliveTime.ToString());
        EditorGUILayout.LabelField("AlertBombTime:", Constants.AlertBombTime.ToString());
        EditorGUILayout.LabelField("ExplosionCountdownTime:", Constants.ExplosionCountdownTime.ToString());

        // Вы можете добавить и другие элементы UI редактора по мере необходимости
    }
}
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MyData", menuName = "Custom/GameSettings", order = 1)]
    public class GameSettings : ScriptableObject
    {
        public List<string> devPlayerNames = new() { "Игорь", "Герман" };
    }
}
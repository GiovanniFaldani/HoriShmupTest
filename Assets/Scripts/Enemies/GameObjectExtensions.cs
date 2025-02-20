using UnityEngine;
using System.Collections.Generic;

namespace Utils{
    // Adds a generic component if not present, or returns it if present
    public static class GameObjectExtensions
    {
        public static T GetOrAdd<T>(GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            return component != null ? component : gameObject.AddComponent<T>();
        }
    }
}

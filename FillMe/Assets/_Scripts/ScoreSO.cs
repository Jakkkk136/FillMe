using UnityEngine;

namespace DefaultNamespace
{
    /// <summary>
    /// Used Scriptable Object to save score between scene loadings
    /// </summary>
    [CreateAssetMenu(fileName = "ScoreSO", menuName = "ScoreSO", order = 0)]
    public class ScoreSO : ScriptableObject
    {
        public int score = 0;
        
        public void AddScore(int i)
        {
            score += i;
        }

        public void ResetScore()
        {
            score = 0;
        }
    }
}
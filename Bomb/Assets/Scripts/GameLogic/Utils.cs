using System;
using System.Collections.Generic;


namespace contribute
{
    public class Singleton<T> where T : class, new()
    {
        private Singleton()
        {
            
        }

        private static readonly Lazy<T> instance = new Lazy<T>(() => new T());

        public static T Instance
        {
            get { return instance.Value; }
        }
    }
}


public static class Utils
{
    public static WordCondition GetWordConditionRandom()
    {
        return Utils.CONDITIONS[rand.Next(Utils.CONDITIONS.Length)];
    }

    private static Random rand = new Random();
    private static readonly WordCondition[] CONDITIONS =
    {
        WordCondition.BEGINING, WordCondition.ANYWHERE, WordCondition.END
    };
}

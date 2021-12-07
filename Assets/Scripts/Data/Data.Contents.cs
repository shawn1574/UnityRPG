using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]//json 파일 읽어오기위해서 필요(json parsing)
    public class Stat
    {

        public int level; //json에 있는 변수이름과 일치 시켜줘야함
        public int maxHp;
        public int attack;
        public int totalExp;


    }
    [Serializable]//json 파일 읽어오기위해서 필요(json parsing)
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            //data.stats.ToDictionary() ios 쪽에 문제가 있었다고함 그래서 foreach문으로 하나하나
            //넣어줌
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
    #endregion
}

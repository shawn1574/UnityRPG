using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    #region Stat
    [Serializable]//json ���� �о�������ؼ� �ʿ�(json parsing)
    public class Stat
    {

        public int level; //json�� �ִ� �����̸��� ��ġ ���������
        public int maxHp;
        public int attack;
        public int totalExp;


    }
    [Serializable]//json ���� �о�������ؼ� �ʿ�(json parsing)
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            //data.stats.ToDictionary() ios �ʿ� ������ �־��ٰ��� �׷��� foreach������ �ϳ��ϳ�
            //�־���
            foreach (Stat stat in stats)
                dict.Add(stat.level, stat);

            return dict;
        }
    }
    #endregion
}

using System.Collections.Generic;
using System.Globalization;
using LevelCreation;
using UnityEngine;

namespace UI.Screens.LevelSelection
{
    public class LevelScreenModel
    {
        private readonly List<LevelData> _levelDatas;
        public List<LevelDisplayData> LevelDisplayDatas { get; }

        public LevelScreenModel(List<LevelData> levelDatas)
        {
            _levelDatas = levelDatas;
            LevelDisplayDatas = GetLevelData();
        }

        private List<LevelDisplayData> GetLevelData()
        {
            var datas = new List<LevelDisplayData>();
            for (int i = 0; i < _levelDatas.Count; i++)
            {
                var data = new LevelDisplayData
                {
                    levelCount = i + 1,
                    score = (i + 1) * Random.Range(0, 10),
                    title = _levelDatas[i].title
                };
                datas.Add(data);
            }

            return datas;
        }

    }
}

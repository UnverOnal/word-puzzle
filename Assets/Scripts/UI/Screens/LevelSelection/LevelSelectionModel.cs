using System.Collections.Generic;
using UnityEngine;

namespace UI.Screens.LevelSelection
{
    public class LevelSelectionModel
    {
        public List<TempData> LevelDatas { get; }

        public LevelSelectionModel()
        {
            LevelDatas = GetLevelData();
        }

        private List<TempData> GetLevelData()
        {
            var datas = new List<TempData>();
            for (int i = 0; i < 10; i++)
            {
                var data = new TempData
                {
                    levelCount = i + 1,
                    score = (i + 1) * Random.Range(0, 10),
                    title = "Title" + (i + 1)
                };
                datas.Add(data);
            }

            return datas;
        }

    }
}

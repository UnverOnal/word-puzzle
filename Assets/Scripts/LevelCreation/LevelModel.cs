using System.Collections.Generic;
using GamePlay.TileSystem;
using Services.FileConversionService;

namespace LevelCreation
{
    public class LevelModel
    {
        public List<LevelData> LevelDatas { get; private set; }
        public LevelData CurrentLevel => LevelDatas[_levelCount];
        
        public List<Tile> Tiles { get; }
        public List<EmptyTile> FormingTiles { get; set; }

        private readonly IConverter _jsonConverter;
        private readonly LevelAssets _levelAssets;
        

        private int _levelCount;

        public LevelModel(IFileConversionService fileConversionService, LevelAssets levelAssets)
        {
            _jsonConverter = fileConversionService.GetConverter(ConverterType.JsonConverter);
            _levelAssets = levelAssets;

            Tiles = new List<Tile>();
        }

        public void CreateLevelData()
        {
            LevelDatas = new List<LevelData>();
            var levelDataFiles = _levelAssets.levelDataFiles;
            for (int i = 0; i < levelDataFiles.Length; i++)
            {
                var file = levelDataFiles[i];
                var convertedData = _jsonConverter.GetData<LevelData>(file);
                LevelDatas.Add(convertedData);
            }
        }

        public void UpdateLevelCount()
        {
            _levelCount++;
        }
    }
}

using System.Collections.Generic;
using GamePlay.TileSystem;
using Services.FileConversionService;

namespace LevelCreation
{
    public class LevelModel
    {
        public List<LevelCreationData> LevelCreationDatas { get; private set; }
        
        public List<LetterTile> LetterTiles { get; }
        public List<BlankTile> FormingTiles { get; set; }

        private readonly IConverter _jsonConverter;
        private readonly LevelAssets _levelAssets;
        
        public LevelModel(IFileConversionService fileConversionService, LevelAssets levelAssets)
        {
            _jsonConverter = fileConversionService.GetConverter(ConverterType.JsonConverter);
            _levelAssets = levelAssets;

            LetterTiles = new List<LetterTile>();
        }

        public void CreateLevelCreationData()
        {
            LevelCreationDatas = new List<LevelCreationData>();
            var levelDataFiles = _levelAssets.levelDataFiles;
            for (int i = 0; i < levelDataFiles.Length; i++)
            {
                var file = levelDataFiles[i];
                var convertedData = _jsonConverter.GetData<LevelCreationData>(file);
                LevelCreationDatas.Add(convertedData);
            }
        }

        public LevelCreationData GetLevel(int index)
        {
            return LevelCreationDatas[index];
        }
    }
}

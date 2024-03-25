using System;
using LevelCreation;
using Services.DataStorageService;
using VContainer;

namespace GamePlay.Score
{
    public class ScorePresenter
    {
        public event Action<int> OnScoreUpdated;
        public int Score => _scoreModel.Score;
        public int HighScore => _scoreModel.HighScore;

        [Inject] private readonly ScoreData _scoreData;
        
        private readonly ScoreModel _scoreModel;

        [Inject]
        public ScorePresenter(LevelPresenter levelPresenter, IDataStorageService dataStorageService)
        {
            _scoreModel = new ScoreModel(dataStorageService ,levelPresenter);
        }

        public void CalculateScore(string word)
        {
            var wordPoint = 0;
            var letters = word.ToCharArray();
            for (var j = 0; j < letters.Length; j++)
            {
                var letter = letters[j];
                var point = GetLetterPoint(letter);
                wordPoint += _scoreData.fixedFactor * word.Length * point;
            }

            _scoreModel.AddPoint(wordPoint);
            OnScoreUpdated?.Invoke(_scoreModel.Score);
        }

        public void CalculateTotalScore(int remainingLetterCount)
        {
            var totalPoints = _scoreModel.Score;
            totalPoints -= remainingLetterCount * _scoreData.punishmentPoint;
            _scoreModel.SetScore(totalPoints);
        }

        public void Reset()
        {
            OnScoreUpdated?.Invoke(_scoreModel.Score);
            _scoreModel.Reset();
        }

        private int GetLetterPoint(char letter)
        {
            var letterDatas = _scoreData.letterDatas;
            for (var i = 0; i < letterDatas.Length; i++)
            {
                var letterData = letterDatas[i];
                if (letterData.letter.Equals(char.ToLower(letter)))
                    return letterData.point;
            }

            return 0;
        }
    }
}
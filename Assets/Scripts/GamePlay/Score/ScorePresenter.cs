using System.Collections.Generic;

namespace GamePlay.Score
{
    public class ScorePresenter
    {
        public int Score => _scoreModel.Score;
        
        private readonly ScoreData _scoreData;
        private readonly ScoreModel _scoreModel;

        public ScorePresenter(ScoreData scoreData)
        {
            _scoreData = scoreData;
            _scoreModel = new ScoreModel();
        }

        public void CalculateScores(List<string> words, int remainingLetterCount)
        {
            var totalPoint = 0;
            for (int i = 0; i < words.Count; i++)
            {
                var word = words[i];
                var letters = word.ToCharArray();
                for (int j = 0; j < letters.Length; j++)
                {
                    var letter = letters[j];
                    var point = GetLetterPoint(letter);
                    var wordPoint = _scoreData.fixedFactor * word.Length * point;
                    totalPoint += wordPoint;
                }
            }

            totalPoint -= remainingLetterCount * _scoreData.punishmentPoint;
            _scoreModel.UpdateScore(totalPoint);
        }
        
        private int GetLetterPoint(char letter)
        {
            var letterDatas = _scoreData.letterDatas;
            for (int i = 0; i < letterDatas.Length; i++)
            {
                var letterData = letterDatas[i];
                if (letterData.letter.Equals(char.ToLower(letter)))
                    return letterData.point;
            }

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2
{
    public class Settings
    {
        public bool IsHintVisible; 
        public enum DifficultyLevel { Easy, Normal, Hard }
        private DifficultyLevel difficultyLevel;
        public delegate void PropertySettingsHandler(DifficultyLevel newDifficulty);
        public event PropertySettingsHandler DifficultyChanged;
        public DifficultyLevel Difficulty
        {
            get
            {
                return difficultyLevel;
            }
            set
            {
                difficultyLevel = value;
                if (Difficulty == DifficultyLevel.Easy) { if (DifficultyChanged != null) { DifficultyChanged(Difficulty); } }
                if (Difficulty == DifficultyLevel.Normal) { if (DifficultyChanged != null) { DifficultyChanged(Difficulty); } }
                if (Difficulty == DifficultyLevel.Hard) { if (DifficultyChanged != null) { DifficultyChanged(Difficulty); } }
            }
        }
        public Settings(bool isHintVisible, DifficultyLevel difficulty)
        {
            IsHintVisible = isHintVisible;
            Difficulty = difficulty;
        }

    }
}

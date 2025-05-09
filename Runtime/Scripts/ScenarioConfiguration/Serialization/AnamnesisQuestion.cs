using System;
using System.Collections.Generic;

namespace medicaltraining.assetstore.ScenarioConfiguration.Serialization
{
    /// <summary>
    /// Serializable data format for anamnesis questions. 
    /// </summary>
    [Serializable]
    public class AnamnesisQuestion
    {
        public string QuestionID;
        public string AnswerText;
        public List<FollowUpQuestion> FollowUpQuestions;
        [Serializable]
        public class FollowUpQuestion
        {
            public string QuestionText;
            public string AnswerText;
        }
    }
}
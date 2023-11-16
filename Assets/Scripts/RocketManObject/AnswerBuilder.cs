using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Utility;

namespace RocketManObject
{
    public class AnswerBuilder : MonoBehaviour
    {
        [SerializeField] private string name;
        [SerializeField] private List<string> answers;
        [SerializeField] private Sprite avatar;
        
        [SerializeField] private AnimationManager animationManager;

        public void SetName(string name)
        {
            this.name = name;
        }

        public void AddAnswer(string answer)
        {
            answers.Add(answer);
        }
        
        public void SetAvatar(Sprite avatar)
        {
            this.avatar = avatar;
        }

        public void BuildAnswer()
        {
            Utility.RocketManObject rocketManObject = new Utility.RocketManObject(avatar, name, answers);
            animationManager.GetCreditListRocketman().Add(name);
            animationManager.addRocketManObject(rocketManObject);
            animationManager.SaveLists();
            
            answers = new List<string>();
            name = "";
        }
        
        public void BuildAnswerQueen()
        {
            
            animationManager.GetCreditListDontStopMeNow().Add(name);
           
        }
    }
}
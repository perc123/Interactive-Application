using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class RocketManObject
    { 
        [SerializeField] private Sprite _avatar;
        [SerializeField] private string _name;
        [SerializeField] private List<string> _answers;
    
        public RocketManObject(Sprite avatar, string name, List<string> answers)
        {
            _avatar = avatar;
            _name = name;
            _answers = answers;
        }
        
        public Sprite Avatar
        {
            get => _avatar;
            set => _avatar = value;
        }
        
        public string Name
        {
            get => _name;
            set => _name = value;
        }
        
        public List<string> Answers
        {
            get => _answers;
            set => _answers = value;
        }
    }
}

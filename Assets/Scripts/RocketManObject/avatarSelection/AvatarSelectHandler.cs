using UnityEngine;
using UnityEngine.UI;

namespace RocketManObject.avatarSelection
{
    public class AvatarSelectHandler : MonoBehaviour
    {
        
        [SerializeField] private AnswerBuilder answerBuilder;
        [SerializeField] private GameObject avatarObject;

        public void SetAvatar()
        {
            Sprite avatar = avatarObject.GetComponent<Image>().sprite;
            answerBuilder.SetAvatar(avatar);
        }
    }
}

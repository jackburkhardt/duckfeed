using UnityEngine;

namespace DialogueSystem
{
    public class PlayerResponse
    {
        private int _nodeID;
        private string _dialogueLine;
        private DialogueNode _next;
        private Skill _skillRequirement;
        private Perk _perkRequirement;
        private AnimationClip _animationClip;
        private AudioClip _voice;

        public PlayerResponse(int nodeID, string dialogueLine, DialogueNode next, AnimationClip animationClip = null, AudioClip voice = null)
        {
            _nodeID = nodeID;
            _dialogueLine = dialogueLine;
            _next = next;
            _animationClip = animationClip;
            _voice = voice;
        }

        public PlayerResponse(int nodeID, string dialogueLine, DialogueNode next, Skill skillRequirement, AnimationClip animationClip = null, AudioClip voice = null)
        {
            _nodeID = nodeID;
            _dialogueLine = dialogueLine;
            _next = next;
            _skillRequirement = skillRequirement;
            _animationClip = animationClip;
            _voice = voice;
        }

        public PlayerResponse(int nodeID, string dialogueLine, DialogueNode next, Perk perkRequirement, AnimationClip animationClip = null, AudioClip voice = null)
        {
            _nodeID = nodeID;
            _dialogueLine = dialogueLine;
            _next = next;
            _perkRequirement = perkRequirement;
            _animationClip = animationClip;
            _voice = voice;
        }
    }
}
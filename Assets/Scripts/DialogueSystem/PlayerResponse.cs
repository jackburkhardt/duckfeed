using UnityEngine;

namespace DialogueSystem
{
    public class PlayerResponse : IDialogueComponent
    {
        private int _ID;
        private string _dialogueLine;
        private DialogueNode _next;
        private Skill _skillRequirement;
        private Perk _perkRequirement;
        private AnimationClip _animationClip;
        private AudioClip _voice;

        public PlayerResponse(int ID, string dialogueLine, DialogueNode next, AnimationClip animationClip = null, AudioClip voice = null)
        {
            _ID = ID;
            _dialogueLine = dialogueLine;
            _next = next;
            _animationClip = animationClip;
            _voice = voice;
        }

        public PlayerResponse(int ID, string dialogueLine, DialogueNode next, Skill skillRequirement, AnimationClip animationClip = null, AudioClip voice = null)
        {
            _ID = ID;
            _dialogueLine = dialogueLine;
            _next = next;
            _skillRequirement = skillRequirement;
            _animationClip = animationClip;
            _voice = voice;
        }

        public PlayerResponse(int ID, string dialogueLine, DialogueNode next, Perk perkRequirement, AnimationClip animationClip = null, AudioClip voice = null)
        {
            _ID = ID;
            _dialogueLine = dialogueLine;
            _next = next;
            _perkRequirement = perkRequirement;
            _animationClip = animationClip;
            _voice = voice;
        }

        public int ID()
        {
            return _ID;
        }
        public string DialogueText()
        {
            return _dialogueLine;
        }

        public DialogueNode Next
        {
            get => _next;
            set => _next = value;
        }

        public Skill SkillRequirement
        {
            get => _skillRequirement;
            set => _skillRequirement = value;
        }

        public Perk PerkRequirement
        {
            get => _perkRequirement;
            set => _perkRequirement = value;
        }

        public AnimationClip AnimationClip
        {
            get => _animationClip;
            set => _animationClip = value;
        }

        public AudioClip Voice
        {
            get => _voice;
            set => _voice = value;
        }
    }
}
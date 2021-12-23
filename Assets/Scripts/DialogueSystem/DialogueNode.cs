using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueNode : IDialogueComponent
    {
        // sample text file formatting (.npc):
        // _ID:_playerText:_npcResponse:_next(id)(:levelrequirement-level(:skillrequirement-unlocked))
        private int _ID;
        private string _npcText;
        private List<PlayerResponse> _playerResponses;
        private AnimationClip _animationClip;
        private AudioClip _voice;

        public DialogueNode(int id, string npcText, List<PlayerResponse> playerResponses, AnimationClip animationClip = null, AudioClip voice = null)
        {
            _ID = id;
            _npcText = npcText;
            _playerResponses = playerResponses;
            _animationClip = animationClip;
            _voice = voice;
        }
        
        public int ID()
        {
            return _ID;
        }
        public string DialogueText()
        {
            return _npcText;
        }

        public List<PlayerResponse> PlayerResponses
        {
            get => _playerResponses;
            set => _playerResponses = value;
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
using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueNode
    {
        // sample text file formatting (.npc):
        // _ID:_playerText:_npcResponse:_next(id)(:levelrequirement-level(:skillrequirement-unlocked))
        private int _ID;
        private string _playerText;
        private string _npcResponse;
        private List<PlayerResponse> _playerResponses;
        private AnimationClip _animationClip;
        private AudioClip _voice;
        


    }
}
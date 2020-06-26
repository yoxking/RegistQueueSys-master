using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntFrm.MainService.Entities
{
    public class SpeechData
    {
        public string CounterNo { set; get; }
        public string VoiceName { set; get; }
        public int VoiceRate{ set; get; }
        public int VoiceVolume { set; get; }
        public string VoiceText { set; get; }

        public string PreMusic { set; get; }
        public string PostMusic { set; get; }
    }
}

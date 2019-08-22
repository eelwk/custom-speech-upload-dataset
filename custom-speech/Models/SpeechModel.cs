using System;
using System.Collections.Generic;
using System.Text;

namespace custom_speech.Models
{
    public class SpeechModel
    {
        public string name { get; set; }
        public string description { get; set; }
        public string locale { get; set; }
        public string dataImportKind { get; set; }
        public IDictionary<string, string> properties { get; set; }
    }
    
    public class LanguageModel : SpeechModel
    {
        public byte[] languagedata { get; set; }
    }

    public class AcousticModel : SpeechModel
    {
        public byte[] transcriptions { get; set; }
        public byte[] audiodata { get; set; }
    }
}

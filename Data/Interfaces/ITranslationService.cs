using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Data.Interfaces
{
    public interface ITranslationService
    {
        String Translate(string textToTranslate, Language targetLang, Language sourceLang);
        String DetectLanguage(string textToDetect);
    }
}

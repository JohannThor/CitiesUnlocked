using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Interfaces;

namespace Data.Implementations.Mock
{
    public class TranslateService : ITranslationService
    {
        public string DetectLanguage(string textToDetect)
        {
            throw new NotImplementedException();
        }

        public string Translate(string textToTranslate, Common.Language sourceLang, Common.Language targetLang)
        {
            if (sourceLang == Common.Language.BritishEnglish)
            {
                if (targetLang == Common.Language.Spanish)
                {
                    switch (textToTranslate)
                    {
                        case "Hello":
                            return "Hola";
                        case "Good day":
                            return "Buenos Dias";
                        default:
                            return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
}

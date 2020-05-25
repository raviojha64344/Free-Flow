using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FreeFlow.Parsers
{
    /**/
    public class JsonParser : Parser
    {
        public override string To(object obj)
        {
            if(obj != null)
                return JsonUtility.ToJson(obj);
            return string.Empty;
        }

        public override T From<T>(string str)
        {
            if (!string.IsNullOrEmpty(str))
                return JsonUtility.FromJson<T>(str);
            return default;
        }
    }
}

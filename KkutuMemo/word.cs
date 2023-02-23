using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KkutuMemo
{
    public class Word
    {
        public string word;
        public List<string> tags = new List<string>();
        public int priority = 0;

        // 해당 태그를 지녔는지
        public bool hasTag(string target)
        {
            return tags.Contains(target);
        }

        public bool hasTags(string[] targets)
        {
            bool result = true;
            foreach (string target in targets)
            {
                if (!hasTag(target))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public bool advancedFilter(string filter)
        {
            string[] split = filter.Split('~'); // 가~마 -> "가"나다라'마'바"사" x, 가~사 o

            if (split.Length == 2)
            {
                string start = split[0];
                string end = split[1];

                if (word.StartsWith(start) && word.EndsWith(end))
                {
                    return true;
                }
            }
            else if (filter.Contains("<"))
            {
                int condition;
                bool available = int.TryParse(filter.Replace("<", ""), out condition);
                if (available && word.Length <= condition)
                {
                    return true;
                }
            }
            else if (filter.Contains(">"))
            {
                int condition;
                bool available = int.TryParse(filter.Replace(">", ""), out condition);
                if (available && word.Length >= condition)
                {
                    return true;
                }
            } else if (word.Contains(filter)) // 그냥 단어 포함 여부
            {
                return true;
            }
            return false;
        }

        // 필터를 거쳐 나올 수 있는 단어인지 줌, 0=안됌, 1=됌, -1=필터 아님
        public bool checkFilter(string filter, bool sortFrom = true, bool sortLength = true)
        {
            if (word.StartsWith(filter)) // 필터로 시작하는 경우
            {
                if (sortFrom)
                {
                    priority = 100;
                }
                else
                {
                    priority = 200;
                }
            } else if (word.EndsWith(filter)) // 필터로 끝나는 경우
            {
                if (sortFrom)
                {
                    priority = 200;
                }
                else
                {
                    priority = 100;
                }
            } else if (word.Contains(filter)) // 필터 내용을 포함하는 경우
            {
                priority = 300;
            } else // 고급 필터
            {
                priority = 0;
                string[] filters = filter.Split(' ');
                foreach (string subfilter in filters)
                {
                    if (advancedFilter(subfilter) == false)
                    {
                        return false;
                    }
                }
            }

            // 길이에 따라 순서 변화
            if (sortLength)
            {
                priority -= word.Length;
            }
            else
            {
                priority += word.Length;
            }
            return true;
        }
    }
}

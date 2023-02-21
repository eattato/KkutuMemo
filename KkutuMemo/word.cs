using System;
using System.Collections.Generic;
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

        // 필터를 거쳐 나올 수 있는 단어인지 줌, 0=안됌, 1=됌, -1=필터 아님
        public int checkFilter(string filter)
        {
            string[] contains = filter.Split(':'); // 가:마 -> "가"나다라"마"바사 o
            string[] all = filter.Split('~'); // 가~마 -> "가"나다라'마'바"사" x, 가~사 o

            if (contains.Length == 2)
            {
                bool startFound = false;
                bool endFound = false;
                foreach (char c in word)
                {
                    if (!startFound && c == contains[0][0])
                    {
                        startFound = false;
                    } else if (c == contains[1][0])
                    {
                        endFound = true;
                        break;
                    }
                }

                if (startFound && endFound)
                {
                    return 1;
                } else
                {
                    return 0;
                }
            }
            else if (all.Length == 2)
            {
                if (word[0] == all[0][0] && word[word.Length - 1] == all[1][0])
                {
                    return 1;
                } else
                {
                    return 0;
                }
            } else if (word.Contains(filter))
            {
                return 1;
            }
            return -1;
        }
    }
}

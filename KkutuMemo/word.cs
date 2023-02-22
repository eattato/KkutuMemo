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
                string[] contains = filter.Split(':'); // 나:마 -> 가"나"다라"마"바사 o
                string[] mixed = filter.Split(';'); // 가;마;사 -> "가"나다라"마"바"사" o
                string[] all = filter.Split('~'); // 가~마 -> "가"나다라'마'바"사" x, 가~사 o

                if (contains.Length >= 2) // 내용 포함 필터
                {
                    List<string> contain = contains.ToList();
                    foreach (char c in word)
                    {
                        if (contain.Count == 0)
                        {
                            break;
                        } else if (c == contain[0][0])
                        {
                            contain.RemoveAt(0);
                        }
                    }

                    if (contain.Count > 0)
                    {
                        return false;
                    }
                } else if (mixed.Length >= 2) // 혼합 필터
                {
                    List<string> mix = mixed.ToList();

                    char first = mix[0][0];
                    char last = mix[mix.Count - 1][0];

                    mix.RemoveAt(mix.Count - 1);
                    mix.RemoveAt(0);

                    for (int ind = 1; ind < word.Length - 1; ind++)
                    {
                        char c = word[ind];
                        if (mix.Count == 0)
                        {
                            break;
                        }
                        else if (c == mix[0][0])
                        {
                            mix.RemoveAt(0);
                        }
                    }

                    if (mix.Count > 0 || !(first == word[0] && last == word[word.Length - 1]))
                    {
                        return false;
                    }
                }
                else if (all.Length == 2) // 시작~끝 필터
                {
                    if (!(word[0] == all[0][0] && word[word.Length - 1] == all[1][0]))
                    {
                        return false;
                    }
                }
                else if (!word.Contains(filter))
                {
                    return false;
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

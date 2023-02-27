using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KkutuMemo
{
    internal class Hangul
    {
        private static string[] craft = new string[]
            {
                "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ",
                "ㅏㅐㅑㅒㅓㅔㅕㅖㅗㅘㅙㅚㅛㅜㅝㅞㅟㅠㅡㅢㅣ",
                " ㄱㄲㄳㄴㄵㄶㄷㄹㄺㄻㄼㄽㄾㄿㅀㅁㅂㅄㅅㅆㅇㅈㅊㅋㅌㅍㅎ"
            };

        public static char[] split(char target)
        {
            int temp = Convert.ToUInt16(target);
            if (temp >= 0xAC00 && temp <= 0xD79F)
            {
                temp -= 0xAC00;

                char cho = craft[0][temp / (21 * 28)];
                temp %= 21 * 28;

                char jung = craft[1][temp / 28];
                temp %= 28;

                // 종성이 없는 경우가 있을 수 있음, 종성이 없으면 temp가 0으로 나옴
                char jong = craft[2][temp];
                return new char[] { cho, jung, jong };
            } else if (target >= 'ㄱ' && target <= 'ㅎ') { // 초성만 있을 경우
                return new char[] { target, ' ', ' ' };
            }
            else
            {
                return null;
            }
        }

        public static char merge(char[] target)
        {
            int chosung = craft[0].IndexOf(target[0]);
            int jungsung = craft[1].IndexOf(target[1]);
            int jongsung = craft[2].IndexOf(target[2]);

            int result = 0xAC00 + (chosung * 21 + jungsung) * 28 + jongsung;
            return Convert.ToChar(result);
        }

        public static char duum(char target)
        {
            char[] charSplit = split(target);
            
            if (charSplit != null)
            {
                if (
                // 한자음 녀, 뇨, 뉴, 니 → 여, 요, 유, 이
                    charSplit[0] == 'ㄴ' &&
                    (charSplit[1] == 'ㅕ' || charSplit[1] == 'ㅛ' || charSplit[1] == 'ㅠ' || charSplit[1] == 'ㅣ')
                )
                {
                    charSplit[0] = 'ㅇ';
                }

                else if (
                    // 한자음 랴, 려, 례, 료, 류, 리 → 야, 여, 예, 요, 유, 이
                    charSplit[0] == 'ㄹ' &&
                    (charSplit[1] == 'ㅑ' || charSplit[1] == 'ㅕ' || charSplit[1] == 'ㅖ' || charSplit[1] == 'ㅛ' || charSplit[1] == 'ㅠ' || charSplit[1] == 'ㅣ')
                )
                {
                    charSplit[0] = 'ㅇ';
                }

                else if (
                    // 한자음 라, 래, 로, 뢰, 루, 르 → 나, 내, 노, 뇌, 누, 느
                    charSplit[0] == 'ㄹ' &&
                    (charSplit[1] == 'ㅏ' || charSplit[1] == 'ㅐ' || charSplit[1] == 'ㅗ' || charSplit[1] == 'ㅚ' || charSplit[1] == 'ㅜ' || charSplit[1] == 'ㅡ')
                )
                {
                    charSplit[0] = 'ㄴ';
                }

                return merge(charSplit);
            } else
            {
                return target;
            }
        }

        public static char[] superSplit(char target)
        {
            Dictionary<char, char[]> map = new Dictionary<char, char[]>();
            map.Add('ㄳ', new char[] { 'ㄱ', 'ㅅ' });
            map.Add('ㄵ', new char[] { 'ㄴ', 'ㅈ' });
            map.Add('ㄶ', new char[] { 'ㄴ', 'ㅎ' });
            map.Add('ㄺ', new char[] { 'ㄹ', 'ㄱ' });
            map.Add('ㄻ', new char[] { 'ㄹ', 'ㅁ' });
            map.Add('ㄼ', new char[] { 'ㄹ', 'ㅂ' });
            map.Add('ㄽ', new char[] { 'ㄹ', 'ㅅ' });
            map.Add('ㄾ', new char[] { 'ㄹ', 'ㅌ' });
            map.Add('ㄿ', new char[] { 'ㄹ', 'ㅍ' });
            map.Add('ㅀ', new char[] { 'ㄹ', 'ㅎ' });
            map.Add('ㅄ', new char[] { 'ㅂ', 'ㅅ' });

            if (map.ContainsKey(target)) {
                return map[target];
            } else
            {
                return new char[] { target };
            }
        }
    }
}

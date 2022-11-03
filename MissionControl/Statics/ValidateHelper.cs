using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace MissionControl.Statics
{
    public class ValidateHelper
    {
        public static string RemoveCharacter(string str, char character)
        {
            if (str == null)
                return null;
            int charindex = str.IndexOf(character);
            if (charindex != -1)
                str = str.Remove(charindex, 1);
            return str;
        }
        
        public static string Sanitize(string str, bool allow_newline, bool allow_upper, bool allow_numeric, List<string> allow_tag, char[] allowed, out bool ok)
        {
            /*
             * maybe this is a wrong approach, but this is as far as Ive gotten with sanitizing
             * there are many pitfalls when sanitizing, I havent thought of them all:)
             * */

            if (str.IsNull())
                throw new Exception();

            if (allow_tag.IsNull())
                throw new Exception();

            if (allow_tag.Contains(""))
                throw new Exception();

            ok = true;

            //char[] tags = { '!', '1', '2', '3', '4', '5' };
            char[] numeric = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] alphalower = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'æ', 'ø', 'å' };
            char[] alphaupper = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Æ', 'Ø', 'Å' };
            char[] newline = { '\r', '\n' };
            //char[] deci = { ',', '.' };
            
            List<string> html = new List<string>()
            {
                "!doctype", 
                "a", "abbr", "acronym", "address", "applet", "area", "article", "aside", "audio", 
                "b",  "base", "basefont", "bdi", "bdo", "big", "blockquote", "body", "br", "button", 
                "canvas", "caption", "center", "cite", "code", "col", "colgroup", "data", "datalist", "dd", "del", "details", "dfn", "dialog", "dir", "div", "dl", "dt", 
                "em", "embed", 
                "fieldset", "figcaption", "figure", "font", "footer", "form", "frame", "frameset", 
                "h1", "h2", "h3", "h4", "h5", "h6", "head", "header", "hr", "html", 
                "i", "iframe", "img", "input", "ins", 
                "kbd", 
                "label","legend", "li", "link",
                "main", "map", "mark", "meta", "meter",
                "nav", "noframes", "noscript",
                "object", "ol", "optgroup", "option", "output",
                "p", "param", "picture", "pre", "progress", "q", 
                "rp", "rt", "ruby", 
                "s", "samp", "script", "section", "select", "small", "source", "span", "strike", "strong", "style", "sub", "summary", "sup", "svg",
                "table", "tbody", "td", "template", "textarea", "tfoot", "th", "thead", "time", "title", "tr", "track", "tt", 
                "u", "ul", 
                "var", "video", 
                "wbr" 
            };

            string tmp = str.ToLower().Trim();
            foreach(string _t in html)
            {
                if (allow_tag.Contains(_t))
                    continue;
                if(tmp.Contains("<" + _t + ">"))
                    ok = false;
            }

            if (!ok)
                return "";

            for (int i = 0; i < str.Length; i++)
            {
                char c = str.ElementAt(i);
                if (allowed.Contains(c) || (allow_newline && newline.Contains(c)) || (allow_upper && alphaupper.Contains(c)) || alphalower.Contains(c) || (allow_numeric && numeric.Contains(c)))
                    continue;
                
                str = RemoveCharacter(str, c);
                ok = false;
                i--;
            }

            return str;
        }

        public static string _Sanitize(string str, bool allow_newline, bool allow_upper, List<string> allow_tag, char[] allowed, out bool ok)
        {
            /*
             * maybe this is a wrong approach, but this is as far as Ive gotten with sanitizing
             * another approach could be to have a list of all html tags, exept allowed, and then check if the string contains any of those
             * there are many pitfalls when sanitizing, I havent thought of them all:)
             * */

            if (str.IsNull())
                throw new Exception();

            if (allow_tag.IsNull())
                throw new Exception();

            if (allow_tag.Contains(""))
                throw new Exception();

            ok = true;

            char[] tags = { '!', '1', '2', '3', '4', '5' };
            char[] numeric = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] alphalower = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'æ', 'ø', 'å' };
            char[] alphaupper = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'Æ', 'Ø', 'Å' };
            char[] newline = { '\r', '\n' };


            for (int i = 0; i < str.Length; i++)
            {
                char c = str.ElementAt(i);

                if (c == '<')
                {
                    /*
                     * first we check if its start of an allowed tag
                     * */
                    bool _ok = false;
                    foreach (string at in allow_tag)
                    {
                        if (i <= str.Length - at.Length - 2 && str.Substring(i, at.Length + 2) == "<" + at + ">")
                        {
                            _ok = true;
                            i += at.Length + 1;
                        }
                        else if (i <= str.Length - at.Length - 3 && str.Substring(i, at.Length + 3) == "</" + at + ">")
                        {
                            _ok = true;
                            i += at.Length + 2;
                        }
                        else if (i <= str.Length - at.Length - 4 && str.Substring(i, at.Length + 4) == "<" + at + " />")
                        {
                            _ok = true;
                            i += at.Length + 3;
                        }
                        if (_ok)
                            break;
                    }

                    /*
                     * then we check if its just a 'one time' occurance
                     * */
                    if (_ok)
                        continue;
                    else
                    {
                        bool __ok = true;
                        int j = i + 1;

                        //15 is just a number longer than any html tag
                        while (j < i + 15 && j < str.Length)
                        {
                            bool firstrun = j == i + 1;
                            char elem = str.ElementAt(j);
                            j++;

                            if (alphalower.Contains(elem) || alphaupper.Contains(elem) || tags.Contains(elem))
                                continue;

                            __ok = firstrun ? true : elem != '>';
                            if (!__ok)
                                str = "";

                            break;
                        }
                        ok = __ok;
                    }
                }
                if (!ok)
                    return str;
            }


            for (int i = 0; i < str.Length; i++)
            {
                char c = str.ElementAt(i);
                if (allowed.Contains(c) || (allow_newline && newline.Contains(c)) || (allow_upper && alphaupper.Contains(c)) || alphalower.Contains(c) || numeric.Contains(c))
                    continue;

                str = RemoveCharacter(str, c);
                ok = false;
                i--;
            }

            return str;
        }

        public static string Only(string str, char[] allowed, out bool ok)
        {
            if (str.IsNull())
                throw new Exception();

            ok = true;

            for (int i = 0; i < str.Length; i++)
            {
                char c = str.ElementAt(i);
                if (allowed.Contains(c))
                    continue;

                str = RemoveCharacter(str, c);
                ok = false;
                i--;
            }
            return str;
        }
    }
}
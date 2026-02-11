﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VirtualDesktopHelper
{
    /// <summary>
    /// 热键管理器，负责处理热键的存储、格式化和解析
    /// 所有对热键显示、存储格式以及从字符串解析的逻辑集中在这里，便于维护
    /// </summary>
    public static class HotkeyManager
    {
        private static readonly Dictionary<string, Keys> ModifierMap = new Dictionary<string, Keys> (StringComparer.OrdinalIgnoreCase)
        {
            { "CTRL", Keys.Control },
            { "CONTROL", Keys.Control },
            { "ALT", Keys.Alt },
            { "SHIFT", Keys.Shift },
            { "WIN", (Keys)NHotkey.WindowsForms.ModKeys.Windows }
        };

        /// <summary>
        /// 将修饰键转为用户可读的前缀（例如 "Ctrl + "）
        /// </summary>
        private static string BuildModifierPrefix(Keys modKeys)
        {
            var parts = new List<string>();

            if (modKeys.HasFlag(Keys.Control)) parts.Add("Ctrl");
            if (modKeys.HasFlag(Keys.Alt)) parts.Add("Alt");
            if (modKeys.HasFlag(Keys.Shift)) parts.Add("Shift");
            if (modKeys.HasFlag((Keys)NHotkey.WindowsForms.ModKeys.Windows)) parts.Add("Win");

            return parts.Count > 0 ? string.Join(" + ", parts) + " + " : string.Empty;
        }

        /// <summary>
        /// 格式化热键用于显示（界面友好）
        /// </summary>
        public static string FormatHotkey(Keys keys, Keys modKeys)
        {
            string result = BuildModifierPrefix(modKeys);

            // 获取不带修饰符标志的键名（过滤掉 Keys.Modifiers 标志）
            Keys pureKey = keys & ~Keys.Modifiers;
            if (pureKey != Keys.None)
            {
                result += pureKey.ToString();
            }

            // 兼容旧的枚举名称，尽量返回友好显示
            return result.Replace("ControlKey", "Ctrl").Replace("Menu", "Alt").Replace("ShiftKey", "Shift")
                         .Replace("LWin", "Win").Replace("RWin", "Win");
        }

        /// <summary>
        /// 格式化热键用于持久化存储（去掉冗余的 " + "）
        /// </summary>
        public static string FormatHotkeyForStorage(Keys keys, Keys modKeys)
        {
            if (keys == Keys.None && modKeys == Keys.None)
            {
                return string.Empty;
            }

            string prefix = BuildModifierPrefix(modKeys);

            Keys pureKey = keys & ~Keys.Modifiers;
            if (pureKey != Keys.None)
            {
                return (prefix + pureKey.ToString()).Trim();
            }

            // 只有修饰键时，去掉末尾的 " + "
            return prefix.TrimEnd(' ', '+').Trim();
        }

        /// <summary>
        /// 从存储字符串解析热键（支持 Ctrl/Alt/Shift/Win）
        /// </summary>
        public static void ParseHotkeyString(string hotkeyString, out Keys keys, out Keys modKeys)
        {
            keys = Keys.None;
            modKeys = Keys.None;

            if (string.IsNullOrWhiteSpace(hotkeyString))
            {
                return;
            }

            // 按'+'分割字符串以分离修饰符和键
            string[] parts = hotkeyString.Split('+');

            foreach (string part in parts)
            {
                string trimmedPart = part.Trim();
                if (string.IsNullOrEmpty(trimmedPart))
                {
                    continue;
                }

                // 先尝试作为修饰键解析
                if (ModifierMap.TryGetValue(trimmedPart.ToUpperInvariant(), out Keys mapped))
                {
                    modKeys |= mapped;
                    continue;
                }

                // 否则尝试解析为 Keys 枚举
                if (Enum.TryParse(trimmedPart, true, out Keys parsedKey))
                {
                    keys = parsedKey;
                }
            }
        }
    }
}
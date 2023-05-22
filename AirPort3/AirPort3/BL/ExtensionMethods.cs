using AirPort.DB.Models;
using AirPort.SignalR;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace AirPort.BL {
    public static class Util {

        /// <summary>
        /// Console Color - write to the console with color
        /// </summary>
        /// <param name="text">The text to write to the console</param>
        /// <param name="color">The color to paint the text</param>
        public static void PrintColor(this string text, ConsoleColor color) {
            //var now = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// Convert Any Variable to Json String
        /// </summary>
        /// <typeparam name="T">Any Type</typeparam>
        /// <param name="t">The Object</param>
        /// <returns>String in Json </returns>
        public static string ToJson<T>(this T t) {
            return JsonSerializer.Serialize(t);
        }

        /// <summary>
        /// Convert Json String to Object
        /// </summary>
        /// <typeparam name="T">The object type to Convert To</typeparam>
        /// <param name="json">The String</param>
        /// <returns>object T</returns>
        public static T? FromJson<T>(this string json) {
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}

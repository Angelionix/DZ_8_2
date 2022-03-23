using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrgInfoSystem_prototype
{
    static class Utility
    {
        #region SafetyInput
        /// <summary>
        /// Метод защищенного ввода для Int в заданном диапазоне
        /// </summary>
        /// <param name="min"> Нижняя граница допустимого диапазона</param>
        /// <param name="max"> Верхняя граница диапазона</param>
        /// <returns>Значине в Int </returns>
        public static int SafeInputing(int min, int max)
        {
            bool correctAnswer = true;
            int choise = 0;
            do                                                                                                      // защищенны ввод того как будем вводить данные для рассчетов
            {                                                                                                       //
                if (int.TryParse(Console.ReadLine(), out choise))                                                   // Чекаем что ввели число, а не другой символ
                {                                                                                                   //
                    if (choise < min || choise > max)                                                                   // Чекаем что чило в корректном диапазоне
                    {                                                                                               //
                        Console.WriteLine($"Пожалуйста введите число от {min} до {max}");                                   // Если число не корректное просим ввести заново
                        correctAnswer = false;                                                                        // переменная райтасер делаем false чтобы заново запустить цикл ввода
                    }                                                                                               //
                    else                                                                                            //
                    {                                                                                               // Если число коректное, то переменной rightAnswer присваиваем true
                        correctAnswer = true;                                                                         // и идем дальше
                    }                                                                                               //
                }                                                                                                   //
                else                                                                                                //
                {                                                                                                   //
                    Console.WriteLine("Пожалуйста введите число, а не какой либо иной символ");                    // Если ввели не число просим ввести корректное чилсо 
                    correctAnswer = false;                                                                            // и запускаем цикл ввода заново
                }                                                                                                   //
            }                                                                                                       //
            while (!correctAnswer);
            return choise;
        }

        /// <summary>
        ///  Метод для безопасного ввода большиз числе в определенром интервале
        /// </summary>
        /// <param name="min">мин значение</param>
        /// <param name="max">макс значение</param>
        /// <returns>чилсо типа ulong </returns>
        public static ulong SafeInputing(ulong min, ulong max)
        {
            bool correctAnswer = true;
            ulong choise = 0;
            do                                                                                                      // защищенны ввод того как будем вводить данные для рассчетов
            {                                                                                                       //
                if (ulong.TryParse(Console.ReadLine(), out choise))                                                   // Чекаем что ввели число, а не другой символ
                {                                                                                                   //
                    if (choise < min || choise > max)                                                                   // Чекаем что чило в корректном диапазоне
                    {                                                                                               //
                        Console.WriteLine($"Пожалуйста введите число от {min} до {max}");                                   // Если число не корректное просим ввести заново
                        correctAnswer = false;                                                                        // переменная райтасер делаем false чтобы заново запустить цикл ввода
                    }                                                                                               //
                    else                                                                                            //
                    {                                                                                               // Если число коректное, то переменной rightAnswer присваиваем true
                        correctAnswer = true;                                                                         // и идем дальше
                    }                                                                                               //
                }                                                                                                   //
                else                                                                                                //
                {                                                                                                   //
                    Console.WriteLine("Пожалуйста введите число, а не какой либо иной символ");                    // Если ввели не число просим ввести корректное чилсо 
                    correctAnswer = false;                                                                            // и запускаем цикл ввода заново
                }                                                                                                   //
            }                                                                                                       //
            while (!correctAnswer);
            return choise;
        }
        #endregion
    }
}

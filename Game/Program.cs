#region Using Statements
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
#endregion

namespace RescueGame
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Console.WriteLine("당신의 크리스마스를 지키는 중...");

            var exceptdevenv = Process.GetProcessesByName("devenv");

            for (; ; )
            {
                var devenvs = Process.GetProcessesByName("devenv");

                foreach (var p in devenvs)
                {
                    if (exceptdevenv.Any(elem => elem.Id == p.Id)) continue;
                    System.Threading.Thread.Sleep(10);
                    var filename = p.MainModule.FileName;
                    p.Kill();
                    System.Console.WriteLine("비주얼스튜디오가 납치당했어요!");

                    using (var game = new RescueGame())
                    {
                        game.Run();
                        if (game.enemyreport.IsDestroyed)
                        {
                            System.Console.WriteLine(@"당신은 학점을(를) 잃었습니다.
당신은 애인을(를) 잃었습니다. 아니 원래 없었나요?
학사경고증이(가) 당신의 부모님께 배송되었습니다.
비주얼스튜디오를 되찾았습니다!! 하지만... 이게 최선일까요?
당신은 아직 Ctrl-C를 눌러 이 작업을 취소할 수 있습니다.");
                            Console.ReadLine();
                            Process.Start(filename);
                            return;
                        }
                        else
                        {
                            System.Console.WriteLine("훌륭한 선택입니다! 오늘 하루정돈 산책이라도 하며 자신을 돌아보는 시간을 가져봐요!");
                        }
                    }
                }
                System.Threading.Thread.Sleep(10);

            }
        }
    }
#endif
}

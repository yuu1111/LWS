using System;




namespace LWS
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            RandomTitleGenerator.Initialize("ndata.csv");

            for (int i = 1; i < 17; ++i)
            {
                HSPRNG.Randomize(10500 + i);
                Console.WriteLine(RandomTitleGenerator.Generate(true));
            }


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
        }
    }
}
using System;
using System.Runtime.InteropServices;



namespace LWS
{
    // FIXME: simplify this ugly duplicate boilerplate code.



    /// <summary>
    /// Hot Soup Processor's Random Number Generator
    /// </summary>
    class HSPRNG
    {
        /// <summary>
        /// Generate random integer within [0, max).
        /// </summary>
        public static int Rnd(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException("\"max\" must be greater than 0.");

            if (Environment.Is64BitProcess)
                return HSP_rnd64(max);
            else
                return HSP_rnd32(max);
        }


        /// <summary>
        /// Set current tick count to the seed.
        /// </summary>
        public static void Randomize()
        {
            if (Environment.Is64BitProcess)
                HSP_randomize_time64();
            else
                HSP_randomize_time32();
        }


        /// <summary>
        /// Set current tick count to the seed.
        /// </summary>
        public static void Randomize(int seed)
        {
            if (Environment.Is64BitProcess)
                HSP_randomize64(seed);
            else
                HSP_randomize32(seed);
        }



        /// <summary>
        /// Generate random integer within [0, max).
        /// </summary>
        public static int ExRnd(int max)
        {
            if (max <= 0)
                throw new ArgumentOutOfRangeException("\"max\" must be greater than 0.");

            if (Environment.Is64BitProcess)
                return HSPexrand_rnd64(max);
            else
                return HSPexrand_rnd32(max);
        }


        /// <summary>
        /// Set current tick count to the seed.
        /// </summary>
        public static void ExRandomize()
        {
            if (Environment.Is64BitProcess)
                HSPexrand_randomize_time64();
            else
                HSPexrand_randomize_time32();
        }


        /// <summary>
        /// Set current tick count to the seed.
        /// </summary>
        public static void ExRandomize(int seed)
        {
            if (seed < 0)
                throw new ArgumentOutOfRangeException("\"seed\" must be greater than or equal to 0.");

            if (Environment.Is64BitProcess)
                HSPexrand_randomize64(seed);
            else
                HSPexrand_randomize32(seed);
        }




        // 32-bit functions
        [DllImport("HSP_rnd_x86.dll", EntryPoint = "HSP_rnd")]
        private static extern int HSP_rnd32(int max);

        [DllImport("HSP_rnd_x86.dll", EntryPoint = "HSP_randomize_time")]
        private static extern void HSP_randomize_time32();

        [DllImport("HSP_rnd_x86.dll", EntryPoint = "HSP_randomize")]
        private static extern void HSP_randomize32(int seed);


        [DllImport("HSP_rnd_x86.dll", EntryPoint = "HSPexrand_rnd")]
        private static extern int HSPexrand_rnd32(int max);

        [DllImport("HSP_rnd_x86.dll", EntryPoint = "HSPexrand_randomize_time")]
        private static extern void HSPexrand_randomize_time32();

        [DllImport("HSP_rnd_x86.dll", EntryPoint = "HSPexrand_randomize")]
        private static extern void HSPexrand_randomize32(int seed);



        // 64-bit functions
        [DllImport("HSP_rnd_x64.dll", EntryPoint = "HSP_rnd")]
        private static extern int HSP_rnd64(int max);

        [DllImport("HSP_rnd_x64.dll", EntryPoint = "HSP_randomize_time")]
        private static extern void HSP_randomize_time64();

        [DllImport("HSP_rnd_x64.dll", EntryPoint = "HSP_randomize")]
        private static extern void HSP_randomize64(int seed);


        [DllImport("HSP_rnd_x64.dll", EntryPoint = "HSPexrand_rnd")]
        private static extern int HSPexrand_rnd64(int max);

        [DllImport("HSP_rnd_x64.dll", EntryPoint = "HSPexrand_randomize_time")]
        private static extern void HSPexrand_randomize_time64();

        [DllImport("HSP_rnd_x64.dll", EntryPoint = "HSPexrand_randomize")]
        private static extern void HSPexrand_randomize64(int seed);
    }
}

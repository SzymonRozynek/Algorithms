using System;
using SFML;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Threading;

namespace Algorithms {
    static class MainClass {

        static void Main() {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Program p = new Program();
            p.Start();
        }
    }
}
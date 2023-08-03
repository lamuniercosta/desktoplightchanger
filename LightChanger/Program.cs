using CUE.NET;
using CUE.NET.Brushes;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.InteropServices;

namespace LightChanger
{
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    public partial class Program
    {
        [LibraryImport("User32.dll", SetLastError = true)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [LibraryImport("LightFX.dll", SetLastError = true)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        [return: MarshalAs(UnmanagedType.I4)]
        private static partial int LFX_Initialize();
        
        [LibraryImport("LightFX.dll", SetLastError = true)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        [return: MarshalAs(UnmanagedType.I4)]
        private static partial int LFX_Update();

        [LibraryImport("LightFX.dll", SetLastError = true)]
        [UnmanagedCallConv(CallConvs = new Type[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        [return: MarshalAs(UnmanagedType.I4)]
        private static partial int LFX_Light(int locationMask, int colorVal);

        private static void Main(string[] args)
        {
            var handle = Process.GetCurrentProcess().MainWindowHandle;
            LFX_Initialize();
            CueSDK.Initialize();
            ShowWindow(handle, 0);

            while (true)
            {
                Thread.Sleep(1);
                InitializeLfx();

                UpdateKeyboard();

                UpdateMousePad();

                UpdateMouse();
            }
        }

        private static void InitializeLfx()
        {
            const int locationMask = 0x07FFFFFF;
            const int colorMask = 0x00000000;
            LFX_Light(locationMask, colorMask);
            LFX_Update();
        }

        private static void UpdateMouse()
        {
            var mouse = CueSDK.MouseSDK;
            mouse.Brush = new SolidColorBrush(Color.Black);
            mouse.Update();
        }

        private static void UpdateMousePad()
        {
            var mousepad = CueSDK.MousematSDK;
            mousepad.Brush = new SolidColorBrush(Color.Black);
            mousepad.Update();
        }

        private static void UpdateKeyboard()
        {
            var keyboard = CueSDK.KeyboardSDK;
            keyboard.Brush = new SolidColorBrush(Color.Black);
            keyboard.Update();
        }
    }
}
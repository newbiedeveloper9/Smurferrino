using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Smurferrino.Helpers;

namespace Smurferrino.Business.Helpers
{
    public static class ManageMemory
    {
        public static Process Process;
        public static IntPtr ptrProcessHandle;

        public static int NumberOfBytesRead = 0;
        public static int NumberOfBytesWritten = 0;

        public static bool Attach(string processName)
        {
            if (Process.GetProcessesByName(processName).Length > 0)
            {
                Process = Process.GetProcessesByName(processName)[0];
                ptrProcessHandle = WinApi.OpenProcess(PROCESS_VM_OPERATION | PROCESS_VM_READ | PROCESS_VM_WRITE, false,
                    Process.Id); // Sets Our ProcessHandle
                return true;
            }

            return false;
        }

        public static int GetModuleAddress(string moduleName)
        {
            try
            {
                foreach (ProcessModule ProcMod in Process.Modules)
                {
                    if (!moduleName.Contains(".dll"))
                        moduleName = moduleName.Insert(moduleName.Length, ".dll");

                    if (moduleName == ProcMod.ModuleName)
                        return (int)ProcMod.BaseAddress;
                }
            }
            catch
            {
            }

            return -1;
        }

        public static T ReadMemory<T>(int address) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T)); // Get ByteSize Of DataType
            byte[] buffer = new byte[ByteSize]; // Create A Buffer With Size Of ByteSize
            WinApi.ReadProcessMemory((int)ptrProcessHandle, address, buffer, buffer.Length,
                ref NumberOfBytesRead); // Read value From MemoryAddr

            return ByteArrayToStructure<T>(buffer); // Transform the ByteArray to The Desired DataType
        }

        public static float[] ReadMatrix<T>(int address, int matrixSize) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T));
            byte[] buffer = new byte[ByteSize * matrixSize]; // Create A Buffer With Size Of ByteSize * matrixSize
            WinApi.ReadProcessMemory((int)ptrProcessHandle, address, buffer, buffer.Length, ref NumberOfBytesRead);

            return ConvertToFloatArray(buffer); // Transform the ByteArray to A Float Array (PseudoMatrix ;P)
        }

        public static void WriteMemory<T>(int address, object value)
        {
            byte[] buffer = StructureToByteArray(value); // Transform Data To ByteArray 

            WinApi.WriteProcessMemory((int)ptrProcessHandle, address, buffer, buffer.Length, out NumberOfBytesWritten);
        }

        public static void WriteMemory<T>(int address, char[] value)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(value);

            WinApi.WriteProcessMemory((int)ptrProcessHandle, address, buffer, buffer.Length, out NumberOfBytesWritten);
        }

        #region Transformation

        public static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentException();

            float[] floats = new float[bytes.Length / 4];

            for (int i = 0; i < floats.Length; i++)
                floats[i] = BitConverter.ToSingle(bytes, i * 4);

            return floats;
        }

        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        private static byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }

        #endregion

        #region Constants

        const int PROCESS_VM_OPERATION = 0x0008;
        const int PROCESS_VM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;

        #endregion
    }
}


//Thanks to
//https://github.com/C0re-Cheats/C0reExternal-BaseOffset-v2/blob/master/MemoryAddr.cs
//<3
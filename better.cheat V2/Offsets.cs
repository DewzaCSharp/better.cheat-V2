﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace better.cheat_V2
{
    public class Offsets
    {
        public static byte[] PumpAmmoOG = { 0x89, 0x83, 0x9C, 0x04, 0x00, 0x00 };
        public static byte[] PumpAmmo = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

        public static byte[] arAmmoOG = { 0x89, 0x83, 0xAC, 0x04, 0x00, 0x00 };
        public static byte[] arAmmo = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

        public static byte[] CameraUpDownOG = { 0xF2, 0x44, 0x0F, 0x11, 0x4B, 0x28 };
        public static byte[] CameraUpDownPatch = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

        public static byte[] CameraLeftRightOG = { 0xF3, 0x44, 0x0F, 0x11, 0x4B, 0x30 };
        public static byte[] CameraLeftRightPatch = { 0x90, 0x90, 0x90, 0x90, 0x90, 0x90 };

        public static byte[] CameraFunOG = { 0xF2, 0x0F, 0x11, 0x7B, 0x40 };
        public static byte[] CameraFunPatch = { 0x90, 0x90, 0x90, 0x90, 0x90 };

        public static byte[] SidewaysCameraOG = { 0xF2, 0x0F, 0x10, 0x53, 0x40 };
        public static byte[] SidewaysCameraPatch = { 0x90, 0x90, 0x90, 0x90, 0x90 };
    }
}

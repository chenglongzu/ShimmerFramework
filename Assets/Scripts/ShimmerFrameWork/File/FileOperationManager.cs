﻿using System.Runtime.InteropServices;
using System;

namespace ShimmerFramework
{
    public class FileOperationManager : BaseManager<FileOperationManager>
    {
        public byte[] OpenFile()
        {
            FileOpenDialog dialog = new FileOpenDialog();

            dialog.structSize = Marshal.SizeOf(dialog);

            //dialog.filter = "png files\0*.png\0psd files\0*.psd\0jpg files\0*.jpg\0All Files\0*.*\0\0";

            dialog.file = new string(new char[256]);

            dialog.maxFile = dialog.file.Length;

            dialog.fileTitle = new string(new char[64]);

            dialog.maxFileTitle = dialog.fileTitle.Length;

            dialog.initialDir = UnityEngine.Application.dataPath;  //默认路径

            dialog.title = "选择文件";

            //注意一下项目不一定要全选 但是0x00000008项不要缺少
            dialog.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;

            if (DialogShow.GetOpenFileName(dialog))
            {
                //读取文件存储为字节数组
                byte[] buffer = new byte[] { };
                buffer = ByteOperation.GetInstance().ReadFileToByte(dialog.file);

                return buffer;
            }

            return null;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class FileOpenDialog
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    public class DialogShow
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] FileOpenDialog dialog);  //这个方法名称必须为GetOpenFileName
    }

}

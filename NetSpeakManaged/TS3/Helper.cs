using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace NetSpeakManaged.TS3
{
    public static class Helper
    {
        public static unsafe string UTF8(this IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
                return null;
            using (var managedCopy = new MemoryStream())
            {
                byte* current = (byte*)ptr.ToPointer();
                for (; *current != 0; current++ )//This does NOT copy the trailing zero
                {
                    managedCopy.WriteByte(*current);
                }
                managedCopy.Position = 0;
                using(var reader = new StreamReader(managedCopy, Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static unsafe IntPtr UTF8(this string inp)
        {
            if (inp == null)
                return IntPtr.Zero;
            byte[] managed = Encoding.UTF8.GetBytes(inp);
            IntPtr result = Marshal.AllocHGlobal(managed.Length + 1);
            byte* current = (byte*)result.ToPointer();
            for (int i = 0; i < managed.Length; i++ )
                current[i] = managed[i];
            current[managed.Length] = 0;
            
            return result;
        }
    }
}

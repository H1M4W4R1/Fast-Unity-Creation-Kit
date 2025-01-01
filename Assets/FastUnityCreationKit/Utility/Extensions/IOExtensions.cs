using System;
using System.IO;
using System.Security;
using FastUnityCreationKit.Utility.Logging;

namespace FastUnityCreationKit.Utility.Extensions
{
    public static class IOExtensions
    {
         /// <summary>
        /// Read bytes from a file.
        /// </summary>
        public static (bool, byte[]) TryReadBytes(string path)
        {
            try
            {
                byte[] data = File.ReadAllBytes(path);
                return (true, data);
            }
            catch (ArgumentNullException)
            {
                Guard<SaveLogConfig>.Error($"File not read. Path '{path}' is empty.");
            }
            catch (PathTooLongException)
            {
                Guard<SaveLogConfig>.Error($"File not read. Path '{path}' is too long.");
            }
            catch (DirectoryNotFoundException)
            {
                Guard<SaveLogConfig>.Error($"File not read. Directory for '{path}' not found.");
            }
            catch (IOException)
            {
                Guard<SaveLogConfig>.Error($"File not read. '{path}' cannot be read due to I/O issues. " +
                                           $"File may be in use or a directory.");
            }
            catch (UnauthorizedAccessException)
            {
                Guard<SaveLogConfig>.Error($"File not read. '{path}' cannot be read. Access denied.");
            }
            catch (NotSupportedException)
            {
                Guard<SaveLogConfig>.Error($"File not read. '{path}' cannot be read. Path is in an invalid format.");
            }
            catch (SecurityException)
            {
                Guard<SaveLogConfig>.Error($"File not read. '{path}' cannot be read. Security check failed.");
            }
            catch (Exception)
            {
                Guard<SaveLogConfig>.Error($"File not read. '{path}' cannot be read. Unknown issue detected.");
            }

            return (false, Array.Empty<byte>());
        }

        /// <summary>
        /// Try to write bytes to a file.
        /// </summary>
        public static bool TryWriteBytes(string path, byte[] bytes)
        {
            try
            {
                File.WriteAllBytes(path, bytes);
                return true;
            }
            catch (ArgumentNullException)
            {
                Guard<SaveLogConfig>.Error($"File not saved. Path '{path}' is empty.");
            }
            catch (PathTooLongException)
            {
                Guard<SaveLogConfig>.Error($"File not saved. Path '{path}' is too long.");
            }
            catch (DirectoryNotFoundException)
            {
                Guard<SaveLogConfig>.Error($"File not saved. Directory for '{path}' not found.");
            }
            catch (IOException)
            {
                Guard<SaveLogConfig>.Error($"File not saved. '{path}' cannot be saved due to I/O issues. " +
                                           $"File may be in use or a directory.");
            }
            catch (UnauthorizedAccessException)
            {
                Guard<SaveLogConfig>.Error($"File not saved. '{path}' cannot be saved. Access denied.");
            }
            catch (NotSupportedException)
            {
                Guard<SaveLogConfig>.Error($"File not saved. '{path}' cannot be saved. Path is in an invalid format.");
            }
            catch (SecurityException)
            {
                Guard<SaveLogConfig>.Error($"File not saved. '{path}' cannot be saved. Security check failed.");
            }
            catch (Exception)
            {
                Guard<SaveLogConfig>.Error($"File not saved. '{path}' cannot be saved. Unknown issue detected.");
            }

            return false;
        }
    }
}
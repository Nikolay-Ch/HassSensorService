using System;

namespace HassDeviceWorkers.ModBus
{
    public interface ISerialPort : IDisposable
    {
        //
        // Summary:
        //     Gets or sets the number of milliseconds before a time-out occurs when a read
        //     operation does not finish.
        //
        // Returns:
        //     The number of milliseconds before a time-out occurs when a read operation does
        //     not finish.
        //
        // Exceptions:
        //   T:System.IO.IOException:
        //     The port is in an invalid state. -or- An attempt to set the state of the underlying
        //     port failed. For example, the parameters passed from this System.IO.Ports.SerialPort
        //     object were invalid.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The read time-out value is less than zero and not equal to System.IO.Ports.SerialPort.InfiniteTimeout.
        public int ReadTimeout { get; set; }

        //
        // Summary:
        //     Writes a specified number of bytes to the serial port using data from a buffer.
        //
        // Parameters:
        //   buffer:
        //     The byte array that contains the data to write to the port.
        //
        //   offset:
        //     The zero-based byte offset in the buffer parameter at which to begin copying
        //     bytes to the port.
        //
        //   count:
        //     The number of bytes to write.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   T:System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   T:System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   T:System.ServiceProcess.TimeoutException:
        //     The operation did not complete before the time-out period ended.
        void Write(byte[] buffer, int offset, int count);

        //
        // Summary:
        //     Reads a number of bytes from the System.IO.Ports.SerialPort input buffer and
        //     writes those bytes into a byte array at the specified offset.
        //
        // Parameters:
        //   buffer:
        //     The byte array to write the input to.
        //
        //   offset:
        //     The offset in buffer at which to write the bytes.
        //
        //   count:
        //     The maximum number of bytes to read. Fewer bytes are read if count is greater
        //     than the number of bytes in the input buffer.
        //
        // Returns:
        //     The number of bytes read.
        //
        // Exceptions:
        //   T:System.ArgumentNullException:
        //     The buffer passed is null.
        //
        //   T:System.InvalidOperationException:
        //     The specified port is not open.
        //
        //   T:System.ArgumentOutOfRangeException:
        //     The offset or count parameters are outside a valid region of the buffer being
        //     passed. Either offset or count is less than zero.
        //
        //   T:System.ArgumentException:
        //     offset plus count is greater than the length of the buffer.
        //
        //   T:System.TimeoutException:
        //     No bytes were available to read.
        int Read(byte[] buffer, int offset, int count);
    }
}

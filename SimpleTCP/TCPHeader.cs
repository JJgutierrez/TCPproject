using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

namespace SimpleTCP
{
    public class TCPHeader
    {
        ushort source_port; // 16 bits
        ushort destination_port; // 16 bits
        uint sequence_number; // 32 bits
        uint acknowledgment_number; // 32 bits
        ushort data_offset_reserved_control_bits;
        
        ushort window; // 16 bits
        ushort checksum; // 16 bits
        ushort urgent_pointer; // 16 bits
        uint options; // variable
      

        public TCPHeader(ushort source_port, ushort destination_port, ushort sequence_number, uint acknowledgment_number,
            ushort data_offset_reserved_control_bits, ushort window, ushort checksum, ushort urgent_pointer, uint options)
        {
            setSourcePort(source_port);
            setDestinationPort(destination_port);
            setSequenceNumber(sequence_number);
            setAcknowledgmentNumber(acknowledgment_number);
            setDataOffsetReservedControlBits(data_offset_reserved_control_bits);
            setWindow(window);
            setChecksum(checksum);
            setUrgentPointer(urgent_pointer);
            setOptions(options);
        }

        public byte[] getHeader()
        {
            byte[] sp = getSourcePort();
            byte[] dp = getDestinationPort();
            byte[] sn = getSequenceNumber();
            byte[] an = getAcknowledgmentNumber();
            byte[] w = getWindow();
            byte[] cs = getCheckSum();
            byte[] up = getUrgentPointer();
            byte[] o = getOptions();
            byte[] headerArray = Concat(sp, dp, sn, an, w, cs, up, o);
            return headerArray;
        }

        private void setSourcePort(ushort source_port = 80)
        {
            this.source_port = source_port;
        }

        private byte[] getSourcePort()
        {
            return ConvertToByteArray(source_port);
        }

        private void setDestinationPort(ushort destination_port = 80)
        {
            this.destination_port = destination_port;
        }

        private byte[] getDestinationPort()
        {
            return ConvertToByteArray(destination_port);
        }

        private void setSequenceNumber(uint seq_num)
        {
            sequence_number = seq_num;
        }
        private byte[] getSequenceNumber()
        {
            return ConvertToByteArray(sequence_number);
        }

        private void setAcknowledgmentNumber(uint ack_num)
        {
            acknowledgment_number = ack_num;
        }
        private byte[] getAcknowledgmentNumber()
        {
            return ConvertToByteArray(acknowledgment_number);
        }

        private void setDataOffsetReservedControlBits(ushort data_offset_reserved_control_bits)
        {
            this.data_offset_reserved_control_bits = data_offset_reserved_control_bits;
        }

        private ushort getDataOffsetReservedControlBits()
        {
            return data_offset_reserved_control_bits;
        }

        private void setWindow(ushort window)
        {
            this.window = window; 
        }
        private byte[] getWindow()
        {
            return ConvertToByteArray(window);
        }

        private void setChecksum(ushort checksum)
        {
            this.checksum = checksum;
        }
        private byte[] getCheckSum()
        {
            return ConvertToByteArray(checksum);
        }

        public void setUrgentPointer(ushort urgent_pointer)
        {
            this.urgent_pointer = urgent_pointer;
        }

        public byte[] getUrgentPointer()
        {
            return ConvertToByteArray(urgent_pointer);
        }
        public void setOptions(uint options)
        {
            this.options = options;
        }
        public byte[] getOptions()
        {
            return ConvertToByteArray(options);
        }
        private byte[] ConvertToByteArray(uint value)
        {
            byte[] byteArray = BitConverter.GetBytes(value);
            return byteArray;
        }
        private byte[] ConvertToByteArray(ushort value)
        {
            byte[] byteArray = BitConverter.GetBytes(value);
            return byteArray;
        }

        private  int ZeroBit(int value, int position)
        {
            return value & ~(1 << position);
        }
        public static byte[] Concat(params byte[][] arrays)
        {
            using (var mem = new MemoryStream(arrays.Sum(a => a.Length)))
            {
                foreach (var array in arrays)
                {
                    mem.Write(array, 0, array.Length);
                }
                return mem.ToArray();
            }
        }

        //public getString()
        //{
        //    string s = System.Text.Encoding.UTF8.GetString(this);
        //    return s;
        //}

        /*public T FromByteArray<T>(byte[] rawValue)
        {
            GCHandle handle = GCHandle.Alloc(rawValue, GCHandleType.Pinned);
            T structure = (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            handle.Free();
            return structure;
        }

        public byte[] ToByteArray(Object obj, int maxLength)
        {
            int rawsize = Marshal.SizeOf(obj);
            byte[] rawdata = new byte[rawsize];
            GCHandle handle = GCHandle.Alloc(rawdata, GCHandleType.Pinned);
            Marshal.StructureToPtr(obj, handle.AddrOfPinnedObject(), false);
            handle.Free();
            if (maxLength < rawdata.Length)
            {
                byte[] temp = new byte[maxLength];
                Array.Copy(rawdata, temp, maxLength);
                return temp;
            }
            else
            {
                return rawdata;
            }
        }*/
    }

    public enum control_digits
    {
        URG,
        ACK,
        PSH,
        RST,
        SYN,
        FIN,
    }

}

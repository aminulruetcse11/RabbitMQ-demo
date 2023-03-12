using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQSender
{
    internal class AttendanceRepository
    {
        List<AttendanceData> AttendanceData;
        public AttendanceRepository()
        {
            AttendanceData = new List<AttendanceData>();
            for (int i = 0; i < 100; i++)
            {
                AttendanceData.Add(new AttendanceData
                {
                    UserId = i + 1,
                    Date = DateTime.Now.ToString()
                });
            }
        }

        public List<AttendanceData> GetData()
        {
            return AttendanceData;
        }
    }
}
